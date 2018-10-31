using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Text scoreText;

	// Update is called once per frame

    public void setText(string str) {
        scoreText.text = str;
    }
}
