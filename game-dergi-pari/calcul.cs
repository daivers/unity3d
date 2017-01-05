using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class calcul : MonoBehaviour {

	public string numberFirst;
	public int CurrentNubmer=1;
	public int numberFirstVar;
	public Text answerCalculatorText;
	private GameObject mainMenu;
	private GameObject calculatorObj;


	public void Start() {
		mainMenu = GetComponent<GamesPlay> ().mainMenu;
		calculatorObj = GetComponent<GamesPlay> ().calculatorObj;
		numberFirst = "0";
		UpdateCalc ();



	}
	public void btnOne() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "1";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "1";
				UpdateCalc ();
			}
		}
	}

	public void btnTwo() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "2";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "2";
				UpdateCalc ();
			}
		}
	}
	public void btnFree() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "3";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "3";
				UpdateCalc ();
			}
		}
	}
	public void btnFoure() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "4";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "4";
				UpdateCalc ();
			}
		}
	}
	public void btnFive() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "5";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "5";
				UpdateCalc ();
			}
		}
	}
	public void btnSix() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "6";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "6";
				UpdateCalc ();
			}
		}
	}
	public void btnSeven() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "7";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "7";
				UpdateCalc ();
			}
		}
	}
	public void btnEight() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "8";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "8";
				UpdateCalc ();
			}
		}
	}
	public void btnNine() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "9";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "9";
				UpdateCalc ();
			}
		}
	}
	public void btnZero() {

		if(CurrentNubmer == 1)
		{
			if(numberFirst != "0")
			{
				numberFirst += "0";
				UpdateCalc ();
			}
			if(numberFirst == "0")
			{
				numberFirst = "0";
				UpdateCalc ();
			}
		}
	}

	public void UpdateCalc() {
		numberFirstVar = System.Convert.ToInt32(numberFirst);
		answerCalculatorText.text = numberFirst;
	}

	public void btnEnter() {

		numberFirstVar = System.Convert.ToInt32(numberFirst);
		answerCalculatorText.text = numberFirst;
		gameObject.GetComponent<round>().answers(numberFirstVar);
		mainMenu.active = false;
		calculatorObj.GetComponent<Animator> ().SetTrigger ("CalculatorClose");

	}

	public void btnDel() {

			numberFirst = numberFirst.Substring(0, numberFirst.Length - 1);
		if (numberFirst.Length == 0) {
			numberFirst = "0";
		}
		UpdateCalc ();



	}





	 
}
