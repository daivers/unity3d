using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	public int xz;


	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) {
		//Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);
		if (gameObject.name == "score1") {
			xz = 0;
		}
		if (gameObject.name == "score2") {
			xz = 1;
		}
		if (gameObject.name == "score3") {
			xz = 2;
		}
		if (gameObject.name == "score4") {
			xz = 3;
		}
		if (gameObject.name == "score5") {
			xz = 4;
		}


		eventData.pointerDrag.GetComponent<test> ().placeWhereCoins = xz;

		

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.parentToReturnTo = this.transform;
		}

		//Debug.Log (eventData.pointerDrag.GetComponent<HighScoreScript> ().group);

		if (gameObject.name == "Home On") {
			//eventData.pointerDrag.GetComponent<HighScoreScript> ().group = 2.ToString();

		}


	}
}
