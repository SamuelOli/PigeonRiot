using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalEnemys;
    public int currentEnemys;

    public Text txtEnemy;

    public static GameManager gm;

    public GameObject gameplay; //HUD do player com skills e munição
    public GameObject gameOver; //tela de Game Over
    public GameObject win;      //tela de Win
    

    private void Awake()
    {
        //Cursor.visible = false;
        /*if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
        gm = this;
    }

    public void Menu()
    {
        //Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        //Cursor.visible = true;
        gameOver.SetActive(true);
        gameplay.SetActive(false);
    }
    public void Win()
    {
        //Cursor.visible = true;
        win.SetActive(true);
        gameplay.SetActive(false);
    }

    public void AddEnemy() //Chamado quando um inimigo é criado
    {
        totalEnemys++;
        GetEnemysCount();
    }

    public void RemoveEnemy() //Chamado quando um inimigo é morto
    {
        currentEnemys++;
        GetEnemysCount();

        if(currentEnemys >= totalEnemys)
        {
            Win();
        }
    }

    public void GetEnemysCount() //Atualiza a UI de inimigos
    {
        string t = "Enemys " + currentEnemys + "/" + totalEnemys;
        txtEnemy.text = t;
        txtEnemy.transform.GetChild(0).GetComponent<Text>().text = t;
        txtEnemy.transform.GetChild(1).GetComponent<Text>().text = t;
    }
}
