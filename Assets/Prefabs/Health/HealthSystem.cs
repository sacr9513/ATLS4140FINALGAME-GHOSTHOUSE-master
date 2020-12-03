using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    private int health {set; get;}
    private int maxHealth;


    
    public HealthSystem(int maxHealth){
        this.maxHealth = maxHealth;
        this.health = maxHealth;
    }

    public float GetHealthPercentage(){
        return (float)health / maxHealth;
    }
    public void Damage(int amount){
        this.health -= amount;
        if(this.health < 0) health = 0;
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        //Debug.Log("Health: " + health);
    }

    public void Heal(int amount){
        this.health += amount;
        if(this.health > maxHealth) health = maxHealth;
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        //Debug.Log("Health: " + health);
    }
}
