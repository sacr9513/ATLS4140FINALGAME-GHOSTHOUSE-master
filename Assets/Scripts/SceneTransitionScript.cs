using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitionScript : MonoBehaviour
{
    [SerializeField]
    private BoxCollider Collider;
    [SerializeField]
    private string sceneName;
    private void Start(){
        Collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "player"){
            SceneManager.LoadScene(sceneName);
        }
    }
}
