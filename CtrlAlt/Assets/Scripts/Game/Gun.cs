using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;                //Dano
    public float attackSpeed;           //Velocidade
    protected float currentAttackSpeed; //Tempo percorrido
    public float range;                 //Distancia
    public float munition;              //Balas no pente atual
    public float munitionClip;          //Balas por pente
    public float totalMunition;         //Balas total
    public Transform bulletSpawner;     //De onde o tiro sai

    public bool reloading;              //Quando está recarregando
    private bool isStarted;             //Se foi iniciado

    public Animator anim;
    public float afterTime;             //Delay do tiro para ficar no tempo da animação

    AudioSource audio;

    void Start()
    {
        bulletSpawner = transform.GetChild(0);
        OnStart();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isStarted = true;
    }

    void Update()
    {
        currentAttackSpeed -= Time.deltaTime;
    }
    void OnStart()
    {
        if (!isStarted)
        {
            if (munitionClip < totalMunition)
            {
                munition = munitionClip;
                totalMunition -= munitionClip;
            }
            else
            {
                munition = totalMunition;
                totalMunition = 0;
            }
            isStarted = true;
        }
    }
    public bool Fire() //Quando atira
    {
        if (munition > 0 && !reloading)
        {
            if (currentAttackSpeed <= 0)
            {
                anim.SetTrigger("Fire");
                munition--;
                audio.Play();
                if (munition <= 0)
                {
                    StartCoroutine(ReloadAfter());
                }
                currentAttackSpeed = attackSpeed;
                return true;
            }
            return false;
        }
        StartCoroutine(Reload());
        return false;
    }

    public string GetText()
    {
       OnStart();
        return munition + "/" + totalMunition;
    }

    IEnumerator ReloadAfter() //Tempo que demora para Recarregar
    {
        yield return new WaitForSeconds(.5f);
        Reload();
    }

    public IEnumerator Reload()  //Recarregar arma
    {
        if (!reloading && munition < munitionClip)
        {
            if (totalMunition > 0)
            {
                anim.SetBool("Reload", true);  //Animação da recarga
                reloading = true;
                yield return new WaitForSeconds(3f); //Tempo da recarga
                if (totalMunition > munitionClip)
                {
                    totalMunition -= munitionClip - munition;
                    munition = munitionClip;                  
                }
                else //Coloca o resto das balas no ultimo pente
                {
                    munition = totalMunition;
                    totalMunition = 0;
                }
                if (GetComponentInParent<PlayerCombat>() != null)
                {
                    GetComponentInParent<PlayerCombat>().SetMunitionText();
                }
                reloading = false;
                anim.SetBool("Reload", false);
            }
            else
            {
                //Audio de feedback para saber que não tem mais balas
            }
        }
    }
}
