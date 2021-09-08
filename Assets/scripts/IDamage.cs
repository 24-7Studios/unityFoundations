using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public interface IDamage
{

	float health { get; set; }
	float shields { get; set; }
	bool instantDeath { get; set; }

	void die();

	void takeDamagefromHit(float baseDamage, float fleshMulitplier);

	void hit();




}
