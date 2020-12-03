using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public float speed = 1f;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("Rotate", 2.0f);
    }

    private IEnumerator Rotate(){
        transform.Rotate(new Vector3(0,90,0), Space.Self);
        Debug.Log("Rotated");
        yield return new WaitForSeconds(4.0f);
        yield return StartCoroutine("Rotate");

    }
   
}
