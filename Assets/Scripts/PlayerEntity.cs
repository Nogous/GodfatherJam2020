using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public float moveSpeed = 1f;

    public Rigidbody2D rb;
    public Camera cam;

    public float offsetAngle = 0f;

    private Vector2 movement;
    private Vector2 mousePos;

    private void Update()
    {
        SetMovement();

        SetOrientation();
    }

    private void FixedUpdate()
    {
        DoActionMove();

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
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Orientate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offsetAngle;
        rb.rotation = angle;
    }
    #endregion
}
