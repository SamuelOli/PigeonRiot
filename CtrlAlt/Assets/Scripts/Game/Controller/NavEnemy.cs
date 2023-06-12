using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavEnemy : MonoBehaviour
{
    private NavMeshAgent agent; //IA de movimenta��o
    GameObject player;      // O player ser� o target dos inimigos
    public GameObject bulletSpawner;
    public float stop; //Parado
    public bool foundPlayer = false; //Encontrou o player?
    void Start()
    {
        player = GameObject.Find("Player");

        //Pega o Componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        //Variaveis setadas como False para N�o utilizar os eixos Y Baseado em 3 dimens�es
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        if (foundPlayer) //J� viu o player?
        {
            Move();      //Sim, Mova
        }
        else
        {
            LookPlayer(); //N�o, Procure
        }
    }

    public void Move()
    {
        //Faz o personagem se locomover pelo cenario at� o player
        Vector2 direction = (player.transform.position - bulletSpawner.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(bulletSpawner.transform.position, direction, 5f);
        Debug.DrawRay(transform.position, direction, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    agent.isStopped = true;
                    return;
                }
                else if(hit.collider.gameObject.tag == "Bullet")
                {
                    agent.isStopped = true;
                    return;
                }
                else if(hit.collider.gameObject.tag == "Obstacle")
                {
                    agent.isStopped = false;
                    return;
                }
            }
        }
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }

    public void LookPlayer() //Procura o player
    {
        Vector2 direction = (player.transform.position - bulletSpawner.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(bulletSpawner.transform.position, direction, 12f); //Tra�a uma reta at� o player
        Debug.DrawRay(transform.position, direction, Color.yellow); //Desenha a reta
        if (hit.collider != null)
        {
            if (hit.collider.gameObject != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    //Debug.Log(name + " Found: " + hit.distance);
                    foundPlayer = true;                                //Achou o player
                }
            }
        }
    }
}

