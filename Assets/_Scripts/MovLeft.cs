using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovLeft : MonoBehaviour
{
    public float speed=30.0f;

    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        //por etiqueta
        //_playerController=GameObject.FindGameObjectWithTag
        //por nombre
        //_playerController = GameObject.Find
        //por tipo, mas costoso
        //_playerController=GameObject.FindObjectOfType(typeof())
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.GameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
