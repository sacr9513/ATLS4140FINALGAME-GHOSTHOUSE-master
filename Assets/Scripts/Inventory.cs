using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemTypes,int> inventory;

    private void Awake(){
        inventory = new Dictionary<ItemTypes, int>();
    }

    public void AddItem(ItemTypes types, int count =1 ){
        if(!inventory.TryGetValue(types, out int current)){
            inventory.Add(types, count);
            DisplayInventory();
            //Debug.Log(inventory[types]);
        }
        else{
            inventory[types] += count;
            DisplayInventory();
            //Debug.Log(inventory[types]);
        }
    }
    public int Get(ItemTypes item){
        if(inventory.TryGetValue(item, out int current)){
            return current;
        }
        else{
            throw new KeyNotFoundException();
        }
    }

    // FOR DEBUGGING
    public void DisplayInventory(){
        foreach(var entry in inventory){
            Debug.Log(entry);
        }
    }

    public void UseItem(Item item) {
        switch (item.type) {
        case ItemTypes.Battery:
            if(inventory.ContainsKey(ItemTypes.Battery) && inventory[ItemTypes.Battery] > 0){
                inventory[ItemTypes.Battery] -= 1;
            }
            else{
                Debug.Log("No Batteries in inventory");

            }
            // fill battery bar NEED UI to activate batteries and the rest of the below
            break;
        case ItemTypes.Key:
            // will implement key mechanics when puzzles are more planned out
            break;
        case ItemTypes.Note:
            // read note
            break;
        }
    }

}
