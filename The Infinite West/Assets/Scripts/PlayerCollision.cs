using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerCollision : MonoBehaviour {

    //[SerializeField]
    //private Transform platform1;

    //[SerializeField]
    //private Transform platform2;
    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private MovePlatforms platformScript;

    [SerializeField]
    private Transform explosion;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text highScoreText;

    //private Vector3 position1;
    //private Vector3 position2;

    //private Vector3 velocity = new Vector3(0, 0, 40);

    // Update is called once per frame
    private void Start() {
        int highScore = PlayerPrefs.GetInt("Score");

        highScoreText.text = highScore.ToString();
        Debug.Log("last high score: " + highScore);
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Collectible")
        {
            Debug.Log("got coin");
            scoreText.text = (Int32.Parse(scoreText.text) + 1).ToString();
            Destroy(collider.transform.parent.gameObject);
            //update the scoreboard
            //play a sound
        }
        if (collider.tag == "Obstacle")
        {
            platformScript.endHappened = true;
            movement.enabled = false;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Destroy(collider.gameObject.GetComponent<Rigidbody>());
            Instantiate(explosion, transform.position, transform.rotation);
            gameObject.SetActive(false);
            //play a sound

            //slow down both platforms
            //position1 = platform1.position + new Vector3(0, 0, -5);
            //position2 = platform2.position + new Vector3(0, 0, -5);
            //platform1.position = Vector3.SmoothDamp(platform1.position, position1, ref velocity, 0.3f);
            //platform2.position = Vector3.SmoothDamp(platform2.position, position2, ref velocity, 0.3f);
            int currentScore = Int32.Parse(scoreText.text);
            if (currentScore > PlayerPrefs.GetInt("Score")) {
                PlayerPrefs.SetInt("Score", Int32.Parse(scoreText.text));
            }
           
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    //private void Update() {
    //    //platform1.position = Vector3.SmoothDamp(platform1.position, position, ref velocity, 0.3f);
    //    //platform2.position = Vector3.SmoothDamp(platform2.position, position, ref velocity, 0.3f);
    //}
}
