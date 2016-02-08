using UnityEngine;
using System.Collections;

public class levelUp : MonoBehaviour {

	public Player player;

	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.gameObject.name == "Player")
		{
			player.PlayerWin ();
		} 
	}
}


