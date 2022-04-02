using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {

    public static List<LayerManager> layerManagers = new List<LayerManager>();

    public GameObject[] layers;

    public static void UpdateSortOrder() {
        int sortOrder = 0;
        foreach (LayerManager lm in layerManagers) {
            foreach (GameObject l in lm.layers) {
                Renderer r = l.GetComponent<Renderer>();
                if (r != null) {
                    r.sortingOrder = sortOrder;
                } else {
                    Canvas c = l.GetComponent<Canvas>();
                    if (c != null) {
                        c.sortingOrder = sortOrder;
                    } else {
                        Debug.LogError($"No renderer or canvas on '{l.name}'");
                    }
                }
            }
            sortOrder++;
        }
    }

    public void ToFront() {
        layerManagers.Remove(this);
        layerManagers.Add(this);
        UpdateSortOrder();
    }

    private void Awake() {
        layerManagers.Add(this);
        UpdateSortOrder();
    }

    private void OnDestroy() {
        layerManagers.Remove(this);
        UpdateSortOrder();
    }

}
