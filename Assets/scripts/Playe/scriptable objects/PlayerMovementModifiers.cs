using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "PlayerMovementOptions", order = 1)]
public class PlayerMovementModifiers : ScriptableObject
{
    public float playerMass = 0.5f;
    public float playerDrag = 4;
    public float playerAngularDrag = 4;
    public float moveForce = 1.35f;
    public float jumpForce = 20;
    public float playerGravity = -0.75f;
    public LayerMask Jumpable;
    public float groundDistance = 0.25f;
    public float groundingForce = 0.05f;
    public float maxAngle = 20;
    public float PositionCompensationDamping = 0.25f;
    public float PostionSnapThreshold = 10;
    public float SyncInterval = 100f;
}
