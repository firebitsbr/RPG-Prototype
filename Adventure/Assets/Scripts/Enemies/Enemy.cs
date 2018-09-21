using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	#region Variables
	public int health = 1;
	#endregion

	#region UnityMethods

	#endregion

	public virtual void Hit()
	{
		health--;
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider otherCollider)
	{
		if(otherCollider.GetComponent<Sword>() != null)
		{
			Hit();
		}
		else if(otherCollider.GetComponent<Arrow>() != null)
		{
			Hit();
			Destroy(otherCollider.gameObject);
		}
	}

}// Main Class
