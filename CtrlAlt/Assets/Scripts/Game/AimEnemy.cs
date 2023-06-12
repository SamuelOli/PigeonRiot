using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer gunRender; // Sprite da arma
    public SpriteRenderer handRender; // Sprite da mão
    private float angleGun;    // Angulo da Arma
    private GameObject player;    

    void Awake() 
    {
        player = GameObject.FindWithTag("Player");
        handRender = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        EnemyRotation();
        GunRotation();        
    }

    private void EnemyRotation()  //Calcula angulo do inimigo
    {
        Vector2 playerPos = player.transform.position;
        Vector2 objectPos = transform.position;
        playerPos.x = playerPos.x - objectPos.x;
        playerPos.y = playerPos.y - objectPos.y;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void GunRotation() //Calcula angulo da arma e do braço
    {
        angleGun = transform.rotation.z * 100;

        if (angleGun <= 70 && angleGun >= -70)
        {
            gunRender.flipY = false;
            handRender.flipY = false;
        }
        else if (angleGun > 70 && angleGun <= 100)
        {
            gunRender.flipY = true;
            handRender.flipY = true;
        }
        else if (angleGun >= -100 && angleGun < -70)
        {
            gunRender.flipY = true;
            handRender.flipY = true;
        }
    }
}
