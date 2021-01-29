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

    float speed = 3.5f;

    /// <summary>
    /// You know what this is
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        velocities = new Dictionary<string, Vector2>();
        velocities["PlayerControl|PlayerMovement"] = new Vector2(0, 0);
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
        Vector2 totalVelocity = Vector2.zero;

        foreach(Vector2 vb in velocities.Values)
        {
            totalVelocity += vb;
        }

        if (totalVelocity.magnitude > speed * 2f)
        {
            totalVelocity = totalVelocity * speed;
        }
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) <= 0.125f && Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0.125f)
        {
            totalVelocity = Vector2.zero;
        }

        rb.velocity = totalVelocity;
    }

    void HandleMovement()
    {
        moveVec = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))).normalized;

        velocities["PlayerControl|PlayerMovement"] = moveVec * speed;
    }

    void UpdateGunPosition()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;

        Vector3 vecDiff = (cursorPos - transform.position).normalized;

        GunRef.position = transform.position + vecDiff * 0.65f;

        GunRef.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vecDiff.y, vecDiff.x) * Mathf.Rad2Deg);
    }
}
