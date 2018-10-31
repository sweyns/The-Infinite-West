using UnityEngine;

public class OnBoxTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider collider) 
    {
        Debug.Log(collider.transform);
        if (collider.tag == "Box1" || collider.tag == "Box2") {
            //Debug.Log("coinattached");
            Transform parent = transform.parent;
            if (parent != null) {
                parent.parent = collider.transform;
            } else {
                transform.parent = collider.transform;
            }
        }
    }
}
