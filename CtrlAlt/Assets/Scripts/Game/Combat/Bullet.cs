using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isEnemy;      //Veio de um inimigo?
    public float damage;      //Dano da bala que será setado pela arma
    public float speed = 10f; //Velocidade
    public float time;        //Tempo de duração
    public Vector2 target;    //Alvo
    Rigidbody2D rb;
    public bool isRotation;   //Pode rotacionar?

    void Start()
    {
        Destroy(gameObject, time);  //Se destroi depois do tempo programado
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //transform.Translate(target * speed * Time.deltaTime);
        rb.velocity = target.normalized * speed * Time.deltaTime; //Aplica velocidade
        if (isRotation)
        {
            rb.rotation += 500 * Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) //Quando colidir
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        if (other.GetComponent<GameCombat>() != null)
        {
            if (other.GetComponent<GameCombat>().isEnemy != isEnemy) //Se for do time oposto aplica o dano
            {
                other.gameObject.GetComponent<GameCombat>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
