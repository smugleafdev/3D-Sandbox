﻿using System;
using System.Collections;
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

    public Camera playerCamera;
    private int equippedSlot;
    private bool equippedSlotChangedFlag = false;
    private ElementalSpellManager spellEmitterManager;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public KeyCodeIntPair[] equipKeyCodes;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    private bool doubleTapped = false;

    void Start() {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        spellEmitterManager = transform.GetComponentInChildren<ElementalSpellManager>();
    }

    void Update() {

        HandleMovement();

        int currentEquipSlot = equippedSlot;
        equippedSlot = equipKeyCodes.FirstOrDefault(ekc => Input.GetKeyDown(ekc.keyCode))?.keyInt ?? -1;
        if (currentEquipSlot != equippedSlot && equippedSlot != -1) {
            spellEmitterManager.Equip(equippedSlot);
        }

        if (Input.GetMouseButtonDown(1)) {
            spellEmitterManager.Fire();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!doubleTapped) {
                StartCoroutine(DoubleTapDelayTimer());
            } else {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

    private void HandleMovement() {
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

    IEnumerator DoubleTapDelayTimer() {
        doubleTapped = true;
        yield return new WaitForSeconds(0.25f);
        doubleTapped = false;
    }
}