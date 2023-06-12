using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public List<GameObject> panels;
    int iPanels;

    
    void Start()
    {
        panels[iPanels].SetActive(true);
        StartCoroutine(NextPanel());
    }

    
    void Update()
    {
        
    }

    IEnumerator NextPanel() //Alterna os paineis para dar a sensação de sirene
    {
        yield return new WaitForSeconds(.3f);
        Debug.Log("NextPanel" + iPanels);
        panels[iPanels].SetActive(false);
        iPanels++;
        if(iPanels >= panels.Count)
        {
            iPanels = 0;
        }
        panels[iPanels].SetActive(true);
        StartCoroutine(NextPanel());
    }
}
