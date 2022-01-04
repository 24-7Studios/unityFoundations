using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public interface IDamage
{


	void die();

	void takeDamagefromHit(float baseDamage, float fleshMulitplier);

	void hit();



}
