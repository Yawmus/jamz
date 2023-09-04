using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static helper.MeshHelper;

public class NoiseDebug : MonoBehaviour
{

    public Texture2D noise;
    
   
    Mesh mesh;
    int width = 1, height = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3[] newVertices, normals;
        //Vector2[] newUV;

        //int VERT_W = 2, VERT_H = 2;
        //int UNIT_W = VERT_W - 1, UNIT_H = VERT_H - 1;

        //GenMeshData(VERT_W, VERT_H, out newVertices, out newUV, out normals);


        //int[] tris = new int[6];
        //int triIdx = 0;


        //// Perspective is defining 2 tris per Unity "unit"
        //for (int y = 0; y < UNIT_H; y++)
        //{
        //    for (int x = 0; x < UNIT_W; x++)
        //    {
        //        int lineSize = VERT_W;
        //        int t = x + y * lineSize;

        //        int bl = t, tl = t + lineSize, tr = t + lineSize + 1, br = t + 1;

        //        tris[triIdx++] = bl;
        //        tris[triIdx++] = tl;
        //        tris[triIdx++] = tr;

        //        tris[triIdx++] = tr;
        //        tris[triIdx++] = br;
        //        tris[triIdx++] = bl;
        //    }
        //}


        //mesh = new Mesh();

        //mesh.vertices = newVertices;
        //mesh.normals = normals;
        //mesh.uv = newUV;
        //mesh.triangles = tris;
        //GetComponent<MeshFilter>().mesh = mesh;
    }

    private void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
            float border = 10;

            int mip = GameObject.Find("ground").GetComponent<MeshGen>().mipLevel;
            Color32[] pixels = noise.GetPixels32(mip);

            Graphics.
            Graphics.DrawTexture(new Rect(Camera.main.pixelWidth - 200, 0, 100, 100), noise);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        print(Camera.main.pixelWidth);
        print(Camera.main.rect.right);
        Rect rect = new Rect(300, 0, 100, 100);
        UnityEditor.Handles.BeginGUI();
        //UnityEditor.Handles.DrawSolidRectangleWithOutline(rect, Color.black, Color.white);
        UnityEditor.Handles.EndGUI();
    }
}
