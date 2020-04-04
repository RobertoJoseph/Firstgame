using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] AudioClip thrust;
    [SerializeField] AudioClip sucess;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem sucessParticles;
    [SerializeField] ParticleSystem deathParticles;

    new Rigidbody rigidbody;
    AudioSource audioSource;
    enum State { Alive, Dying, Loading };
    State state;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // El music ele 7attha dakhlt gwa l audiosource 3lshan anaa 3arfto 3la el AudioSource

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();



        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSucessSequence();
                break;
            default:
                StartDeathSequence();
                break;


        }
        void StartSucessSequence()
        {

            state = State.Loading;
            audioSource.Stop();
            audioSource.PlayOneShot(sucess);
            sucessParticles.Play();
            
            print("lol");
            Invoke("LoadNextLevel", 1f);



        }
        void StartDeathSequence()


        {
            state = State.Dying;
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathParticles.Play();
            Invoke("LoadFirstLevel", 1f); // parameterise time
        }

    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
    void RespondToThrustInput()

    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {
            audioSource.Stop();
            thrustParticles.Stop();
        }


    }
    void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrust);

            Debug.Log("lol");
            thrustParticles.Play();
        }
       





    }
    void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true;
      
        ApplyRotate();
    }


        void ApplyRotate()
        {
        float rotationSpeed = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotationSpeed);
            }



            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationSpeed);
            }
        }
   
    
}