using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FaceCamera : MonoBehaviour
{
    ConstraintSource cam;
    void Start()
    {
        cam.sourceTransform = Camera.main.transform;
        cam.weight = 1;
        GetComponent<LookAtConstraint>().AddSource(cam);
    }
}
