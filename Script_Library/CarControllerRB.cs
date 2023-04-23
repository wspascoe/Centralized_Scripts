using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerRB : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180f, gravitForce = 10f;

    private bool grounded;

    private float speedInput, turnInput;
    // Start is called before the first frame update
    void Start()
    {
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;

        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        transform.position = rb.transform.position;
    }

    private void FixedUpdate()
    {
       if(Mathf.Abs(speedInput) > 0)
        {
            rb.AddForce(transform.forward * speedInput);
        }
    }
}
