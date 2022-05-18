using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{
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
        facingDirection = Quaternion.AngleAxis(camRig.transform.localEulerAngles.y, Vector3.up);
        moveDirection = move.ReadValue<Vector2>();
        moveDirection = moveDirection.normalized * _speed * Time.deltaTime;
        playerTranslation.x = moveDirection.x;
        playerTranslation.z = moveDirection.y;

        playerTranslation = facingDirection * playerTranslation;
        transform.Translate(playerTranslation);

        camRig.transform.SetPositionAndRotation(transform.position + cameraOffset, camRig.transform.rotation);
    }
}
