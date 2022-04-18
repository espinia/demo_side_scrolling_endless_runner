using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{    
    public float floatForce= 10.0f;
    public float upperBound= 14.0f;
    public float boingForce = 10.0f;

    private float gravityModifier = 1.5f;
    private bool _gameOver = false;

    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bouncingSound;

    private MeshRenderer meshRenderer;

    public bool GameOver
    {
        get => _gameOver;
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        meshRenderer = GetComponent<MeshRenderer>(); ;

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !_gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }

        if(transform.position.y> upperBound)
		{
            playerRb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, upperBound, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            _gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            meshRenderer.enabled = false;
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Ground") && !_gameOver)
        {
            playerAudio.PlayOneShot(bouncingSound, 1.0f);
            playerRb.AddForce(Vector3.up * boingForce, ForceMode.Impulse);
        }
    }
}
