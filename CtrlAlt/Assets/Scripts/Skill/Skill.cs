using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillType type;                //Tipo da skill
    public float time;                   //Tempo de Cooldown da skill
    protected float currentTime;        //Tempo des da ultima utilização
    public float duration;             //Tempo de duração da skill total para fins de Buff
    protected float currentDuration;  //Tempo de duração da skill para fins de Buff
    public Image button;             //Botão da skill

    protected  virtual void Start()
    {
        currentTime = time;
        currentDuration = duration;
    }

    
    protected virtual void Update()
    {
        currentTime-=Time.deltaTime;
        currentDuration-=Time.deltaTime;
        button.fillAmount = currentTime / time;
    }

    public virtual void Use()
    {
        if(time <= 0)
        {

        }
    }

    public float GetDuration()
    {
        return currentDuration;
    }
}
public enum SkillType
{
    Defensiva,
    Escape,
    Utilitaria,
    Especiais
}
