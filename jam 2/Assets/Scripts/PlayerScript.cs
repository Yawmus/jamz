using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum Action
    {
        JUMP, IDLE, SWINGING
    };

    public Action action = Action.IDLE;
    Rigidbody2D rb;
    BoxCollider2D bc;
    Sprite s;
    public Rope rope;

    public LayerMask Ground;

    const float MAX_IMPUSE = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        s = GetComponent<Sprite>();
    }

    float h = 0, v = 0;
    float impulse = 0;

    // Update is called once per frame
    void Update()
    {
        float hSpeed = 10f, vSpeed = 10f;
        h = v = 0;

        switch (action)
        {
            case Action.IDLE:
                h = hSpeed * Input.GetAxis("Horizontal");

                if (Input.GetButtonDown("Jump") && rb.IsTouchingLayers(Ground))
                {
                    action = Action.JUMP;
                    impulse = MAX_IMPUSE;
                }
                break;

            case Action.SWINGING:
                if (Input.GetButtonDown("Jump"))
                {
                    action = Action.JUMP;
                    Destroy(rope.anchor);
                }
        
                break;
		}
    }

    private void FixedUpdate()
    {
        //rb.AddForce(transform.up * v + transform.right * h);
        switch (action) {
            case Action.JUMP:
                rb.AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);
                action = Action.IDLE;
                impulse = 0;
                break;

            case Action.IDLE:
                if (rb.IsTouchingLayers(Ground)) {
                    rb.velocity = new Vector2(h, rb.velocity.y);
                }
                break;

            case Action.SWINGING:
                //rb.MovePosition(rope.anchor.transform.position + Vector3.left * .5f + Vector3.down * .5f);
                break;
		}
    }

    ContactPoint2D[] contacts;


    void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.gameObject.name);


        // check if "other" is a wall
            //EditorApplication.isPaused = true;
            
            //print(collision.collider.)
            //print("Collided");
            //print(collision.contacts.Length);

        //print(collider.includeLayers);
                //print("point " + i + ": " + collision.contacts[i].point);


                //print("Collision below? " + (this.transform.position.y > collider.bounds.center.y));
                //print("Under player " + (this.transform.position.y - (bc.size.y / 2)));
                //print("Colllision point " + i + " " + collision.contacts[i].point);
                //print("Other player " + bc.IsTouching(collider));

                // Is the collision below?
                //if (this.transform.position.y > collider.bounds.center.y
                //&& this.transform.position.y - (bc.size.y / 2) < collision.contacts[i].point.y) {
                //print("below!");
                //}
                //           if( ) { 
                //}

                //i++;
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(transform.position, .5f);

        if (contacts != null)
        {
            Gizmos.color = Color.red;
            int i = 0;
            foreach (ContactPoint2D pt in contacts)
            {
                Gizmos.DrawSphere(pt.point, .2f);
            }
        }

        if (rope && rope.anchor) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, rope.anchor.transform.position);
		}
    }

    void OnCollision2DStay(Collision collisionInfo)
    {
    }
}
