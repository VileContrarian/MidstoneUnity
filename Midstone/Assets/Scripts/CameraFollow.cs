using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform target;           
    public Transform camBoundMin;  
    public Transform camBoundMax; 
    float xMin, xMax, yMin, yMax;
    
    void Start () {

        GameObject go = GameObject.FindWithTag("Player");
        if(!go)
        {
            Debug.Log("Player not found.");
            return;
        }

        target = go.GetComponent<Transform>();

        xMin = camBoundMin.position.x;
        yMin = camBoundMin.position.y;

        xMax = camBoundMax.position.x;
        yMax = camBoundMax.position.y;
    }
	
	void Update () {

        if(target)
        {

            transform.position = new Vector3( Mathf.Clamp(target.position.x, xMin, xMax),Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
        }
		
	}
}
