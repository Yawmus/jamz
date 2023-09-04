using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lawn : MonoBehaviour
{
    ComputeShader meshGen;
    ComputeBuffer resultBuffer;

    public int amount = 17;

    int kernel;
    uint threadGroupSize;

    Vector3[] output;


    // Start is called before the first frame update
    void Start()
    {
        kernel = meshGen.FindKernel("Grass");
        meshGen.GetKernelThreadGroupSizes(kernel, out threadGroupSize, out _, out _);
        print(threadGroupSize);

        resultBuffer = new ComputeBuffer(amount, sizeof(float) * 3);
        output = new Vector3[amount];

    }

    // Update is called once per frame
    void Update()
    {
        meshGen.SetBuffer(kernel, "Result", resultBuffer);
        int threadGroups = (int)((amount + (threadGroupSize - 1)) / threadGroupSize);
        meshGen.Dispatch(kernel, threadGroups, 1, 1);
        resultBuffer.GetData(output);

    }


    void OnDestroy()
    {
        resultBuffer.Dispose();
    }
}
