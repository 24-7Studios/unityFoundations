using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootBehaviour
{

    WeaponClass weapon { get; set; }

    public void shoot();
}
