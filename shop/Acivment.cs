using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Acivment  {


	private string name;

	public string Name
	{
		get {return name;}
		set {name = value;}
	}

	private int points;
	public int Points
	{
		get {return points;}
		set {points = value;}
	}

	private bool unlocked;
	public bool Unlocked
	{
		get {return unlocked;}
		set {unlocked = value;}
	}

	private int spriteIndex;
	public int SpriteIndex
	{
		get {return spriteIndex;}
		set {spriteIndex = value;}
	}

	private GameObject acivmentRef;
	private List<Acivment> dependencies = new List<Acivment>();

	private string child;
	public string Child
	{
		get {return child;}
		set {child = value;}
	}




	public Acivment(string name, int points, int spriteIndex, GameObject acivmentRef)

	{
		this.name = name;
		this.unlocked = false;
		this.points = points;
		this.spriteIndex = spriteIndex;
		this.acivmentRef = acivmentRef;

		LoadAcivment();

	}


	public void AddDependency(Acivment dependendency)
	{
		dependencies.Add (dependendency);
	}



	public bool EarnAcivment()
	{
		if (!unlocked && !dependencies.Exists (x => x.unlocked == false))
		{
			acivmentRef.GetComponent<Image>().sprite = AcivmentManadger.Instance.unlockedSprite;
			SaveAcivment(true);


			if (child != null)
			{
				AcivmentManadger.Instance.EarnAcivment(child);
			}
			return true;
		}
		return false;
	}

	public void SaveAcivment(bool value)                     // сохранение при покупке
	{
		unlocked = value;

		int tmpPoints = PlayerPrefs.GetInt("Points");
		PlayerPrefs.SetInt ("Points", tmpPoints += points);
		Debug.Log("проверка сохранения" + name);
		PlayerPrefs.SetInt(name, value ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void LoadAcivment() 
	{
		Debug.Log("загрузка1");

		unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;   // проверка при загрузке куплено или нет

		if (unlocked) 
		{
			Debug.Log("загрузка2");
			AcivmentManadger.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
			acivmentRef.GetComponent<Image>().sprite = AcivmentManadger.Instance.unlockedSprite;

		}
	}
}
