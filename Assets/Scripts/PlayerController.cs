using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    public GameObject powerupIndicator;

    private float sentAwayForce = 15.0f;
    private GameObject focalPoint; 
    private Rigidbody playerRb;

    public bool hasPowerUp = false;//creating a bool variable to check the collision condition.


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

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.58f, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //creating logic uisng if statement to check the collision with the powerup
        if (other.CompareTag("Powerup"))
        {

            hasPowerUp = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }


    //using this method to check for the condition to dectect the collisoin with enemy.
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * sentAwayForce, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + (" with powerup set to " + hasPowerUp));
        }

    }
}
