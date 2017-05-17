using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {

	public Arrow projectile;

	protected override void OnAttackHit ()
	{
		Arrow newArrow = ObjectPooler.Spawn<Arrow> (projectile, transform.position);
		newArrow.Init (castle, attackDamage);
	}

}
