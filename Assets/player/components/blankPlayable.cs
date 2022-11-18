using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using UnityEngine;

public class blankPlayable : MonoBehaviour, iPlayable
{

    private Player myPlayer;

    private PlayerInput input;
    private InputAction lookingAction;



    [SerializeField] private float sens = 0.2f;
    [SerializeField] private float speed = 0.2f;
    //private Vector2 inputDirection;
    private float yMouseInput = 0;
    private float xMouseInput = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        handleMouseInput();
    }


     private void handleMouseInput()
    {
        float MouseX = 0;
        float MouseY = 0;

        
        /*
        if (input.currentControlScheme != null)
        {
            if (input.currentControlScheme.Equals("gamepad"))
            {
                MouseX = controls.Player.looking.ReadValue<Vector2>().x * sens * Time.deltaTime;
                MouseY = controls.Player.looking.ReadValue<Vector2>().y * sens * Time.deltaTime;
            }
            else
            {
                MouseX = controls.Player.looking.ReadValue<Vector2>().x * sens * Time.deltaTime;
                MouseY = controls.Player.looking.ReadValue<Vector2>().y * sens * Time.deltaTime;
            }
        }
        */


        MouseX = lookingAction.ReadValue<Vector2>().x * sens * Time.deltaTime;
        MouseY = lookingAction.ReadValue<Vector2>().y * sens * Time.deltaTime;

        yMouseInput -= MouseY;
        yMouseInput = Mathf.Clamp(yMouseInput, -90f, 90f);
        xMouseInput -= MouseX;


        float yMouseTotal = yMouseInput;
        float xMouseTotal = xMouseInput;

        transform.rotation = Quaternion.Euler((yMouseTotal * Vector3.right) + (Vector3.up * -xMouseTotal));
    }

    public void addPlayer(Player newPlayer)
    {
        if(myPlayer != null)
        {
            removePlayer();
        }
        myPlayer = newPlayer;
    }

    public Player removePlayer()
    {
        DeactivatePlayer();
        detatchPlayer();
        return myPlayer = null;
    }

    public void attatchPlayer()
    {
        myPlayer.transform.SetParent(this.transform);
    }

    public void detatchPlayer()
    {
        myPlayer.transform.SetParent(null);
    }

    public Player ActivatePlayer()
    {
        attatchPlayer();
        myPlayer.setCamRect();
        myPlayer.setLayers();
        input = myPlayer.getPlayerInput();
        bindControls();
        this.gameObject.SetActive(true);
        return myPlayer;
    }

    public Player DeactivatePlayer()
    {
        this.gameObject.SetActive(false);
        return myPlayer;
    }



    private void bindControls()
    {
        //input.onActionTriggered += handleMouseInput;
        lookingAction = input.actions.FindAction("looking");
        lookingAction.Enable();
    }
}
