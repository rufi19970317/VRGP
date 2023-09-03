using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    private MoveCamera _camera;
    bool isWalk = false;

    private WeaponAssultRifle weapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = GetComponent<MoveCamera>();

        // 마우스 고정
        Cursor.lockState = CursorLockMode.Locked;

        weapon = GetComponentInChildren<WeaponAssultRifle>();
    }

    float x, z;

    void Update()
    {
        // 마우스 및 카메라
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isWalk = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isWalk = false;

        // 움직임
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        UpdateWeaponAction();
    }

    void FixedUpdate()
    {
        // 움직임
        Move();
        LimitMove();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _camera.UpdateRotate(mouseX, mouseY);
    }

    float moveSpeed = 30f;

    void Move()
    {
        Vector3 forDir = (transform.localRotation * Vector3.forward).normalized;
        Vector3 rigDir = (transform.localRotation * Vector3.right).normalized;

        rb.AddForce(((forDir * z) + (rigDir * x)) * moveSpeed, ForceMode.Impulse);
    }

    float maxSpeed = 0f;

    void LimitMove()
    {
        maxSpeed = isWalk ? 2.5f : 5f;

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void UpdateWeaponAction()
    {
        if (Input.GetMouseButtonDown(0))
            weapon.StartWeaponAction();
        else if (Input.GetMouseButtonUp(0))
            weapon.StopWeaponAction();

        if (Input.GetKeyDown(KeyCode.R))
            weapon.StartReload();
    }
}
