using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerOptions", order = 1)]
public class localPlayerOptions : ScriptableObject
{
    public float MouseSens = 10;
    public float controllerSens = 100;
    public float interactTime = 1;


    public bool cameraTilt = true;
    public float tiltAmount = 2;
    public float tiltSmoothing = 8;

    public bool viewmodelSway = true;
    public float viewmodelSwayFactor = 0.025f;
    public float viewmodelSwaySmoothing = 15;
    public bool viewmodelShift = true;
    public float viewmodelShiftFactor = 0.075f;
    public float viewmodelShiftSmoothing = 8;
}
