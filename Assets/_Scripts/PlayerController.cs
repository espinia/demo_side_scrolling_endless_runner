using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//hace que requiera el componente obligatoriamente
//si no lo tiene lo añade
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	private const string SPEED_MULTIPLIER = "Speed Multiplier";
	private const string JUMP_TRIGGER = "Jump_trig";
	private const string SPEED_F = "Speed_f";
	private const string DEATH_B = "Death_b";
	private const string DEATH_TYPE = "DeathType_int";

	//privada evita hacks
	private Rigidbody playerRb;
    public float jumpForce;
    public float gravityMultiplier;

    public bool isOnGround=true;

    private bool _gameOver = false;

    private Animator _animator;

    private float speedMultiplier = 1.0f;

    public ParticleSystem explosion;
    public ParticleSystem trail;

    public AudioClip jumpSound, crashSound;

    private AudioSource _audioSource;
    [Range(0,1)]
    public float audioVolume = 1;

    //funciones lamba, get y set
    //variables autocomputadas.
    //normalmente va en mayuscula
    public bool GameOver
	{
        get => _gameOver;
		//lo quitamos para que solo pueda leerse
		//set => _gameOver = value;
        /* Lo podemos proteger para que se quede en true si ya era true
		set
		{
			if (_gameOver == true)
			{
                _gameOver = true;
			}
            else
            {
                _gameOver=value
            }
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        //inicializar componentes en el start
        playerRb = GetComponent<Rigidbody>();

        //modificar la gravedad del motor   
        //se podría hacer con otras componentes del motor
        //si se deja de esta forma cada vez que se arranca en el start se multiplica
        // Physics.gravity *= gravityMultiplier;
        Physics.gravity = gravityMultiplier * new Vector3(0,-9.81f,0);

        _animator = GetComponent<Animator>();
        _animator.SetFloat(SPEED_F, 1.0f);

        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime / 10;
        // este se queda porque no se reinicia el time
        //_animator.SetFloat(SPEED_MULTIPLIER, 1+Time.time/10);
        _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !_gameOver)
		{
            trail.Stop();
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            _animator.SetTrigger(JUMP_TRIGGER);

            //reproduce clip de audio 1 vez y ajusta el volumen
            _audioSource.PlayOneShot(jumpSound, audioVolume);
        }
    }

	private void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!_gameOver)
            {
                isOnGround = true;
                trail.Play();
            }            
        } 
        else if (other.gameObject.CompareTag("Obstacle"))
		{          
            _gameOver = true;
            Debug.Log("GAME OVER");

            trail.Stop();

            explosion.Play();

            _animator.SetInteger(DEATH_TYPE, Random.Range(1,3));
            _animator.SetBool(DEATH_B, true);

            //reproduce clip de audio 1 vez y ajusta el volumen
            _audioSource.PlayOneShot(crashSound, audioVolume);

            Invoke("RestartGame", 3.0f);
        }
	}

    void RestartGame()
	{
        speedMultiplier = 1;
        //SceneManager.UnloadSceneAsync("Prototype 3");
        SceneManager.LoadSceneAsync("Prototype 3",LoadSceneMode.Single);        
	}
}
