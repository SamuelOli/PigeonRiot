using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Skill : Skill
{

    protected override void Start()
    {
        base.Start();
        type = SkillType.Escape;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
    }

    public override void Use()
    {
        if (currentTime <= 0)
        {
            PlayerController p = GetComponentInParent<PlayerController>();
            p.Stop(); //Freia o player
            //Recebe os inputs para calcular a direção
            p.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * 1500); 
            currentTime = time;
            StartCoroutine(Continue());
        }
    }

    //Tempo para que o player não possa se mover durante o dash
    IEnumerator Continue() 
    {
        yield return new WaitForSeconds(.2f);
        GetComponentInParent<PlayerController>().Move();
    }
}
