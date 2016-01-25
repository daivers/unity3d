using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AcivmentButton : MonoBehaviour {
	public GameObject acivmentList;

	public Sprite neutral, highlight;

	private Image sprite;



	void Awake () {
	

		sprite = GetComponent<Image>();
	}

	public void Click() 
	{
	if (sprite.sprite == neutral)
		{
			sprite.sprite = highlight;
			acivmentList.SetActive(true);
		}
	else 
		{
			sprite.sprite = neutral;
			acivmentList.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
