using System;
using UnityEngine;

namespace helper
{
    public static class MeshHelper
    {
        public static void GenMeshData(int v_width, int v_height, out Vector3[] vertices, out Vector2[] uvs, out Vector3[] normals)
        {
            int V_SIZE = v_width * v_height;
            vertices = new Vector3[V_SIZE];
            uvs = new Vector2[V_SIZE];
            normals = new Vector3[V_SIZE];

            for (int y = 0; y < v_height; y++)
            {
                for (int x = 0; x < v_width; x++)
                {
                    vertices[x + y * v_width] = new Vector3(x, y, 0);
                    uvs[x + y * v_width] = new Vector2(x, y);
                    normals[x + y * v_width] = -Vector3.forward;
                }
            }
        }
    }
}
