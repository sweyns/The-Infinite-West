using UnityEngine;
using System.Collections;

public class MovePlatforms : MonoBehaviour {

    [SerializeField]
    private Transform[] platforms = new Transform[2];

    [SerializeField]
    private GameObject collisionBox;

    private bool collisionActivated = false;

    private int currentPlatform;

    public bool endHappened = false;

    //private bool dampStarted = false;

    private float startTime = 0;

    private Vector3 pos1;
    private Vector3 pos2;

    void Start () {
        currentPlatform = 0;
        startTime = Time.time;

	}


    void FixedUpdate () {
        if (!endHappened)
        {
            //Debug.Log("going at speed");
            platforms[0].Translate(new Vector3(0, 0, -0.5f));
            platforms[1].Translate(new Vector3(0, 0, -0.5f));
            pos1 = platforms[0].position;
            pos2 = platforms[1].position;
        }

    }

    //public Transform getAppropriatePlatform() {
    //    return platforms[currentPlatform];
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Box1")
        {
            //Debug.Log("collision with floor 1");
            StartCoroutine(MovePlatform());
        }
        else if (other.tag == "Box2") {
            //Debug.Log("collision with floor 2");
            StartCoroutine(MovePlatform());
        }
    }

    IEnumerator MovePlatform() {
        yield return new WaitForSeconds(1f); 
        if (!collisionActivated)
        {
            collisionBox.SetActive(true);
            collisionActivated = true;
        }
        DestroyObjects();
        Transform currentPlatform_transform = platforms[currentPlatform];
        currentPlatform_transform.position = new Vector3(currentPlatform_transform.position.x, currentPlatform_transform.position.y, currentPlatform_transform.position.z + 300);
        currentPlatform = (currentPlatform == 1) ? 0 : 1;
    }


    private void DestroyObjects() {
        Transform obstacles = platforms[currentPlatform].GetChild(0);
        //Debug.Log("obstacle to remove from: " + obstacles);
        int children = obstacles.childCount;
        for (int i = children - 1; i >= 0; i--) {
            GameObject.Destroy(obstacles.GetChild(i).gameObject);
        }
        Transform collectibles = platforms[currentPlatform].GetChild(2);
        children = collectibles.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            GameObject.Destroy(collectibles.GetChild(i).gameObject);
        }

    }
}
