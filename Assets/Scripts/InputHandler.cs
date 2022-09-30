using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static InputHandler instance;

    private static bool controlsActive = true;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static InputHandler GetInstance()
    {
        return instance;
    }

    void Update()
    {
        PlayerController currentPlayer = TurnManager.GetCurrentPlayer();

        //Aiming
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentPlayer.Aim(hit.point);
        }

        if (controlsActive)
        {
            //Movement
            float forwardBackward = Input.GetAxis("Vertical") * Time.deltaTime;
            float turning = Input.GetAxis("Horizontal") * Time.deltaTime;

            currentPlayer.Move(forwardBackward, turning);

            //Action controls
            if (Input.GetMouseButtonUp(0))
            {
                currentPlayer.Fire();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentPlayer.Jump();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                TurnManager.SwapTurns();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentPlayer.ToggleWeapons();
            }
            if(Input.GetMouseButton(0))
            {
                currentPlayer.HoldDownFire();
            }
        }

}

    public static void SetControlsActive(bool state)
    {
        controlsActive = state;
    }

}
