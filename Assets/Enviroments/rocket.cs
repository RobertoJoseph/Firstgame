using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] AudioClip thrust;
    [SerializeField] AudioClip sucess;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem sucessParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] float LoadLevelDelay = 1f;
    bool collisionDisabled = false;
    [SerializeField] GameObject myPrefab;
  
   

    public Rigidbody rg;
    AudioSource audioSource;
    enum State { Alive, Dying, Loading };
    State state;

    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>(); // El music ele 7attha dakhlt gwa l audiosource 3lshan anaa 3arfto 3la el AudioSource

    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.Alive) // da 3lshan lma ymot myyfdlsh mkml ene a2dr al3bb beh , f bystna lhd ma l state trg3 tany tt8yr lel intial
        {
            RespondToThrustInput();
            RespondToRotateInput();



        }
        if (Debug.isDebugBuild)
        {

      
        RespondToDebug();
    }

}

    
    private void RespondToDebug()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                collisionDisabled = !collisionDisabled;

            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       
        
        if (state != State.Alive||collisionDisabled)
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
            sucessParticles.Play(); // for particles
         
            
            Invoke("LoadNextLevel", LoadLevelDelay);
           



        }
        void StartDeathSequence()


        {
          
            state = State.Dying;
           
            
            audioSource.Stop();
           
            audioSource.PlayOneShot(death);
            thrustParticles.Stop(); // i added it
            deathParticles.Play();
         
          



            Invoke("LoadSameLevel", LoadLevelDelay);
           // parameterise time
        }

    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; //previous level
        print(currentScene);


        int nextScene = currentScene + 1; // lgded
        if (nextScene == SceneManager.sceneCountInBuildSettings) //3add l scenes
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void LoadSameLevel()
    {
        
        Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        

        
        int currentScsene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScsene);

    }
    
    void RespondToThrustInput()

    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {
            StopApplyingThrust();
        }


    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    void ApplyThrust()
    {
        rg.AddRelativeForce(Vector3.up * mainThrust*Time.deltaTime); //frame rate inpendent
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrust);

            Debug.Log("lol");
            thrustParticles.Play();
        }
       





    }
    void RespondToRotateInput()
    {
       
      
        ApplyRotate();
        
    }


      void ApplyRotate()
      {
      float rotationSpeed = rcsThrust * Time.deltaTime;
        rg.angularVelocity = Vector3.zero; //remove rotation due to physics
      if (Input.GetKey(KeyCode.A))
          {
             transform.Rotate(Vector3.forward * rotationSpeed);
          }



          if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationSpeed); //rule of left and hand
            }
        
    }
   
    
}