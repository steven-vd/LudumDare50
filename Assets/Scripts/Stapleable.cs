using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(DragnDrop))]
public class Stapleable : Stampable {

    [HideInInspector]
    public List<Stapleable> linked = new List<Stapleable>();

    public override void Stamp(GameObject pfStamp, Vector2 absolutePosition, Quaternion rotation) {
        GameObject.Instantiate(pfStamp, new Vector3(absolutePosition.x, absolutePosition.y, transform.position.z), rotation, tfStampImageParent);
    }

    public void Link(List<Stapleable> stapleables) {
        linked.AddRange(stapleables);
        linked.Remove(this);
    }

    public void GetAllLinked(List<Stapleable> allLinked) {
        foreach (Stapleable l in linked) {
            if (!allLinked.Contains(l)) {
                allLinked.Add(l);
                l.GetAllLinked(allLinked);
            }
        }
    }

}
