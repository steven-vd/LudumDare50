using UnityEngine;

public class DragnDrop : MonoBehaviour {

    [Tooltip("Defaults to gameobject with name 'Desk'")]
    public GameObject Surface;
    public float returnToLastLegalPositionSpeed = 3;

    [HideInInspector]
    public Vector2 mousePosDeltaOnInitiateDrag = Vector2.zero;
    [HideInInspector]
    public Vector3 lastLegalPosition = Vector3.zero;

    private void Awake() {
        if (Surface == null) {
            Surface = GameObject.Find("Desk");
        }
    }

    public bool IsInLegalPosition() {
        return transform.position.y + transform.lossyScale.y / 2 < Surface.transform.position.y + Surface.transform.lossyScale.y / 2;
    }

    private void Update() {
        if (!Input.GetMouseButton(0) && !IsInLegalPosition()) {
            transform.position = Vector3.Lerp(transform.position, lastLegalPosition, returnToLastLegalPositionSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDown() {
        mousePosDeltaOnInitiateDrag = transform.position - Master.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag() {
        Vector2 mousePos = Master.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(
            mousePos.x + mousePosDeltaOnInitiateDrag.x,
            mousePos.y + mousePosDeltaOnInitiateDrag.y,
            transform.position.z
        );

        //Check if still on surface
        if (IsInLegalPosition()) {
            lastLegalPosition = transform.position;
        }
    }

}
