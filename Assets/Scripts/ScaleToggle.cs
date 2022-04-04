using UnityEngine;

public class ScaleToggle : MonoBehaviour {

    public Vector3 initialScale, alternativeScale;
    [HideInInspector]
    public bool alt = false;

    public void Toggle() {
        if (alt) {
            transform.localScale = initialScale;
        } else {
            transform.localScale = alternativeScale;
        }
        alt = !alt;
    }

}
