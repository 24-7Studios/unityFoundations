using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Data", menuName = "PlayerStuffOptions", order = 1)]
public class PlayerStuffScriptableObject : ScriptableObject
{
    public localPlayerOptions playerOptions;
    public PlayerMovementModifiers playerMovement;
    public PlayerHealthModifiers playerHealth;
}
