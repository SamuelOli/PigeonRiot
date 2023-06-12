using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour  //Usado para que a musica do jogo continue em loop mesmo quando mudar de cena
{
    public static SoundManager sm;

    private void Awake()
    {
        if (sm == null)
         {
             sm = this;
         }
         else if (sm != this)
         {
             Destroy(gameObject);
         }
        DontDestroyOnLoad(gameObject);
    }
}
