using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.gameObject.name == "Player")
		{
			Destroy(gameObject);
		} 
	}
}
