using UnityEngine;

public class Citizen_Logic : MonoBehaviour {

    public static Citizen_Logic Instance;

    private GameObject currentCitizen;
    private GameObject nextCitizen;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        nextCitizen = transform.GetChild(0).gameObject;
        WalkUp();
    }

    void Update() {
        if(nextCitizen != null)
        if(nextCitizen.transform.position.x < 0.0f){
            nextCitizen.transform.position = new Vector3(nextCitizen.transform.position.x+300.0f* Time.deltaTime, nextCitizen.transform.position.y, nextCitizen.transform.position.z); 
        }
        else{
            UnLoadPackage();
            //WalkAway(false); //Auto accept when in center
        }

        if(currentCitizen != null)
        if(currentCitizen.transform.position.x < 1500.0f){
        currentCitizen.transform.position = new Vector3(currentCitizen.transform.position.x+400.0f* Time.deltaTime, currentCitizen.transform.position.y, currentCitizen.transform.position.z); 
        }
        else{
        Destroy(currentCitizen);
        
        }

    }

    public void WalkUp(){
        nextCitizen.SetActive(true);
        nextCitizen.transform.position = new Vector3(-1500.0f, nextCitizen.transform.position.y, nextCitizen.transform.position.z);
        
    }

    public void WalkAway(bool delayed){
        if(delayed == true)
        transform.parent.GetChild(2).GetChild(1).gameObject.GetComponent<Animator>().Play("Appearing", 0);
        else
        transform.parent.GetChild(2).GetChild(0).gameObject.GetComponent<Animator>().Play("Appearing", 0);


        DeletePackage();
        currentCitizen = nextCitizen;
        if(transform.childCount >= 2){
             nextCitizen =  transform.GetChild(1).gameObject;
             WalkUp();
        }

    }

    public void UnLoadPackage(){
        if(transform.parent.GetChild(1).childCount >= 1)
        transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(true);
        
        TransactionHandler.Instance.goPackage = transform.parent.GetChild(1).GetChild(0).gameObject;
    }

    public void DeletePackage(){
        Destroy(transform.parent.GetChild(1).GetChild(0).gameObject);

        TransactionHandler.Instance.goPackage = null;
    }




}
