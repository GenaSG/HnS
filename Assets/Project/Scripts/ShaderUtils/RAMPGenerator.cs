using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAMPGenerator : MonoBehaviour
{
    private enum Algorithm
    {
        Sigmoid,
        SmoothStep,
        Step,
    }
    [SerializeField]
    private Algorithm algorithm;
    [SerializeField]
    private int rampSteps = 100;
    [SerializeField]
    private float sigmoidExp = 9f;
    [SerializeField]
    private float sigmoidOffset = -0.5f;


    [ContextMenu("Generate RAMP HLSL array")]
    private void GenerateRamp()
    {
        float[] ramp = new float[rampSteps];
        for(int i = 0; i < rampSteps; i++)
        {
            if(algorithm == Algorithm.Sigmoid)
            {
                ramp[i] = Sigmoid((float)i / (float)rampSteps, sigmoidExp);
            }
            else if (algorithm == Algorithm.SmoothStep)
            {
                ramp[i] = Mathf.SmoothStep(0f, 1f, (float)i / (float)rampSteps);
            }
            else if(algorithm == Algorithm.Step)
            {
                ramp[i] = (float)i / (float)rampSteps > 0.5f ? 1f : 0f;
            }

            
        }
        Debug.Log(String.Join(",",ramp));
    }

    private float Sigmoid(float factor,float exp)
    {
        return 1 / (1 + Mathf.Exp(-(factor - sigmoidOffset) * exp));
    }
}
