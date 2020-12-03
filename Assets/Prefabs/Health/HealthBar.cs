using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Image healthBar;

    public void Setup(HealthSystem healthSystem){
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        
    }

    
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e){
        healthBar.fillAmount = healthSystem.GetHealthPercentage();
    }
}
