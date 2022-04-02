using UnityEngine;

public class CameraPanByCursor : MonoBehaviour {

    public float minY = 540.0f, maxY;
    public float edgePanDistance;
    public float panSpeed = 1;

    [HideInInspector]
    public float desiredY;

    private void Awake() {
        desiredY = minY;
    }

    void Update() {
        if (Input.mousePosition.y > Screen.height - edgePanDistance) {
            desiredY = maxY;
        } else if (Input.mousePosition.y < edgePanDistance) {
            desiredY = minY;
        }

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(transform.position.y, desiredY, panSpeed * Time.deltaTime),
            transform.position.z
        );
    }
}
