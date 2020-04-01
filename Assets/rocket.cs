using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // El music ele 7attha dakhlt gwa l audiosource 3lshan anaa 3arfto 3la el AudioSource
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private  void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))

        {
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
            audioSource.Play();
            
            }
            

        }
        else
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
            

        }

       
 }  

