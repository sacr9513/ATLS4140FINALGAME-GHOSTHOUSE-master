using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private Collider interactionZone;
    private void Start(){
        interactionZone = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other){
        //check tags of collisions and code for: notes, batteries, keys
        if(other.gameObject.tag == "Player"){
            Debug.Log("Item found");
        }
        
    }
}
