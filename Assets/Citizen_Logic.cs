using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_Logic : MonoBehaviour
{
    private GameObject currentCitizen;
    private GameObject nextCitizen;
    

    // Start is called before the first frame update
    void Start() {
    nextCitizen = transform.GetChild(0).gameObject;
    WalkUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(nextCitizen != null)
        if(nextCitizen.transform.position.x < 0.0f){
        nextCitizen.transform.position = new Vector3(nextCitizen.transform.position.x+300.0f* Time.deltaTime, nextCitizen.transform.position.y, nextCitizen.transform.position.z); 
        }
    
        if(currentCitizen != null)
        if(currentCitizen.transform.position.x < 1500.0f){
        currentCitizen.transform.position = new Vector3(currentCitizen.transform.position.x+400.0f* Time.deltaTime, currentCitizen.transform.position.y, currentCitizen.transform.position.z); 
        }
        else
        Destroy(currentCitizen);
        


    }

    public void WalkUp(){
        nextCitizen.SetActive(true);
        nextCitizen.transform.position = new Vector3(-1500.0f, nextCitizen.transform.position.y, nextCitizen.transform.position.z);
        
    }

    public void WalkAway(){
        currentCitizen = nextCitizen;
        if(transform.childCount >= 2){
        nextCitizen =  transform.GetChild(1).gameObject;
        WalkUp();
    }

    }

    public void UnLoadPackage(){



    }

    public void DeletePackage(){


    }


}
