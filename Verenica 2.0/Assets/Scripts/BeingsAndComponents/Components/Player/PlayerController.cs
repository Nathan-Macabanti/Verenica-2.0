using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public InputAction playerControls;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(this.TryGetComponent<PlayerMovement>(out PlayerMovement _playerMovement))
        {
            this.playerMovement = _playerMovement;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            throw new System.Exception("No Keyboard");
        }

        if (keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame)
        {
            playerMovement.Move(0);
        }
        else if(keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame)
        {
            playerMovement.Move(1);
        }
        else if(keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame)
        {
            playerMovement.Move(2);
        }

        if(keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame)
        {
            playerMovement.Jump();
        }
    }
}
