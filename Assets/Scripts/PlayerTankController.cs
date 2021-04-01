using UnityEngine;

public class PlayerTankController : MonoBehaviour {

    bool domeShieldActive;
    Vector3 placementPosition;
    [SerializeField] GameObject domeShieldObject;
    GameObject domeShieldInstance;
    // PlayerPauseManager pauseMenu;

    void Start() {
        // pauseMenu = GetComponent<PlayerPauseManager>();
    }

    void Update() {
        // if (!pauseMenu.gameIsPaused) {
        HandleActions();
        // }
        if (domeShieldActive) {
            // Deplete mana on maintaining shield, initial casting, or both?
            // Leaning on just initial casting
            //mana -= Time.deltaTime;?
        }
    }

    void HandleActions() {
        if (Input.GetMouseButton(0)) {
            LayerMask mask = LayerMask.GetMask("TankUI");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f, mask)) {
                // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 3f);
                // Debug.Log("Did Hit");

                if (hit.point.y >= 0) { // Remove if you ever make the sphere just a dome
                    SetShieldPosition(hit.point);
                    PlaceShield();
                }
            }
        }
    }

    private void PlaceShield() {
        if (domeShieldInstance == null) {
            domeShieldInstance = GameObject.Instantiate(domeShieldObject, placementPosition, transform.rotation);
        } else {
            domeShieldInstance.GetComponent<DomeShield>().SetShieldPosition(placementPosition);
        }
    }

    public void SetShieldPosition(Vector3 position) {
        domeShieldActive = true;
        placementPosition = position;
    }
}