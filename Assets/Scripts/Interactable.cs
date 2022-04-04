using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour {

    public UnityEvent OnInteract;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            OnInteract.Invoke();
        }
    }

}
