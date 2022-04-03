using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SetZByY))]
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
        if (transform.GetChild(0).transform is RectTransform) {
            return transform.position.y + (transform.GetChild(0).transform as RectTransform).rect.height * transform.GetChild(0).lossyScale.y / 2 <
            (Surface.transform as RectTransform).anchoredPosition.y + (Surface.transform as RectTransform).rect.height * Surface.transform.lossyScale.y / 2;
        } else {
            return transform.position.y + transform.GetChild(0).lossyScale.y / 2 <
            (Surface.transform as RectTransform).anchoredPosition.y + (Surface.transform as RectTransform).rect.height * Surface.transform.lossyScale.y / 2;
        }
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
        GetComponent<SetZByY>().UpdateZ();

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
