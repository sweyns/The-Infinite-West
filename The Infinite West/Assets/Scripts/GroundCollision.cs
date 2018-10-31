using UnityEngine;

public class GroundCollision : MonoBehaviour {

    private void OnCollisionEnter(Collision collisionInfo) {
        if (collisionInfo.collider.tag == "Floor1" || collisionInfo.collider.tag == "Floor2") {
            Debug.Log(collisionInfo.collider.transform);
            Transform obstacleParent = transform.parent;
            if (obstacleParent != null) {
                obstacleParent.parent = collisionInfo.collider.transform;
            } else {
                transform.parent = collisionInfo.collider.transform;
            }
        }
    }
}
