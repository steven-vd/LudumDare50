using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist_Generator : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] faces;
    public int faceCount;
    public bool setFace = false;
    public bool limitFace = false;
    public int activeFace;
    public bool weird;

    void Start(){
        faceCount = transform.childCount;
        faces = new GameObject[faceCount];
        for(int i = 0; i < faceCount; i++){
            faces[i] = transform.GetChild(i).gameObject;
        }
        if(setFace == false)
        activeFace = Random.Range(0, faceCount);
        else if(weird == false && limitFace == true)
        activeFace = Random.Range(0, 2);
        else if(limitFace == true)
        activeFace = Random.Range(3, 5);

        faces[activeFace].SetActive(true);
        Debug.Log(activeFace);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
