using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : GameCombat
{
    public GameObject player;
    public GameManager gm;
    public Gun gun;
    bool inFire;              //Atirando

    public List<Loot> loot;   //Lista de loot

    NavEnemy nav;             //IA de movimentação

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        isEnemy = true;
        gm.AddEnemy();
        nav= GetComponent<NavEnemy>();
    }

    public override void Start()
    {
        base.Start();
        bulletSpawner = gun.bulletSpawner; 
        gun.totalMunition = 99999999; //Munição infinita, mas o pente ainda tem um limite
    }

    void Update()
    {
        base.Update();

        if (currentAttackSpeed <= 0)
        {
            Attack(player.transform.position);
        }
    }

    public override void Attack(Vector3 enemy)
    {
        Vector2 direction = (player.transform.position - bulletSpawner.position);    //pega a direção
        RaycastHit2D hit = Physics2D.Raycast(bulletSpawner.position, direction, 5f); //Traça uma reta entre o inimigo e o player
        Debug.DrawRay(bulletSpawner.transform.position, direction, Color.red);       //Desenha a reta
        if (hit.collider != null)
        {
            if (hit.collider.gameObject != null)
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    //Debug.Log(hit.transform.gameObject.name);
                    
                }
                else //Viu o Player
                {
                    nav.foundPlayer = true; //Segue o Player
                    if (!inFire)
                    {
                        if (gun.Fire()) //Atira
                        {
                            //Debug.Log(name + " Attack: " + hit.distance);
                            StartCoroutine(AttackAfter(player.transform.position));
                            inFire = true;
                        }
                    }
                }
            }
        }
    }

    IEnumerator AttackAfter(Vector3 enemy) //Atira no player
    {
        yield return new WaitForSeconds(gun.afterTime); //Delay do tiro para igualar com a animação
        Vector2 d = (bulletSpawner.position - finalCollider.position);
        RaycastHit2D hit = Physics2D.Raycast(finalCollider.position, d, d.magnitude); //Traça uma reta do braço até o spawner para que não atire pela parede
        if (hit.collider != null && hit.collider.gameObject.tag == "Obstacle") //Detecta a parede
        {
            //Colisão com a parede
            Debug.Log("Atirando na: " + hit.collider.name);
        }
        else //Pode atirar
        {
            Vector2 target = new Vector2(enemy.x - transform.position.x, enemy.y - transform.position.y); //Direção do tiro
            GameObject b = Instantiate(bullet, bulletSpawner.position, this.transform.rotation);
            b.transform.up = target;
            b.GetComponent<Bullet>().target = target;
            b.GetComponent<Bullet>().isEnemy = isEnemy;
            b.GetComponent<Bullet>().damage = gun.damage;
        }
        inFire = false;
    }

    public void DropLoot()  //Dropa o loot randomicamente para o player
    {
        float r = Random.Range(0, 1000f);
        for (int i = 0; i < loot.Count; i++)
        {
            if (loot[i] != null)
            {
                if (loot[i].chance >= r)
                {
                    Instantiate(loot[i].gameObject, transform.position, transform.rotation);
                    return;
                }
                else
                {
                    r -= loot[i].chance;
                }
            }
        }
    }

    public override void Death() //Morto
    {
        gm.RemoveEnemy();
        DropLoot();
        Destroy(this.gameObject);
    }

}