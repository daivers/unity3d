//вспышка при выстреле
using UnityEngine;
using System.Collections;

public class MussleFlash : MonoBehaviour {

	public GameObject flashHolder;  //файлы с картинкой вспышки и компонентом свет
	public Sprite[] flashSprites;   //спрайт со вспышкой
	public SpriteRenderer[] spriteRenderers;

	public float flashTime;

	void Start() {
		Deactivate();
	}

	public void Activate() {
		flashHolder.SetActive (true);

		int flashSpriteIndex = Random.Range (0, flashSprites.Length);  //выбор рандомного спрайта со вспышкой
		for (int i = 0; i < spriteRenderers.Length; i++) {  
			spriteRenderers[i].sprite = flashSprites[flashSpriteIndex];  //меняем идекс спрайта 
		}

		Invoke ("Deactivate", flashTime);   //вызов метода Deactivate через flashTime

	}

	public void Deactivate () {
		flashHolder.SetActive (false);

	}
}
