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
    /// Reference for the part of the player
    /// </summary>
    #region Body Parts
        public Transform HeadRef, BodyRef, ArmRef, GunRef;
    #endregion

    /// <summary>
    /// You know what this is
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Ypdate
    /// </summary>
    void Update()
    {
        UpdateGunPosition();
    }

    void UpdateGunPosition()
    {
        Vector3 cursorPos = (Input.mousePosition - (new Vector3(Screen.width, Screen.height))/2f).normalized;

        GunRef.position = transform.position + cursorPos * 0.65f;

        GunRef.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(cursorPos.y, cursorPos.x) * Mathf.Rad2Deg);
    }
}
