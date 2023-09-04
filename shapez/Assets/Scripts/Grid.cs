using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 aStart, aEnd;
    public Vector2 bStart, bEnd;
    public readonly Vector2 cStart, cEnd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public readonly int GRID_SIZE = 10;

    private void OnDrawGizmos()
    {
        for(int x = -(GRID_SIZE / 2); x <= GRID_SIZE / 2; x++) {
            Gizmos.DrawLine(new Vector3(x, -(GRID_SIZE / 2)), new Vector3(x, GRID_SIZE / 2));
		}
        for(int y = -(GRID_SIZE / 2); y <= GRID_SIZE / 2; y++) { 
            Gizmos.DrawLine(new Vector3(-(GRID_SIZE / 2), y), new Vector3(GRID_SIZE / 2, y));
		}

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(0, 0), .1f);


        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(aStart, .1f);
        Gizmos.DrawLine(aStart, aEnd);
        Gizmos.DrawWireSphere(aEnd, .1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(bStart, .1f);
        Gizmos.DrawLine(bStart, bEnd);
        Gizmos.DrawWireSphere(bEnd, .1f);


        // Calc
        //float rads = Mathf.Deg2Rad * transform.localEulerAngles.z;

        //Vector2 to = new Vector2(Mathf.Cos(rads), Mathf.Sin(rads));
        //Gizmos.DrawLine(pt.point, pt.point + to * transform.localScale.x);
        //print(transform.localEulerAngles);
        //print(Mathf.Deg2Rad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(cStart, .1f);
        Gizmos.DrawLine(cStart, cEnd);
        Gizmos.DrawWireSphere(cEnd, .1f);

    }
}