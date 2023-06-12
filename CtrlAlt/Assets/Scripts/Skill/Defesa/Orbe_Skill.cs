using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe_Skill : Skill
{
   
    protected override void Start()
    {
        base.Start();
        type = SkillType.Defensiva;
    }

    
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Use();
        }

        if(currentDuration <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false; // Desabilita o escudo ao redor do player
        }
    }

    public override void Use()
    {
        if (currentTime <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = true; // Mostra um escudo ao redor do player

            currentDuration = duration;
            currentTime = time;
        }
    }
}
