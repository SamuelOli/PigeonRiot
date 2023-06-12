using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCombat : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth; //Vida maxima
    protected float health; //Vida atual
    public float armor;     //Armadura

    public float attackSpeed;
    protected float currentAttackSpeed;
    public bool isEnemy;  //É inimigo?

    [Header("Prefs")]
    public GameObject bullet;        //Bala
    public Transform bulletSpawner;  //De onde sai as balas
    public GameObject canvasWorld;   //Canvas onde será instanciada a vida e o UI de dano
    public GameObject txtDamage;     //UI do dano
    public GameObject txtDamageBlock;//UI do dano bloqueado
    public GameObject prefHP;        //UI HP
    public Transform finalCollider;  //Final do collider, usado para calcular se existe parede entre ele o bulletSpawner

    public virtual void Start()
    {
        canvasWorld = GameObject.FindGameObjectWithTag("Canvas World");
        txtDamage = canvasWorld.transform.GetChild(0).gameObject;
        txtDamageBlock = canvasWorld.transform.GetChild(1).gameObject;
        health = maxHealth;
        prefHP = Instantiate(prefHP, canvasWorld.transform);
        prefHP.GetComponent<HPUI>().combat = transform;
        prefHP.GetComponent<HPUI>().intanciado = true;
    }

    public virtual void Update()
    {
        currentAttackSpeed -= Time.deltaTime;
    }

    public virtual void Attack(Vector3 target) //Ataque
    {
        if (currentAttackSpeed <= 0)
        {
            Vector2 distance = target - transform.position;
            GameObject b = Instantiate(bullet, transform.position, transform.rotation);
            b.GetComponent<Bullet>().target = distance;

            target = new Vector3(0, 0, target.z);
            b.transform.LookAt(new Vector3(0, 0, 1), target);

            currentAttackSpeed = attackSpeed;
        }
    }

    public virtual void TakeDamage(float damage) //Toma dano
    {
        damage -= damage * armor / 100; //Calcula o dano com base na armadura

        health -= damage;
        //Debug.Log(damage + " - " + health);
        TxtDamageInstance(damage); //UI do Dano

        prefHP.transform.GetChild(0).GetComponent<Image>().fillAmount = health / maxHealth; //Atualiza a barra de vida

        if (health <= 0)
        {
            Death(); //Morto
        }
    }
    public virtual void Heal(int h) //Cura
    {
        health += h;
        if (health > maxHealth) //Verifica para não ficar com mais vida do que o limite
        {
            health = maxHealth;
        }
        prefHP.transform.GetChild(0).GetComponent<Image>().fillAmount = health / maxHealth; //Atualiza a barra de vida
    }
    public void TxtDamageInstance(float damage) //UI do Dano
    {
        GameObject d;
        if(damage > 0) { d = Instantiate(txtDamage, canvasWorld.transform); }
        else { d = Instantiate(txtDamageBlock, canvasWorld.transform); }

        d.transform.position = transform.position + Vector3.up;
        d.SetActive(true);
        d.GetComponent<Text>().text = damage.ToString();
        d.transform.GetChild(0).GetComponent<Text>().text = damage.ToString(); //Sombra do texto
        d.transform.GetChild(1).GetComponent<Text>().text = damage.ToString(); //Sombra do texto
    }
    public virtual void Death() //Morto
    {
        Destroy(this.gameObject); 
    }
}
