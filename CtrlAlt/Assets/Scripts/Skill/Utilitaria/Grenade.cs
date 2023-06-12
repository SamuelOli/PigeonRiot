using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public bool isEnemy;
    public float damage;
    public float speed = 10f;
    public float time;
    public Vector2 target;
    Rigidbody2D rb;

    public GameObject explosion;
    void Start()
    {
        Destroy(gameObject, time);
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //transform.Translate(target * speed * Time.deltaTime);
        rb.velocity = target.normalized * speed * Time.deltaTime;
        rb.rotation += 500 * Time.deltaTime;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            explosion = Instantiate(explosion);
            explosion.transform.position = transform.position;
            explosion.GetComponent<GrenadeExplosion>().damage = damage;
            explosion.GetComponent<GrenadeExplosion>().isEnemy = isEnemy;
            Destroy(this.gameObject);
        }
        if (other.GetComponent<GameCombat>() != null)
        {
            if (other.GetComponent<GameCombat>().isEnemy != isEnemy)
            {
                explosion = Instantiate(explosion);
                explosion.transform.position = transform.position;
                explosion.GetComponent<GrenadeExplosion>().damage = damage;
                explosion.GetComponent<GrenadeExplosion>().isEnemy = isEnemy;
                Destroy(this.gameObject);
            }
        }
    }
}