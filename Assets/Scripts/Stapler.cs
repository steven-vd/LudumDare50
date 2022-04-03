using System.Collections.Generic;
using UnityEngine;

public class Stapler : Stamp {

    protected override void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            ContactFilter2D cf2d = new ContactFilter2D();
            cf2d.useTriggers = true;
            cf2d.layerMask = 1 << 8;

            List<Collider2D> forms = new List<Collider2D>();
            if (GetComponent<Collider2D>().OverlapCollider(cf2d, forms) == 0) {
                // No colliders
                return;
            }

            List<Stapleable> stapleables = new List<Stapleable>();
            for (int i = 0; i < forms.Count; ++i) {
                Stapleable stapleable = forms[i].GetComponent<Stapleable>();
                if (stapleable != null) {
                    stapleables.Add(stapleable);
                }
            }

            Transform foremost = forms[0].transform;
            foreach (Stapleable stapleable in stapleables) {
                stapleable.Link(stapleables);

                if (stapleable.transform.position.z < foremost.position.z) {
                    foremost = stapleable.transform;
                }
            }
            foremost.GetComponent<Stapleable>().Stamp(pfStamp, transform.position, transform.rotation);
        }
    }

}
