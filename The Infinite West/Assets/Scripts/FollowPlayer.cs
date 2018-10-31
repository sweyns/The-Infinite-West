using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [SerializeField]
    private Transform player;
    [SerializeField]
    private float z_offset;

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, 6f, player.position.z + z_offset);
	}
}
