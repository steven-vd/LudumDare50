using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour {

    public GameObject pfStamp;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            ContactFilter2D cf2d = new ContactFilter2D();
            cf2d.useTriggers = true;
            cf2d.layerMask = 1 << 8 | 1 << 9;

            List<Collider2D> forms = new List<Collider2D>();
            if (GetComponent<Collider2D>().OverlapCollider(cf2d, forms) == 0) {
                // No colliders
                return;
            }

            Transform foremost = forms[0].transform;
            foreach (Collider2D formCollider in forms) {
                if (formCollider.transform.position.z < foremost.position.z) {
                    foremost = formCollider.transform;
                }
            }
            foremost.GetComponent<Stampable>().Stamp(pfStamp, transform.position, transform.rotation);
        }
    }

}