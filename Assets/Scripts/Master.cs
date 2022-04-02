using UnityEngine;

public class Master : MonoBehaviour {

    public static Master Instance;

    public Camera MainCamera;

    private void Awake() {
        Instance = this;
    }

}
