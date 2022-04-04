using System.Collections.Generic;
using UnityEngine;

public class Stapler : MonoBehaviour {

    public GameObject pfStaple;
    public AudioSource audio;
    public AudioClip stamp1;
    public AudioClip stamp2;
    public AudioClip stamp3;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            if(Random.Range(0, 3)==0)
            audio.clip = stamp1;
            else if(Random.Range(0, 2)==0)
            audio.clip = stamp2;
            else
            audio.clip = stamp3;
            audio.Play();



            ContactFilter2D cf2d = new ContactFilter2D();
            cf2d.useTriggers = true;
            cf2d.layerMask = 1 << 8;
            cf2d.useLayerMask = true;

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
            foremost.GetComponent<Stapleable>().Staple(pfStaple, transform.position, transform.rotation);
        }
    }

}
