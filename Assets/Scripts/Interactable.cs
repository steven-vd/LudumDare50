using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DragnDrop))]
[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour {

    public UnityEvent OnInteract;

    private void OnMouseOver() {
        DragnDrop dragnDrop = GetComponent<DragnDrop>();
        if (dragnDrop._currentlyDragging && Input.GetMouseButtonDown(1)) {
            OnInteract.Invoke();
        }
    }

}
