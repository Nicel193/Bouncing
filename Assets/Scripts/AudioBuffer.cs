using System.Collections.Generic;
using UnityEngine;

public class AudioBuffer
{
    private List<float> buffer;
    private int bufferSize;
    
    public AudioBuffer(int sampleRate, float duration)
    {
        bufferSize = (int)(sampleRate * duration);
        buffer = new List<float>(bufferSize);
    }

    public void AddSample(float sample)
    {
        buffer.Add(sample);
        if (buffer.Count > bufferSize)
        {
            buffer.RemoveAt(0);
        }
    }

    public float[] GetSamples(int startIndex = 0)
    {
        if (startIndex < 0)
        {
            startIndex = 0;
        }
        int count = buffer.Count - startIndex;
        float[] samples = new float[count];
        buffer.CopyTo(startIndex, samples, 0, count);
        return samples;
    }
}