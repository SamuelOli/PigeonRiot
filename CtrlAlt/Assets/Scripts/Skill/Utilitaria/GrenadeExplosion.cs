using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public bool isEnemy;
    public float damage;

    public List<GameCombat> enemys;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GameCombat>() != null)
        {
            //if (other.GetComponent<GameCombat>().isEnemy != isEnemy)
            {
                if (!enemys.Contains(other.GetComponent<GameCombat>()))
                {
                    enemys.Add(other.GetComponent<GameCombat>());
                }
            }
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(.25f);
        foreach(GameCombat comb in enemys)
        {
            comb.TakeDamage(damage);
        }
        StartCoroutine(DestoyAfter());
    }
    IEnumerator DestoyAfter()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
