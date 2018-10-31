using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementTutorial : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float sidewaysForce = 500f;
    [SerializeField]
    private float upwardForce = 2f;

    [SerializeField]
    private GameObject jumpHelp;

    [SerializeField]
    private GameObject collectHelp;

    [SerializeField]
    private MovePlatforms platformMovement;

    [SerializeField]
    private GameManager manager;

    private bool onGround = true;
    private bool shouldJump = false;
    private float lastJumpedTime;

    public bool stage1 = true;
    

    //gettime only reset if 
    private void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) {
            shouldJump = true;
            lastJumpedTime = Time.time;
        }
        ////checks for sides

    }

    IEnumerator jumpHelpStage() {
        //manager.enabled = false;
        yield return StartCoroutine(Wait(2.1f));
        platformMovement.endHappened = true;
        //freeze everything
        jumpHelp.SetActive(true);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));
        rb.AddForce(0, 10, 0, ForceMode.Impulse);
        onGround = false;
        shouldJump = false;
        jumpHelp.SetActive(false);
        platformMovement.endHappened = false;
        Debug.Log("key pressed");
        yield return StartCoroutine(Wait(2f));
        collectHelp.SetActive(true);
        stage1 = false;
        yield return StartCoroutine(Wait(3f));
        collectHelp.SetActive(false);
        manager.enabled = true;


        //start spawning shit
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(keyCode));
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //Debug.Log("waited");
        //StartCoroutine(WaitForKeyDown(KeyCode.Space));
    }

    private void Start()
    {
        rb.centerOfMass = Vector3.zero;
        //.enabled = false;
        StartCoroutine(jumpHelpStage());
        //Invoke("DisplayJumpHelp", 2.6f);
    }

    void DisplayJumpHelp() {
        //wait until
        //display jump help
        Debug.Log("hey");
        jumpHelp.SetActive(true);
        StartCoroutine(WaitForKeyDown(KeyCode.Space));
        //yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));
        //Debug.Log("hey");
        //invoke the next thing after another second
    }

    //IEnumerator WaitForJump() {
    //    Debug.Log("waitingforJump");
    //    yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));
    //}
    // Update is called once per frame
    void FixedUpdate () {

        if (!stage1)
        {
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
            if (Input.GetKey("d"))
            {
                //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }

            if (Input.GetKey("a"))
            {
                //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            //if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            //}

            if (rb.position.y < -1f)
            {
                FindObjectOfType<GameManager>().EndGame();
            }



            //jump
            if (shouldJump)
            {
                rb.AddForce(0, upwardForce * Time.deltaTime, 0, ForceMode.Impulse);
                onGround = false;
                shouldJump = false;
            }






            transform.LookAt(rb.velocity + new Vector3(0, 0, 40.0f) + transform.position);
        }
        //Debug.Log(rb.velocity);
    }

    private void OnCollisionStay (Collision collisionInfo) {
        if ((collisionInfo.collider.tag == "Floor1" || collisionInfo.collider.tag == "Floor2") && (Time.time - lastJumpedTime) > 0.2f) {
            //Debug.Log("happening");
            onGround = true;
        }
    }
}
