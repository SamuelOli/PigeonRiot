using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grenade_Skill : Skill
{
    public GameObject pref;
    public float damage;
    public int amount;
    public Text txtAmount;
    GameCombat gc;

    protected override void Start()
    {
        base.Start();
        type = SkillType.Escape;
        gc = gameObject.GetComponentInParent<GameCombat>(); //Chama o Game Controller do Player
        SetAmoutTxt(); //Atualiza a quantidade de granadas para o Player
    }

    protected override void Update()
    {
        if (amount > 0)
        {
            base.Update();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Use();
        }
    }

    public override void Use()
    {
        if(currentTime <= 0 && amount > 0)
        {
            //Calcula a direção da granda
            Vector2 target = Input.mousePosition;
            target = Camera.main.ScreenToWorldPoint(target);
            Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
            //transform.up = direction;

            //Instancia a granada
            GameObject b = Instantiate(pref, gc.bulletSpawner.position, this.transform.rotation);
            b.transform.up = direction;
            b.GetComponent<Grenade>().damage = damage;
            b.GetComponent<Grenade>().target = direction;
            b.GetComponent<Grenade>().isEnemy = gc.isEnemy;

            //Reseta os valorezes da skill
            currentTime = time;
            amount--;
            SetAmoutTxt();
            button.fillAmount = currentTime / time;
        }
    }

    public void SetAmoutTxt() //Exibe para o player a quantidade de granadas que possui
    {
        txtAmount.text = amount.ToString();
        txtAmount.transform.GetChild(0).GetComponent<Text>().text = amount.ToString();
        txtAmount.transform.GetChild(1).GetComponent<Text>().text = amount.ToString();
    }
}
