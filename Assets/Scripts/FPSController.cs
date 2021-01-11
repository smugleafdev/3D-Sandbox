using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class KeyCodeIntPair {
    [SerializeField]
    public KeyCode keyCode;
    [SerializeField]
    public int keyInt;
}

public class FPSController : MonoBehaviour {

    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject flyText;
    int equippedSlot;
    // bool equippedSlotChangedFlag = false;
    bool pausedFlag = false;
    ElementalSpellManager spellEmitterManager;
    PlayerPauseManager pauseMenu;

    [SerializeField] float walkingSpeed = 7.5f;
    [SerializeField] float runningSpeed = 11.5f;
    [SerializeField] float jumpSpeed = 8.0f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    [SerializeField] KeyCodeIntPair[] equipKeyCodes;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    bool canMove = true;
    // bool doubleTapped = false;

    float accuracy;
    int shotsFired, shotsHit;

    void Start() {
        pauseMenu = GetComponent<PlayerPauseManager>();
        characterController = GetComponent<CharacterController>();
        spellEmitterManager = transform.GetComponentInChildren<ElementalSpellManager>();

        LockCursor();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            pauseMenu.HandlePause();
        }
        HandleCursor();

        if (!pauseMenu.gameIsPaused) {
            HandleMovement();
            HandleSpells();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        CalculateEnemyAccuracy();
    }

    void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HandleCursor() {
        if (pausedFlag != pauseMenu.gameIsPaused) {
            pausedFlag = pauseMenu.gameIsPaused;
            if (pausedFlag) {
                UnlockCursor();
            } else {
                LockCursor();
            }
        }
    }

    void HandleMovement() {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) {
            moveDirection.y = jumpSpeed;
        } else {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void HandleSpells() {
        int currentEquipSlot = equippedSlot;
        equippedSlot = equipKeyCodes.FirstOrDefault(ekc => Input.GetKeyDown(ekc.keyCode))?.keyInt ?? -1;
        if (currentEquipSlot != equippedSlot && equippedSlot != -1) {
            spellEmitterManager.Equip(equippedSlot);
        }

        if (Input.GetMouseButtonDown(1)) {
            spellEmitterManager.Fire();
        }
    }

    public void DamagePlayer(int damage) {
        // TODO: Player health, dying, respawning, etc
        // currentHealth -= damage;
        ObjectUtils.ShowFlyText(flyText, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), $"-{damage}");
    }

    void CalculateEnemyAccuracy() {
        bool printLine = false;
        if (shotsFired != ObjectUtils.shotsFired || shotsHit != ObjectUtils.shotsHit) printLine = true;
        shotsFired = ObjectUtils.shotsFired;
        shotsHit = ObjectUtils.shotsHit;

        if (shotsFired > 0 && shotsHit > 0) {
            accuracy = (float)shotsHit / (float)shotsFired * 100f;
            Debug.Log(accuracy.ToString("F2"));
            if (printLine) {
                Debug.Log(accuracy.ToString("F2"));
            }
        }
    }

    float offset = 12f;
    private void OnGUI() {
        GUI.Label(new Rect(10, offset * 1, 100, 20), $"Shots fired: {shotsFired}");
        GUI.Label(new Rect(10, offset * 2, 100, 20), $"Shots hit: {shotsHit}");
        GUI.Label(new Rect(10, offset * 3, 1000, 20), $"Accuracy: {accuracy.ToString("F2")}%");
    }

    // Double tap example here in case I forget and need it later
    // if (Input.GetKeyDown(KeyCode.Escape) {
    //     if (!doubleTapped) {
    //         StartCoroutine(DoubleTapDelayTimer());
    //     } else {
    //         UnityEditor.EditorApplication.isPlaying = false;
    //     }
    // }

    // IEnumerator DoubleTapDelayTimer() {
    //     doubleTapped = true;
    //     yield return new WaitForSeconds(0.25f);
    //     doubleTapped = false;
    // }
}