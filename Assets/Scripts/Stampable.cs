using UnityEngine;

public class Stampable : MonoBehaviour {

    public Transform tfStampImageParent;
    public float scaleMod = 1.0f;

    public virtual void Stamp(GameObject pfStamp, Vector2 absolutePosition, Quaternion rotation) {
        GameObject stamp = GameObject.Instantiate(pfStamp, new Vector3(absolutePosition.x, absolutePosition.y, transform.position.z), rotation, tfStampImageParent);
        stamp.transform.localScale *= scaleMod;
    }

}
