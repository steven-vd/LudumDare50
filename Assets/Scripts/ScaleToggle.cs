using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ScaleToggle : MonoBehaviour {

    public Vector3 initialScale, alternativeScale;
    [HideInInspector]
    public bool alt = false;

    public void Toggle() {
        if (alt) {
            transform.localScale = initialScale;
            //No need to reset Z bc SetZByY
        } else {
            transform.localScale = alternativeScale;
            transform.parent.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z - 100
            );
        }
        alt = !alt;
    }

}
