using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    protected PlayerInputMap pim;
    protected InputAction look;
    private float mouseSensitivity = 1.0f;

    private float minCameraAngle = -20.0f;
    private float maxCameraAngle = 60.0f;

    private float verticalAngle = 0.0f;
    private float horizontalAngle = 0.0f;

    public void Awake()
    {
        pim = new PlayerInputMap();
    }

    public void OnEnable()
    {
        look = pim.Player.Look;
        look.Enable();
    }

    public void OnDisable()
    {
        look.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 mouseMove = look.ReadValue<Vector2>();
        verticalAngle -= mouseMove.y * mouseSensitivity;
        horizontalAngle += mouseMove.x * mouseSensitivity;
        verticalAngle = Mathf.Clamp(verticalAngle, minCameraAngle, maxCameraAngle);

        transform.localRotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0.0f);
        // transform.localRotation = Quaternion.Euler(verticalAngle, 0.0f, 0.0f);
    }
}
