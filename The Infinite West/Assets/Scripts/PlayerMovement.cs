
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float sidewaysForce = 500f;
    [SerializeField]
    private float upwardForce = 2f;

    private bool onGround = true;
    private bool shouldJump = false;
    private float lastJumpedTime;


    //gettime only reset if 
    private void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) {
            shouldJump = true;
            lastJumpedTime = Time.time;
        }
        ////checks for sides

    }

    private void Start()
    {
        rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {

        //Debug.Log(onGround);

        if (transform.position.x > 5.8f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            transform.position = new Vector3(5.80005f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -5.8f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            transform.position = new Vector3(-5.8f, transform.position.y, transform.position.z);
        }
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        //make sure the car points forward and isnt rotated

       

        //left and right movement
        if (Input.GetKey("d")) {
            //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a")) {
            //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        //if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
        //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        //}

        if (rb.position.y < -1f) {
            FindObjectOfType<GameManager>().EndGame();
        }



        //jump
        if (shouldJump) {
            rb.AddForce(0, upwardForce * Time.deltaTime, 0, ForceMode.Impulse);
            onGround = false;
            shouldJump = false;
        }

       



        transform.LookAt(rb.velocity + new Vector3(0, 0, 40.0f) + transform.position);

        //Debug.Log(rb.velocity);
    }

    private void OnCollisionStay (Collision collisionInfo) {
        if ((collisionInfo.collider.tag == "Floor1" || collisionInfo.collider.tag == "Floor2") && (Time.time - lastJumpedTime) > 0.2f) {
            //Debug.Log("happening");
            onGround = true;
        }
    }
}
