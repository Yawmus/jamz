using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static helper.MeshHelper;

public class MeshGen : MonoBehaviour
{
    Vector3[] newVertices;
    Vector2[] newUV;
    Mesh mesh;
    public Texture2D noise;
    int UNIT_WIDTH, UNIT_HEIGHT;


    const float HEIGHT_SCALE = 25f;

    [Range(0, 5)]
    public int mipLevel;


    public void Apply(int unitWidth, int unitHeight, int frequency) {
        UNIT_WIDTH = unitWidth;
        UNIT_HEIGHT = unitHeight;
        int VERT_WIDTH = unitWidth + 1;
        int VERT_HEIGHT = unitHeight + 1;


        // Original tex is 256 * 256 (or Mip 0)
        // At Mip 2, it's halfed twice, so 64 * 64
        Color32[] pixels = noise.GetPixels32(0);
        float[] noiseData = new float[pixels.Length];

        //float noiseMin, noiseMax;

        int i = 0;
        foreach (Color32 pixel in pixels)
        {

            noiseData[i] = (pixel.r / 255f);
            //print(noiseData[i]);
            i++;
        }




        int V_SIZE = VERT_WIDTH * VERT_HEIGHT;

        Vector3[] newVertices, normals;
        Vector2[] newUV;

        GenMeshData(VERT_WIDTH, VERT_HEIGHT, out newVertices, out newUV, out normals );

        for (int y = 0; y < VERT_HEIGHT; y++)
        {
            for (int x = 0; x < VERT_WIDTH; x++)
            {
                float height = noiseData[x + y * VERT_WIDTH];
                newVertices[x + y * VERT_WIDTH] = new Vector3(x, y, height);
            }
        }



        int[] dirtTris = new int[(UNIT_WIDTH * UNIT_HEIGHT * 2) * 3];
        int[] grassTris = new int[(UNIT_WIDTH * UNIT_HEIGHT * 2) * 3];
        int[] stoneTris = new int[(UNIT_WIDTH * UNIT_HEIGHT * 2) * 3];

        int stoneTriIdx = 0, dirtTriIdx = 0, grassTriIdx = 0;

        // Defining what type of triangle we're working with.
        // Simple approach -- mean of vertice height positions

        // Might make more sense to "iterate" through all possible triangles, then assign
        // what is found with a particular mat type

        // I think the idea is to "collect" the 3 verts, do the math, then decide the sub-mesh type

        for (int y = 0; y < UNIT_HEIGHT; y++)
        {
            for (int x = 0; x < UNIT_WIDTH; x++)
            {
                int lineSize = UNIT_WIDTH + 1;
                int t = x + y * lineSize;

                int bl = t, tl = t + lineSize, tr = t + lineSize + 1, br = t + 1;

                // Triangle position 1 (anchored at top-left)
                float meanHeight = (newVertices[bl].z + newVertices[tl].z + newVertices[tr].z) / 3f;
                //print(meanHeight);

                if (meanHeight > .6f)
                {
                    stoneTris[stoneTriIdx++] = bl;
                    stoneTris[stoneTriIdx++] = tl;
                    stoneTris[stoneTriIdx++] = tr;
                }
                else if (meanHeight > .4f)
                {

                    dirtTris[dirtTriIdx++] = bl;
                    dirtTris[dirtTriIdx++] = tl;
                    dirtTris[dirtTriIdx++] = tr;
                }
                else
                {

                    grassTris[grassTriIdx++] = bl;
                    grassTris[grassTriIdx++] = tl;
                    grassTris[grassTriIdx++] = tr;
                }

                // Triangle position 2 (anchored at bottom-right)
                meanHeight = (newVertices[tr].z + newVertices[br].z + newVertices[bl].z) / 3f;

                if (meanHeight > .6f)
                {
                    stoneTris[stoneTriIdx++] = tr;
                    stoneTris[stoneTriIdx++] = br;
                    stoneTris[stoneTriIdx++] = bl;
                }
                else if (meanHeight > .4f)
                {

                    dirtTris[dirtTriIdx++] = tr;
                    dirtTris[dirtTriIdx++] = br;
                    dirtTris[dirtTriIdx++] = bl;
                }
                else
                {

                    grassTris[grassTriIdx++] = tr;
                    grassTris[grassTriIdx++] = br;
                    grassTris[grassTriIdx++] = bl;
                }

            }
        }

        for (int k = 0; k < newVertices.Length; k++)
        {
            Vector3 vert = newVertices[k];
            newVertices[k] = new Vector3(vert.x, vert.y, vert.z * HEIGHT_SCALE);
        }

        Destroy(mesh);
        mesh = new Mesh();
        mesh.subMeshCount = 3;

        mesh.vertices = newVertices;
        mesh.normals = normals;
        //mesh.RecalculateNormals();
        mesh.uv = newUV;
        mesh.SetTriangles(grassTris, 0);
        mesh.SetTriangles(dirtTris, 1);
        mesh.SetTriangles(stoneTris, 2);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        Apply(13, 13, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float mod = .01f;
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        for (var i = 0; i < vertices.Length; i++)
        {
            //vertices[i] += normals[i] * Mathf.Sin(Time.time);
        }


        //vertices[0] += Vector3.forward * Mathf.Sin(Time.time) * mod;
        //print(Mathf.Sin(Time.time));

        mesh.vertices = vertices;

    }

    void OnDrawGizmos()
    {
        //return;

        if (mesh == null) {
            return;
        }

        // Transform from Object space to World space
        Vector3[] transVerts = new Vector3[mesh.vertices.Length];
        transform.TransformPoints(mesh.vertices, transVerts);

        Vector3[] transNorms = new Vector3[mesh.normals.Length];
        transform.TransformDirections(mesh.normals, transNorms);

        //Draw triangles
        Gizmos.color = Color.green;
        foreach (Vector3 vert in transVerts)
        {
            Gizmos.DrawSphere(vert, .1f);
            Gizmos.color = Color.red;
        }

        Gizmos.color = Color.blue;


        // Currently drawing each triangle gizmo seperately, but could get an aggregated list
        // of all triangles in relation to one-another. This would prob require hammering down
        // the orientation of how the tris are drawn to avoid the lines being all wacky.

        //List<Vector3> drawVerts = new List<Vector3>();

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            int a = mesh.triangles[i];
            int b = mesh.triangles[i + 1];
            int c = mesh.triangles[i + 2];

            Span<Vector3> pts = new Vector3[3] {
                transVerts[a],
                transVerts[b],
                transVerts[c]
            };


   //         drawVerts.AddRange(new Vector3[4]{
   //          transVerts[a],
   //          transVerts[b],
   //          transVerts[c],
   //          transVerts[a]
			//}
			//)
            Gizmos.DrawLineStrip(pts, true);
            //Gizmos.DrawLine(transVerts[b], transVerts[c]);
            //Gizmos.DrawLine(transVerts[c], transVerts[a]);



        }

        //Draw normals
        for (int i = 0; i < transVerts.Length; i++)
        {
            Vector3 vert = transVerts[i];
            Vector3 norm = transNorms[i];

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(vert, .1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(vert, vert + norm);

        }
    }

}
