using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public ComputeShader Shader;
    int kernel;
    uint threadGroupSize;
    Vector3[] output;
    ComputeBuffer resultBuffer;

    public int SphereAmount = 10;


    // Start is called before the first frame update
    void Start()
    {
        //program we're executing
        kernel = Shader.FindKernel("FillWithRed");
        Shader.GetKernelThreadGroupSizes(kernel, out threadGroupSize, out _, out _);

        //buffer on the gpu in the ram
        resultBuffer = new ComputeBuffer(SphereAmount, sizeof(float) * 3);
        output = new Vector3[SphereAmount];

        print(output[0]);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetBuffer(kernel, "Result", resultBuffer);
        int threadGroups = (int)((SphereAmount + (threadGroupSize - 1)) / threadGroupSize);
        Shader.Dispatch(kernel, threadGroups, 1, 1);
        resultBuffer.GetData(output);

        print(output);
    }

    private void OnDestroy()
    {
        resultBuffer.Dispose();
    }
}
