using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class HexSnap : MonoBehaviour {

    public Vector3 offset;
    public Hex hex {
        get {
            return Hex.FromWorld(transform.position);
        }
    }

    public Hex localHex {
        get {
            return Hex.FromWorld(transform.localPosition);
        }
    }

    public void ApplyTransform() {
        Vector3 newPos = this.localHex.ToWorld(0f) + offset;
        transform.localPosition = newPos;
    }

    //#if UNITY_EDITOR
    void Start() {
        //if (!Application.isPlaying) {
            ApplyTransform();
        //}
    }

    void OnDrawGizmosSelected() {
        //UnityEditor.Handles.Label(transform.position, hex.ToString());
    }
    //#endif
}
