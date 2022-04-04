using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour {

    public GameObject pfStamp;
    public AudioSource audio;
    public AudioClip stamp1;
    public AudioClip stamp2;


    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            if(Random.Range(0, 2)==0)
            audio.clip = stamp1;
            else
            audio.clip = stamp2;
            audio.Play();

            ContactFilter2D cf2d = new ContactFilter2D();
            cf2d.useTriggers = true;
            cf2d.layerMask = 1 << 8 | 1 << 9;
            cf2d.useLayerMask = true;

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

            foremost.GetComponent<Stampable>().Stamp(pfStamp, new Vector3 (transform.position.x,transform.position.y-50.0f,transform.position.z), transform.rotation);
        }
    }

}