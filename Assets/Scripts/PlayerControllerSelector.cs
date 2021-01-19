using UnityEngine;

public class PlayerControllerSelector : MonoBehaviour {

    [SerializeField]
    bool playerIsVr;
    [SerializeField]
    GameObject vrController, mkController;

    void Start() {
        GameObject playerController;
        if (playerIsVr) {
            playerController = GameObject.Instantiate(vrController, transform.position, transform.rotation);
        } else {
            playerController = GameObject.Instantiate(mkController, transform.position, transform.rotation);
        }
        // playerController.transform.SetParent(transform);
        transform.SetParent(playerController.transform);
    }
}