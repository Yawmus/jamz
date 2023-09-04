using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    GameObject go;
    public TextAsset textFile; 

    // Start is called before the first frame update
    void Start()
    {
        string text = textFile.text;
        Debug.Log(text);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
