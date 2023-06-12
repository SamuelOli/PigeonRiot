using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayer : MonoBehaviour
{
    public List<SpriteRenderer> gunRenders; // Sprite das armas
    private float angleGun;                 // Angulo da arma
    public SpriteRenderer handRender;       // Sprite da mão

    void Start()
    {
        handRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GunRotation();
        PlayerRotation();
    }

    private void GunRotation() //Calcula angulo do braço e do braço
    {
        angleGun = transform.rotation.z * 100;
        //print("Angulo da arma " + angleGun);
        if (angleGun <= 70 && angleGun >= -70)
        {
            handRender.flipY = false;
        }
        else if (angleGun > 70 && angleGun <= 100)
        {
            handRender.flipY = true;
        }
        else if (angleGun >= -100 && angleGun < -70)
        {
            handRender.flipY = true;
        }

        foreach (SpriteRenderer f in gunRenders) //Para cada arma
        {
            f.flipY = handRender.flipY;
        }
    }

    private void PlayerRotation() //Calcula a rotação do braço do player
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
