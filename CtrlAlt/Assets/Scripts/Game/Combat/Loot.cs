using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Header("Loot 10 = 1%"), Range(0, 1000)]
    public int chance;

    public int valor;
    public TypeLoot t;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCombat p = collision.gameObject.GetComponent<PlayerCombat>();
            if (t == TypeLoot.heal)
            {
                p.Heal(valor);
            }
            else if (t == TypeLoot.Pistol)
            {
                p.CollectMunition(0, valor);
            }
            else if(t == TypeLoot.M4)
            {
                p.CollectMunition(1, valor);
            }
            else
            {
                p.CollectGrenade(valor);
            }
            Destroy(this.gameObject);
        }
    }
    
}



public enum TypeLoot
{
    heal,
    M4,
    Pistol,
    Grenade
}