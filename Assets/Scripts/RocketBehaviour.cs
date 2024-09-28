using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{

    private Transform target;
    private float speed = 15.0f;
    private bool homing;

    public float rocketStrength = 15.0f;
    public float aliveTimer = 5.0f;
    //private Transform homingTarget;


    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {

            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            transform.LookAt(target);
        }
    }

    //This method will take a transform and set it as a target.
    public void Fire(Transform newTarget)
    {

        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }


    //Makes the collided target object run away from the rockets object or objects this script is attached to
    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();

                Vector3 away = -collision.contacts[0].normal;

                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);

                Destroy(gameObject);
            }
        }
    }
}
