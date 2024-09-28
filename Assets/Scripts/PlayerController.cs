using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PowerupType currentPowerup = PowerupType.none;

    public float speed = 5.0f;
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;

    private Coroutine powerupCountdown;

    private float sentAwayForce = 15.0f;
    private GameObject focalPoint; 
    private Rigidbody playerRb;

    public bool hasPowerUp = false;//creating a bool variable to check the collision condition.

    //for smash powerupType

    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    bool smashing = false;
    float floorY;


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

        //Creating a if logic to check the current powerup type to be rockets when pressed F button
        if (currentPowerup == PowerupType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerup == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //creating logic uisng if statement to check the collision with the powerup
        if (other.CompareTag("Powerup"))
        {

            hasPowerUp = true;
            currentPowerup = other.gameObject.GetComponent<PowerUp>().powerupType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
           powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerup = PowerupType.none;
        powerupIndicator.gameObject.SetActive(false);
    }


    //using this method to check for the condition to dectect the collisoin with enemy.
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerupType.Pushback)
        {

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(awayFromPlayer * sentAwayForce, ForceMode.Impulse);
            Debug.Log("Player collided with " + collision.gameObject.name + " with powerup set to " + currentPowerup.ToString());
        }

    }

    //Method for finding each object in the scene which have Enemy script attached to it and setting them a target.
    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);

            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        //Store the y position befor taking off
        floorY = transform.position.y;

        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;

        //setting the maximum y position using floor y
        float maxY = floorY + 5.0f;

        while (Time.time < jumpTime && transform.position.y < maxY)
        {

            
                //Move the player up while still keeping their x velocity
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, smashSpeed);
                yield return null;
            

            
        }

        //Now move the player down
        while (transform.position.y > floorY)
        {

            
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -smashSpeed * 2);
            yield return null;
            
        }

        //Cycle through all enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            //Apply an explosion force that originates from our position

            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }

            //We are no loonger smashing so set the smashing boolean to false
            smashing = false;
        }

    }
}
