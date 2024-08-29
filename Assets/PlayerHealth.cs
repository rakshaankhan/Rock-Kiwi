using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int testDamageAmt = 20;
    public int currentHealth;
    public int currentEnergy;
    public int currentArmor;

    public HealthBar healthBar;
    public HealthBar energyShield;
    public HealthBar armorDial;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxHealth;
        currentArmor = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        energyShield.SetMaxHealth(maxHealth);
        armorDial.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHealthDamage(testDamageAmt);
            TakeEnergyDamage(10);
            TakeArmorDamage(15);
        }
        
    }

    void TakeHealthDamage(int healthdamage)
    {
        currentHealth = System.Math.Max(0, currentHealth-healthdamage);
        healthBar.SetHealth(currentHealth);

    }

    void TakeEnergyDamage(int energydamage)
    {
        currentEnergy -= energydamage;
        energyShield.SetHealth(currentEnergy);
    }

    void TakeArmorDamage(int armorDamage)
    {
        currentArmor -= armorDamage;
        armorDial.SetHealth(currentArmor);
    }
}
