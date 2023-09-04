using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshGenEditor : EditorWindow
{
    public string meshName;
    GameObject meshObj = null;
    int width = 20, height = 20, freq = 1;

    [MenuItem("Window/MeshGenerator")]
    public static void ShowWindow() {
        GetWindow<MeshGenEditor>("Mesh gen settings");
	}

    void OnGUI()
    {
        meshName = EditorGUILayout.TextField("Name", meshName);
        meshObj = GameObject.Find(meshName);


        if (meshObj != null) {

            bool showTris, showNormals;

            GUILayout.Label("Width", EditorStyles.label);
            width = EditorGUILayout.IntSlider(width, 1, 100);

            GUILayout.Label("Height", EditorStyles.label);
            height = EditorGUILayout.IntSlider(height, 1, 100);

            GUILayout.Label("Frequency", EditorStyles.label);
            freq = EditorGUILayout.IntSlider(freq, 1, 10);

            if (GUILayout.Button("Apply")) {
                meshObj.GetComponent<MeshGen>().Apply(width, height, freq);
            }
        }
    }
}
