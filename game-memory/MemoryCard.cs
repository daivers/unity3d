using UnityEngine;
using System.Collections;

public class MemoryCard : MonoBehaviour {

	[SerializeField] private GameObject cardBack; 
	[SerializeField] private SceneController controller;
	private int _id;
	public int id {
		get {return _id;} 
	}

	public void SetCard(int id, Sprite image) {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}
	

	public void OnMouseDown() {
		if (cardBack.activeSelf && controller.canReveal) { //Проверка свойства canReveal контроллера, позволяющая гарантировать, что одновременно могут быть открыты всего две карты.
			cardBack.SetActive(false); // Делаем объект неактивным/невидимым.
			controller.CardRevealed(this); // Уведомление контроллера при открытии этой карты.
		}
	}
	public void Unreveal() { //Открытый метод, позволяющий компоненту SceneController снова скрыть карту (вернув на место спрайт card_back).
		cardBack.SetActive(true);
	}
}