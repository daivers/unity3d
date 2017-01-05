using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

	public static bool stop;
	public static bool isDone;
	public static string result;
	//public bool reverse;
	//public enum TimerMode { seconds = 1};
	//public TimerMode timerMode = TimerMode.minutes;
	public int startValue;
	public int endValue;
	public Text textOutput;
	public bool startAwake = true;
	private int min, sec;
	private string m,s;
	public bool proverka = false;
	public GameObject round;

	public void StartTimer ()
	{
		isDone = false;
		stop = false;
		startValue = 22;
		endValue = 0;
		sec = startValue;
		if(endValue > startValue)
		{
			Debug.Log("Game Timer: В этом режиме, параметр 'End Value' не может быть больше, чем 'Start Value'");
			stop = true;
		}
		if (proverka == false) {
			StartCoroutine (RepeatingFunction ());
		}
	}


	

	IEnumerator RepeatingFunction ()
	{
		while(true) 
		{
			if(!stop && !isDone) TimeCount();
			yield return new WaitForSeconds(1);
		}
	}

	void TimeCount ()
	{		
		if (sec == endValue) {
			if (!round.GetComponent<round> ().proverkaTimerAnswer) {
				isDone = true;
				proverka = true;
				round.GetComponent<round> ().finishDeal ();
			} else {
				sec = 0;
				isDone = true;
				proverka = true;
				//round.GetComponent<round> ().finishDeal ();
			}

		} if (!round.GetComponent<round> ().proverkaTimerAnswer) {
			CurrentTime ();
			sec--;
		} else {
			CurrentTime ();
			sec = 0;
		}
	}


	void CurrentTime()
	{
		s = sec.ToString();
	}

	void OnGUI()
	{
			result = s;
			textOutput.text = result;
	}
}