using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;


    // Method that destroy projectiles after they hit something
    public void Hit()
    {
        Destroy(gameObject);
    }


    // Getter for damage
    public int GetDamage() { return damage; }
}
