using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;

    public ItemTypes type;
    public Inventory inventory;
    private void Awake(){
        var player = GameObject.FindGameObjectWithTag("player");
        inventory = player.GetComponent<Inventory>();
    }

    public Item(string name, ItemTypes type){
        name = name;
        type = type;
    }
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "player"){
            inventory.AddItem(type);

            //BUG: MOVING THROUGH ITEM OBJECTS TOO FAST WILL NOT BE DESTROYED FAST ENOUGH AND COUNT IT TWICE OR MORE
            // NEEDS TIME TO PICK UP ITEMS ONCE THEN DESTROY THEM
            // HAVE TRIED TO USE ONTRIGGER ENTER AND EXIT FUNCTION

            Destroy(this.GetComponent<MeshFilter>());
            Destroy(this);
            //Destroy(this.GetComponent<MeshFilter>());
        }
        else{

        }
        
    }
}