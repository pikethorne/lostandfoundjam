using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for controlling the fucking player hell yeah
/// </summary>
[System.Serializable]
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Different sources of velocity changes
    /// </summary>
    Dictionary<string, Vector2> velocities;

    /// <summary>
    /// Reference for the part of the player
    /// </summary>
    #region Body Parts
    public Transform HeadRef, BodyRef, ArmRef, GunRef;
    #endregion

    /// <summary>
    /// Rigidbody attached to gameObject
    /// </summary>
    private Rigidbody2D rb;

    Vector2 moveVec;

    float speed = 15f;

    /// <summary>
    /// You know what this is
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Ypdate
    /// </summary>
    void Update()
    {
        UpdateGunPosition();
        HandleMovement();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
        else if (moveVec.magnitude <= 0.025f)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void HandleMovement()
    {
        moveVec = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))).normalized;

        rb.velocity += moveVec * speed * Time.deltaTime;
    }

    void UpdateGunPosition()
    {
        Vector3 cursorPos = (Input.mousePosition - (new Vector3(Screen.width, Screen.height)) / 2f).normalized;

        GunRef.position = transform.position + cursorPos * 0.65f;

        GunRef.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(cursorPos.y, cursorPos.x) * Mathf.Rad2Deg);
    }
}
