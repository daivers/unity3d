using UnityEngine;
using System.Collections;

public class WayDestroy : MonoBehaviour {

	void OnTriggerStay( Collider other )
	{    
		if (other.gameObject.tag == "Player") { 
			Destroy (gameObject);
		}


}
}