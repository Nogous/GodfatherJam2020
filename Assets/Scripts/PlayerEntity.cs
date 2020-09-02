using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : MonoBehaviour
{
    private enum State
    {
        Normal,
        Attacking,
    }

    public float moveSpeed = 1f;

    public Rigidbody2D rb;
    public Camera cam;

    public float offsetAngle = 0f;

    private Vector2 movement;
    private Vector3 mousePos;
    private State state;

    private void Start()
    {
        state = State.Normal;
    }

    private void Update()
    {
        SetMovement();

        SetOrientation();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                DoActionMove();
                break;
            case State.Attacking:
                break;
            default:
                break;
        }

        Orientate();
    }

    #region Movement
    private void SetMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void DoActionMove()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    #endregion

    #region Orientation
    private void SetOrientation()
    {
        mousePos = GetMouseWorldPosition();
    }

    private void Orientate()
    {
        Vector2 lookDir = (Vector2)mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offsetAngle;
        rb.rotation = angle;
    }
    #endregion

    #region Attack
    private void SetAttack()
    {
        Vector3 attackDir = (mousePos - transform.position).normalized;
        state = State.Attacking;
        // play animation
    }
    private void DoActionAttack()
    {

    }
    #endregion

    #region tool
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWthZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWthZ()
    {
        return GetMouseWorldPositionWthZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWthZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWthZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWthZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    #endregion
}
