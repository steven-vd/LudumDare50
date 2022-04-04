using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CameraFade : MonoBehaviour {

    public static float _desiredAlpha;
    public static float _speed;
    public static Image _image;

    public static bool Halted = false;

    private void Awake() {
        _image = GetComponent<Image>();
        _desiredAlpha = _image.color.a;
        Halted = false;
    }

    public static void FadeIn(float seconds) {
        _speed = 1.0f / seconds;
        _desiredAlpha = 0.0f;
        _image.color = new Color(//If you're being redirected here by an error, remember to use the CameraFade prefab in your scene
            _image.color.r,
            _image.color.g,
            _image.color.b,
            1
        );
    }

    public static void FadeOut(float seconds) {
        _speed = 1.0f / seconds;
        _desiredAlpha = 1.0f;
        _image.color = new Color(
            _image.color.r,
            _image.color.g,
            _image.color.b,
            0
        );
    }

    private void Update() {
        if (Halted) {
            return;
        }

        _image.color = new Color(
            _image.color.r,
            _image.color.g,
            _image.color.b,
            _image.color.a + _speed * Time.deltaTime * (_desiredAlpha > _image.color.a ? 1 : -1)
        );
    }

}
