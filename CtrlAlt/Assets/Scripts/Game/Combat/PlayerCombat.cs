using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : GameCombat
{
    public List<Gun> guns;   //Lista de armas
    int iGun;                //Arma atual
    public Text txtMunition; 
    bool inFire;             //Atirando?
    public Grenade_Skill grenade; //Skill de Granada

    public override void Start()
    {
        base.Start();
        isEnemy = false;
        bulletSpawner = guns[iGun].bulletSpawner; //Seta a arma
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.Mouse0))  //Tiro
        {
            Attack(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) //Troca de arma
        {
            NextGun();
        }
        if (Input.GetKey(KeyCode.R))  //Recarrega
        {
            StartCoroutine(guns[iGun].Reload());
        }
        
    }

    public override void Attack(Vector3 target) //Atira
    {
        //base.Attack(enemy);
        if (!inFire) //Se não estiver atirando
        {
            if (guns[iGun].Fire()) //Atira com a arma selecionada
            {
                StartCoroutine(AttackAfter(target)); //Delay do tiro
                inFire = true; //Agora está atirando
            }
        }
    }

    IEnumerator AttackAfter(Vector3 target) //Delay do tiro para sincronizar com a animação
    {
        yield return new WaitForSeconds(guns[iGun].afterTime); //Tempo de delay
        target = Camera.main.ScreenToWorldPoint(target); //Ponto onde o mouse está

        Vector2 d = (guns[iGun].bulletSpawner.position - finalCollider.position); 
        RaycastHit2D hit = Physics2D.Raycast(finalCollider.position, d, d.magnitude); //Traça uma linha entre o player e o spawner da bala
        if (hit.collider != null && hit.collider.gameObject.tag == "Obstacle") //Detecta se tem parede no meio
        {
            //Colisão com a parede
            Debug.Log("Atirando na: " + hit.collider.name);
        }
        else  //Pode atirar
        {
            Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y); //Direção do tiro para onde o mouse está
            GameObject b = Instantiate(bullet, guns[iGun].bulletSpawner.position, this.transform.rotation);
            b.transform.up = direction;
            b.GetComponent<Bullet>().damage = guns[iGun].damage;
            b.GetComponent<Bullet>().target = direction;
            b.GetComponent<Bullet>().isEnemy = isEnemy;
        }
        
        SetMunitionText(); //Atualiza UI de munição
        inFire = false; //Não está mais atirando
    }

   public void CollectMunition(int i, int valor) //Coleta munição
    {
        guns[i].totalMunition += valor;
        SetMunitionText();
    }

    public void CollectGrenade(int valor) //Coleta granada
    {
        grenade.amount += valor;
        grenade.SetAmoutTxt();
    }

    public void SetMunitionText() //Atualiza UI de munição
    {
        txtMunition.text = guns[iGun].GetText();
        txtMunition.transform.GetChild(0).GetComponent<Text>().text = guns[iGun].GetText();
        txtMunition.transform.GetChild(1).GetComponent<Text>().text = guns[iGun].GetText();
    }

    public override void TakeDamage(float damage) //Atualiza o sistema de tomar dano
    {
        if(GetComponentInChildren<Orbe_Skill>() != null) //Verifica se existe skill de escudo
        {
            if(GetComponentInChildren<Orbe_Skill>().GetDuration() > 0) //Se ela estiver ativa bloqueia todo o dano
            {
                TxtDamageInstance(0);
                return;
            }
        }

        base.TakeDamage(damage); //Se não irá tomar dano normalmente
    }

    public void NextGun() //Troca de arma
    {
        guns[iGun].gameObject.SetActive(false); //Desativa a arma antiga

        iGun++;
        if(iGun>= guns.Count) //Retorna para a primeira arma
        {
            iGun = 0;
        }

        guns[iGun].gameObject.SetActive(true);  //Ativa a arma atual
        bulletSpawner = guns[iGun].bulletSpawner; //Atualiza o spawner das balas
        if(bulletSpawner == null)
        {
            bulletSpawner = guns[iGun].transform.GetChild(0); //spawner default para evitar erros
        }

        SetMunitionText(); //Atualiza a UI de munição
    }

    public override void Death() //Morto
    {
        GameManager.gm.GameOver(); //Tela de Game Over

        gameObject.SetActive(false); //Só desativa o player
    }

}
