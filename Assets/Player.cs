using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{
    [SerializeField]
    private CharacterController controller;
    protected PlayerInputMap pim;
    private InputAction move;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 cameraOffset = new Vector3(0.0f, 2.0f, 0.0f);
    private Vector3 playerTranslation = Vector3.zero;
    private Quaternion facingDirection;

    protected GameObject camRig;
    protected GameObject model;


    private void Awake()
    {
        pim = new PlayerInputMap();
    }

    private void OnEnable()
    {
        move = pim.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Start()
    {
        camRig = transform.Find("PlayerCameraRig").gameObject;
        camRig.transform.SetParent(null, true);

        model = transform.Find("PlayerModel").gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    { 
        moveDirection = move.ReadValue<Vector2>();
        moveDirection = moveDirection.normalized;

        playerTranslation.x = moveDirection.x;
        playerTranslation.z = moveDirection.y;
        playerTranslation.y = 0.0f;
        playerTranslation.Normalize();

        if (moveDirection.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(playerTranslation.x, playerTranslation.z) * Mathf.Rad2Deg + camRig.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            playerTranslation = Quaternion.Euler(0f, camRig.transform.eulerAngles.y, 0f) * playerTranslation;
            playerTranslation.Normalize();
            controller.Move(playerTranslation * _speed * Time.deltaTime);
        }

        camRig.transform.SetPositionAndRotation(transform.position + cameraOffset, camRig.transform.rotation);
    }
}
