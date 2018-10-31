using UnityEngine;

public class EndTrigger : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter() {
        gameManager.CompleteLevel();
    }

    public void setGameManager(GameManager manager) {
        gameManager = manager;
    }
}
