using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZByY : MonoBehaviour {

    public float offset = 0;
    public float factor = 1.0f;

    private void Start() {
        UpdateZ();
    }

    public void UpdateZ() {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.y * factor + offset
        );
    }

}
