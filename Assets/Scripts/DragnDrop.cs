using System.Collections.Generic;
using UnityEngine;

public class DragnDrop : MonoBehaviour {

    [Tooltip("Defaults to gameobject with name 'Desk'")]
    public GameObject Surface;
    public float returnToLastLegalPositionSpeed = 3;

    [HideInInspector]
    public Vector2 mousePosDeltaOnInitiateDrag = Vector2.zero;
    [HideInInspector]
    public Vector3 lastLegalPosition = Vector3.zero;
    [HideInInspector]
    public DragnDrop CurrentlyDraggedObject;

    private void Awake() {
        if (Surface == null) {
            Surface = GameObject.Find("Desk");
        }
    }

    //FIXME
    public bool IsInLegalPosition(bool checkRecursive = true) {
        bool inLegalPosition = true;
        if (transform.GetChild(0).transform is RectTransform) {
            inLegalPosition = transform.position.y + (transform.GetChild(0).transform as RectTransform).rect.height * transform.GetChild(0).lossyScale.y / 2 <
            (Surface.transform as RectTransform).anchoredPosition.y + (Surface.transform as RectTransform).rect.height * Surface.transform.lossyScale.y / 2;
        } else {
            inLegalPosition = transform.position.y + transform.GetChild(0).lossyScale.y / 2 <
            (Surface.transform as RectTransform).anchoredPosition.y + (Surface.transform as RectTransform).rect.height * Surface.transform.lossyScale.y / 2;
        }
        if (checkRecursive) {
            Stapleable stapleable = GetComponent<Stapleable>();
            if (stapleable != null) {
                List<Stapleable> allLinked = new List<Stapleable>();
                stapleable.GetAllLinked(allLinked);
                foreach (Stapleable s in allLinked) {
                    inLegalPosition &= s.GetComponent<DragnDrop>().IsInLegalPosition(false);
                }
            }
        }
        return inLegalPosition;
    }

    private void Update() {
        if (!IsInLegalPosition()) {
            //DONT_FIXME all forms are connected here. ACTUALLY it's a feature
            if (!Input.GetMouseButton(0)) {
                transform.position = Vector3.Lerp(transform.position, lastLegalPosition, returnToLastLegalPositionSpeed * Time.deltaTime);
            }
            if (CurrentlyDraggedObject == this && !TransactionHandler.InBetweenTransactions() && Input.GetMouseButtonUp(0)) {
                Form form = GetComponent<Form>();
                if (form != null && form.IsReturnable()) {
                    Citizen_Logic.Instance.WalkAway(!form.IsAccepted());
                    //Destroy stapled forms
                    List<Stapleable> allLinkedStapleables = new List<Stapleable>();
                    GetComponent<Stapleable>().GetAllLinked(allLinkedStapleables);
                    foreach (Stapleable s in allLinkedStapleables) {
                        Destroy(s.gameObject);
                    }
                }
            }
        }
    }

    public void BeginDrag() {
        mousePosDeltaOnInitiateDrag = transform.position - Master.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        CurrentlyDraggedObject = this;
        CurrentlyDraggedObject.transform.eulerAngles = new Vector3(
            CurrentlyDraggedObject.transform.eulerAngles.x,
            CurrentlyDraggedObject.transform.eulerAngles.y,
            CurrentlyDraggedObject.transform.eulerAngles.z - 5
        );
        GetComponentInChildren<ScaleToggle>()?.Toggle();
    }

    private void OnMouseDown() {
        BeginDrag();

        Stapleable stapleable = GetComponent<Stapleable>();
        if (stapleable == null) {
            return;
        }

        List<Stapleable> allLinked = new List<Stapleable>();
        stapleable.GetAllLinked(allLinked);
        foreach (Stapleable s in allLinked) {
            if (s.transform != this.transform) {
                s.GetComponent<DragnDrop>().BeginDrag();
            }
        }
    }

    public void EndDrag() {
        CurrentlyDraggedObject.transform.eulerAngles = new Vector3(
            CurrentlyDraggedObject.transform.eulerAngles.x,
            CurrentlyDraggedObject.transform.eulerAngles.y,
            CurrentlyDraggedObject.transform.eulerAngles.z + 5
        );
        GetComponentInChildren<ScaleToggle>()?.Toggle();
        GetComponent<SetZByY>().UpdateZ();
    }

    private void OnMouseUp(){
        EndDrag();

        Stapleable stapleable = GetComponent<Stapleable>();
        if (stapleable == null) {
            return;
        }

        List<Stapleable> allLinked = new List<Stapleable>();
        stapleable.GetAllLinked(allLinked);
        foreach (Stapleable s in allLinked) {
            if (s.transform != this.transform) {
                s.GetComponent<DragnDrop>().EndDrag();
            }
        }
    }

    public void Drag() {

        Vector2 mousePos = Master.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(
            mousePos.x + mousePosDeltaOnInitiateDrag.x,
            mousePos.y + mousePosDeltaOnInitiateDrag.y,
            transform.position.z
        );

        //Check if still on surface
        if (IsInLegalPosition()) {
            lastLegalPosition = transform.position;
        }
    }

    private void OnMouseDrag() {
        Drag();

        Stapleable stapleable = GetComponent<Stapleable>();
        if (stapleable == null) {
            return;
        }

        List<Stapleable> allLinked = new List<Stapleable>();
        stapleable.GetAllLinked(allLinked);
        foreach (Stapleable s in allLinked) {
            if (s.transform != this.transform) {
                s.GetComponent<DragnDrop>().Drag();
            }
        }
    }

}
