using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotateSpeed = 60.0f;
    public float translateSpeed = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left * Time.deltaTime * translateSpeed);
        //se usa el local porque va girando, asi actualizamos la posicion dentro de la escena
        transform.localPosition += Vector3.left * Time.deltaTime * translateSpeed;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
