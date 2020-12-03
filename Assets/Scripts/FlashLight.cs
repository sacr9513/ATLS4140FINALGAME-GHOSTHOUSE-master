using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private HealthSystem batterySystem;
    private bool isTurnedOn = false;
    private MeshCollider collider;
    private int frames = 0;

    private void Awake(){
        // REUSING HEALTH SYSTEM FOR BATTERY SYSTEM
        batterySystem = new HealthSystem(100000);
        Transform batteryBarTransform = GameObject.FindGameObjectWithTag("FlashLightBattery").transform;
        HealthBar batteryBar = batteryBarTransform.GetComponent<HealthBar>();
        batteryBar.Setup(batterySystem);

        collider = transform.Find("FlashLight").GetComponent<MeshCollider>();
        collider.enabled = false;

    }
    public void UseFlashLight(){
        //turn on flashlight and drain while on
        isTurnedOn ^= true; // will change to opposite state
        collider.enabled ^= true;
    }
    private void ForceTurnOffFlashLight(){
        isTurnedOn = false;
        collider.enabled = false;
    }
    private void Update(){
        if(isTurnedOn){
            if(batterySystem.GetHealthPercentage() > 0) batterySystem.Damage(1);
            else ForceTurnOffFlashLight();
            //drain battery and activate collider
            
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy"){
            var enemyObject = other.gameObject.GetComponent<EnemyScript>();
            enemyObject.TakeDamage(1);
            //Debug.Log("enemy taking damage");
            frames = 0;
        }
    }
    private void OnTriggerStay(Collider other){
        if(other.tag == "Enemy"){
            if(frames <= 60 ){
                frames++;
            }
            else{
                var enemyObject = other.gameObject.GetComponent<EnemyScript>();
                enemyObject.TakeDamage(5);
                //Debug.Log("enemy taking damage" + frames);
            }
            
        }
    }

    public bool FlashLightOn(){
        return isTurnedOn;
    }
    public void UseBattery(){
        batterySystem.Heal(100);
    }
}
