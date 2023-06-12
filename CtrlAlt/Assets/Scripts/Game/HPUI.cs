using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    public Transform combat;
    public bool intanciado = false;
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (combat == null && intanciado)
        {
           Destroy(this.gameObject);
            
        }
        if (combat != null)
        {
            transform.position = pos + combat.position;
        }
    }
}
