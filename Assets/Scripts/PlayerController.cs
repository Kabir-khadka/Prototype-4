using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;

    private GameObject focalPoint; 
    private Rigidbody playerRb;

    private bool hasPowerUp = false;//


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Soaking the rigidbody component of the player in order to make them move
        playerRb = GetComponent<Rigidbody>();

        //Getting reference for the Focal Point gameobject so that we can use its values 
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {

        //Getting the vertical input for the player
        float forwardInput = Input.GetAxis("Vertical");

        //Moving the player in forward direction by adding force to the rigid body
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        
    }

    private void OnCollisionEnter(Collider other)
    {

        //creating logic uisng if statement to check the collision with the powerup
        if (other.CompareTag("Powerup"))
        {

            hasPowerUp = true;
            Destroy(other.gameObject);
        }

    }
}
