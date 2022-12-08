using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "LayersPlayer", order = 1)]
public class LayersPlayer : ScriptableObject
{
    public int p1vm;
    public int p1pm;
    public int p1hb;
    public LayerMask p1vmC;
    public LayerMask p1wmC;
    public LayerMask p1sh;

    public int p2vm;
    public int p2pm;
    public int p2hb;
    public LayerMask p2vmC;
    public LayerMask p2wmC;
    public LayerMask p2sh;

    public int p3vm;
    public int p3pm;
    public int p3hb;
    public LayerMask p3vmC;
    public LayerMask p3wmC;
    public LayerMask p3sh;

    public int p4vm;
    public int p4pm;
    public int p4hb;
    public LayerMask p4vmC;
    public LayerMask p4wmC;
    public LayerMask p4sh;
}
