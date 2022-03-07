using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "PlayerHealthOptions", order = 1)]
public class PlayerHealthModifiers : ScriptableObject
{

    public float defaultHealth = 100;
    public float maxHealth = 100;
    public float defaultShields = 100;
    public float maxShields = 100;

}
