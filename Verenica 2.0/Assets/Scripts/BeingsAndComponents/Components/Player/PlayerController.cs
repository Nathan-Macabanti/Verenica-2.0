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
        if(this.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            this.playerMovement = playerMovement;
        }

        if(this.playerMovement != null)
        {
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.Slide(playerControls.ReadValue<int>());
    }
}
