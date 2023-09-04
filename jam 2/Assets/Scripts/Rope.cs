using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float angleRange = 70;
    public float speed = 40 ;
    public GameObject anchorPrefab;
    public GameObject? anchor;

    HingeJoint2D hinge;


    enum State {
        SWITCHING,
        IDLE
    }

    State ropeState;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    float tempDeg;

    // Update is called once per frame
    void Update()
    {
        switch (hinge.limitState) {
            case JointLimitState2D.LowerLimit:
            case JointLimitState2D.UpperLimit:
                if(ropeState != State.SWITCHING) {
                    JointMotor2D motor = hinge.motor;
                    motor.motorSpeed = -motor.motorSpeed;
                    hinge.motor = motor;

                    ropeState = State.SWITCHING;
                }
                break;
            case JointLimitState2D.Inactive:
                ropeState = State.IDLE;
                break;
        }
    }


    ContactPoint2D pt;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D c = collision.collider;
        if (c.tag == "Player" && !anchor)
        {
            anchor = Instantiate(anchorPrefab, new Vector2(0, 0), Quaternion.identity);
            anchor.name = "anchor";
            anchor.transform.position = GetAnchor(collision.contacts[0].point);
            anchor.transform.parent = transform;

            c.gameObject.GetComponent<PlayerScript>().action = PlayerScript.Action.SWINGING;
            c.gameObject.GetComponent<PlayerScript>().rope = this;

            anchor.GetComponent<FixedJoint2D>().connectedBody = c.gameObject.GetComponent<Rigidbody2D>();
        }
    }


    Vector2 GetAnchor(Vector2 pt) { 
        Vector3 transPoint = transform.InverseTransformPoint(pt);
        transPoint = new Vector3(0, transPoint.y, transPoint.z);
        return transform.TransformPoint(transPoint);
	}


    Vector3 rotateAroundOrigin(Vector3 pt, float deg) { 
        float rad = Mathf.Deg2Rad * deg;
        return new Vector3(pt.x * Mathf.Cos(rad) - pt.y * Mathf.Sin(rad), pt.x * Mathf.Sin(rad) + pt.y * Mathf.Cos(rad));
	}


    Vector3 crossProduct(Vector3 a, Vector3 b) {
        return new Vector3(a.y * b.z - b.y * a.z, a.x * b.z - b.x * a.z, a.x * b.y - b.x * a.y);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // origin
        Vector3 end = new Vector3(0, transform.localScale.y);
        end = -1 * rotateAroundOrigin(end, tempDeg);

        //print("untranslated pos: " + (transform.position + end));
        //print("translated pos: " + transform.InverseTransformPoint(transform.position + end));
        //print("translated collision: " + transform.InverseTransformPoint(pt.point));
        Gizmos.DrawLine(transform.position, transform.position + end);
        

        if(anchor != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(anchor.transform.position, .05f);
        }

        //Vector3 cross = crossProduct(end, new Vector3(pt.point.x, pt.point.y) - transform.position);

        //print("cross: " + cross);
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawLine(new Vector3(), cross);



        // Contact point
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(pt.point, .05f);


        //Gizmos.color = Color.green;

        //float rads = Mathf.Deg2Rad * transform.localEulerAngles.z;

        ////Vector2 to = new Vector2(Mathf.Cos(rads), Mathf.Sin(rads));
        //Gizmos.DrawLine(pt.point, pt.point + to * transform.localScale.x);
        //print(transform.localEulerAngles);
        //print(Mathf.Deg2Rad);
    }
}
