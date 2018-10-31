using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameHasEnded = false;

    [SerializeField]
    private float restartDelay = 1f;

    [SerializeField]
    private GameObject completeLevelUI;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform obstacle;

    [SerializeField]
    private Transform obstacle1;

    [SerializeField]
    private Transform obstacle2;

    [SerializeField]
    private Transform obstacle3;

    [SerializeField]
    private Transform obstacle4;

    [SerializeField]
    private Transform obstacle5;

    [SerializeField]
    private Transform obstacle6;

    [SerializeField]
    private Transform collectible;


    //[SerializeField]
    //private Transform end;

    //private bool endReached = false;



    private void Start () {
        InvokeRepeating("SpawnBlocks", 2f, 1f);
    }


    private void SpawnBlocks()
    {
        //return;
        //randomly generates the obstacles from 5 different configurations of blocks
        int randBlock = Random.Range(0, 7);
        float randPosition = Random.Range(-6f, 6f);
        float randPosition1 = Random.Range(-1.5f, 1.5f);
        float randPosition2 = Random.Range(-6f, 6f);
        //float randPosition2 = Random.Range(-1.5f, 1.5f);
        //Debug.Log("block"+randBlock);
        //Debug.Log("position"+randPosition);
       //randBlock = 4;
        //Debug.Log(randBlock);

        //Transform currentPlatform = FindObjectOfType<MovePlatforms>().getAppropriatePlatform();
        //Debug.Log(currentPlatform);
        switch (randBlock)
        {
            case 0:
                Instantiate(obstacle, new Vector3(randPosition, 0.5f, player.position.z + 90), Quaternion.identity);
                break;
            case 1:
                Instantiate(obstacle1, new Vector3(randPosition1, 0, player.position.z + 90), Quaternion.identity);
                break;
            case 2:
                Instantiate(obstacle2, new Vector3(randPosition1, 0, player.position.z + 90), Quaternion.identity);
                break;
            case 3:
                Instantiate(obstacle3, new Vector3(randPosition1, 0, player.position.z + 90), Quaternion.identity);
                break;
            case 4:
                Instantiate(obstacle4, new Vector3(randPosition1, 0, player.position.z + 90), Quaternion.identity);
                break;
            case 5:
                Instantiate(obstacle5, new Vector3(randPosition1, 0, player.position.z + 90), Quaternion.identity);
                break;
            case 6:
                Instantiate(obstacle6, new Vector3(randPosition1, 0, player.position.z + 90), Quaternion.identity);
                break;
        }

        if (randBlock == 4) {
            if (randPosition2 >= randPosition1 + 1.5f || randPosition2 <= randPosition1 - 1.5f) {
                Instantiate(collectible, new Vector3(randPosition2, 3, player.position.z + 90), Quaternion.identity);
            } else {
                Instantiate(collectible, new Vector3(randPosition1 - 2f, 3, player.position.z + 90), Quaternion.identity);
            }
        } else if (randBlock == 6){
            if ((randPosition2 >= randPosition1 - 1f && randPosition2 <= randPosition1 + 3f) || (randPosition2 <= randPosition1 - 3.5f) || (randPosition2 >= randPosition1 + 6.5f)) {
                Instantiate(collectible, new Vector3(randPosition2, 3, player.position.z + 90), Quaternion.identity);
            } else {
                Instantiate(collectible, new Vector3(randPosition1, 3, player.position.z + 90), Quaternion.identity);
            }
        } else {
            Instantiate(collectible, new Vector3(randPosition2, 3, player.position.z + 90), Quaternion.identity);
        }
    }

    public void CompleteLevel () {
        completeLevelUI.SetActive(true);
    }

    public void EndGame () {
        //if the buildindex is 0 then you should get the next one
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            gameHasEnded = true;
            Invoke("LoadNextLevel", restartDelay);
        }
        if (gameHasEnded == false) {
            gameHasEnded = true;
            Debug.Log("game over");
            Invoke("Restart", restartDelay);
        }
    }

    private void Restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
