using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    GameObject[,] map;
    List<GameObject> ropes;
    GameObject p;
    public GameObject playerPrefab;
    public GameObject tilePrefab;
    public GameObject ropePrefab;


    // Start is called before the first frame update
    void Start()
    {
        var file = Resources.Load<TextAsset>("Maps/1");


        int height = 1, width = 0, temp = 0;
        foreach (char c in file.text)
        {
            if (c == '\n' || c == '\r')
            {
                if (temp > width)
                {
                    width = temp;
                }
                height++;
                temp = 0;
                continue;
            }
            temp++;
        }

        map = new GameObject[width, height];
        ropes = new List<GameObject>();

        int x = 0, y = height - 1;
        foreach (char c in file.text)
        {
            if (c == '\n' || c == '\r')
            {
                x = 0;
                y--;
                continue;
            }

            GameObject template = null;

            switch (c)
            {
                case '#':
                case '`':
                    template = tilePrefab;
                    break;
                case 'X':
                    template = playerPrefab;
                    break;
                case 'R': // What do I do here? In the original it was a single line that moved on some arc. Maybe I could use a Debug line?
                    template = ropePrefab;
                    break;
            }

            if (template != null)
            {
                GameObject clone = Instantiate(template, new Vector2(x, y), Quaternion.identity);


                switch (c)
                {
                    case '#':
                        clone.layer = LayerMask.NameToLayer("Ground");
                        clone.transform.parent = this.gameObject.transform.Find("tiles");
                        map[x, y] = clone;
                        break;
                    case '`':
                        clone.transform.parent = this.gameObject.transform.Find("tiles");
                        map[x, y] = clone;
                        break;
                    case 'X':
                        clone.transform.parent = this.gameObject.transform;
                        map[x, y] = clone;
                        p = map[x, y];
                        break;
                    case 'R':
                        clone.transform.parent = this.gameObject.transform;
                        map[x, y] = clone;
                        ropes.Add(map[x, y]);
                        break;

                }

            }
            x++;
        }



    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, -10);


    }

    private void OnDrawGizmos()
    {
  //      if (ropes == null) {
  //          return;
		//}
            //Gizmos.color = Color.red;
            //Gizmos.DrawBox(rope.transform.position, rope.transform.position + new Vector3(0, -10, 0));

  //      foreach(GameObject rope in ropes) { 
  //          Gizmos.color = Color.white;
  //          Gizmos.DrawLine(rope.transform.position + Vector3.up * 1, rope.transform.position + Vector3.up * -3);
		//}
        
    }
}