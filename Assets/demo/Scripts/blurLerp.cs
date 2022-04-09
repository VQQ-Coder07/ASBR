using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class blurLerp : MonoBehaviour
{
    private PostProcessVolume pp()
    {
        return this.gameObject.GetComponent<PostProcessVolume>();
    }

    public void BlurIn()
    {
        if(pp().profile.TryGetSettings<DepthOfField>(out var dreph))
        {
            dreph.focalLength.value = 140f;
        }
    }
    public void BlurOut()
    {
        if(pp().profile.TryGetSettings<DepthOfField>(out var dreph))
        {
            dreph.focalLength.value = 0f;
        }
    }
}
