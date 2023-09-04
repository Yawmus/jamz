using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int WIDTH = 15, HEIGHT = 31;

    Vector3 player = new Vector3(1, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 pivot = new Vector3(-.5f, -.5f, 0);

        // Backdrop
        Gizmos.color = new Color(.2f, .2f, .3f);
        Gizmos.DrawCube(new Vector3(WIDTH / 2f, HEIGHT / 2f) + pivot, new Vector3(WIDTH, HEIGHT, 0));

        // Objects
        Gizmos.color = new Color(.9f, .1f, .1f);
        Gizmos.DrawCube(player, new Vector3(1, 1, 0));

        // Draw bounds
        Gizmos.color = new Color(.8f, .8f, .8f);
        for (int x = 0; x < WIDTH + 1; x++)
        {
            Gizmos.DrawLine(new Vector3(x, 0) + pivot, new Vector3(x, HEIGHT) + pivot);
        }

        for (int y = 0; y < HEIGHT + 1; y++)
        {
            Gizmos.DrawLine(new Vector3(0, y) + pivot, new Vector3(WIDTH, y) + pivot);
        }

    }
}
