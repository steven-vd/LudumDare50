using UnityEngine;

public class Stamp : MonoBehaviour {

    public GameObject pfStamp;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            print("A");
        }
    }

}