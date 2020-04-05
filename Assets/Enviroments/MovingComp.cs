using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingComp : MonoBehaviour
{
    [SerializeField] Vector3 movementComponent; //mkan eele 3yz t7rko leh 
    [Range(0, 1)] [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;
    Vector3 startPostion;
  


    // Start is called once 
    void Start()
    {
        startPostion = transform.position; //intial place , el transform postion mkano l asasy l awl

        
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0) { return; }
        float cycles = Time.time / period;  // number of periods
        const float tau = Mathf.PI * 2f; //=2pi
        float rawSineWave = Mathf.Sin(cycles * tau); //goes from -1 to +1 , it doesnot matter
        movementFactor = rawSineWave / 2f + 0.5f; //to let movementFactor between 0 and +1
       
        Vector3 displacment = movementComponent * movementFactor; //el masafa ele hy5odha , displacment
        transform.position = startPostion + displacment; // el transform.postion 3obara 3n el bdaya which is intial distance + final distance

        
    }
}
