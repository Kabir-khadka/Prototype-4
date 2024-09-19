using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float rotationSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the key pad values
        float horizontalInput = Input.GetAxis("Horizontal");

        //Rotating the cam when ever player uses horizontal inputs
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        
    }
}
