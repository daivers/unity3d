using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AcivmentManadger : MonoBehaviour {
	public GameObject acivmentPrefab;

	public Sprite[] sprites;

	private AcivmentButton activeButton;
	public ScrollRect scrollRect;

	public GameObject acivmentMenu;

	public GameObject visualAcivment;

	public Dictionary <string, Acivment> acivments = new Dictionary <string, Acivment>();

	public Sprite unlockedSprite;
	public Text textPoints;

	private static AcivmentManadger instance;      //ПХ сохранение покупок

	public static AcivmentManadger Instance
	{
		get {
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<AcivmentManadger>();
			}
			return AcivmentManadger.instance;
		}

	}



	// Use this for initialization
	void Start () {
		// Очистка сохранения
		PlayerPrefs.DeleteAll();

		activeButton = GameObject.Find("GeneralBtn").GetComponent<AcivmentButton>();

		CreateAcivment("Genefffral", "Press N", 5,0);
		CreateAcivment("Genfferal", "Press S", 25,1);
		CreateAcivment("General", "Press", 17,2);
		CreateAcivment("General", "Pres", 33,3);
		CreateAcivment("General", "Pre", 43,4);
		CreateAcivment("General", "Pr", 41,1);
		CreateAcivment("General", "Pfgh", 12,0);
		CreateAcivment("General", "Preiku", 15,2);
		//CreateAcivment("General", "all", 10,0, new string[]{"Press N","Press S"});
	


		foreach (GameObject acivmentList in GameObject.FindGameObjectsWithTag("AcivmentList"))
		{
			acivmentList.SetActive(false);
		}

		activeButton.Click();
		acivmentMenu.SetActive(true);
	}



	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I))
		{
			acivmentMenu.SetActive(!acivmentMenu.activeSelf);
		}

		if (Input.GetKeyDown(KeyCode.N))
		{
			EarnAcivment("Press N");
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			EarnAcivment("Press S");
		}
	}


	public void EarnAcivment(string title)
	{
		if (acivments[title].EarnAcivment())
		{
			//DO SOMETHING AWESOME!!!
			GameObject acivment = (GameObject)Instantiate(visualAcivment);
			SetAcivmentInfo("EarnCanvas", acivment,title);
			textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");  //выводим поинты во время игры
			StartCoroutine(HideAcivment(acivment));
		}
	}


	public IEnumerator HideAcivment(GameObject acivment)
	{
		yield return new WaitForSeconds(3);
		Destroy(acivment);
	}



	public void CreateAcivment(string parent, string title,  int points, int spriteIndex, string[] dependencies = null)
	{
		GameObject acivment = (GameObject)Instantiate(acivmentPrefab);
		Acivment newAcivment = new Acivment(name, points, spriteIndex, acivment);

		acivments.Add(title, newAcivment);

		SetAcivmentInfo(parent, acivment, title);

		if (dependencies != null)
		{
			foreach (string acivmentTitle in dependencies)
			{
				Acivment dependency = acivments[acivmentTitle];
				dependency.Child = title;
				newAcivment.AddDependency(dependency);

				//Dependency = Press Space  <-- Child = Press W
				//NewAcivment = Press W --> Press Space
			}
		}
	}

	public void SetAcivmentInfo(string parent, GameObject acivment, string title)
	{
		acivment.transform.SetParent(GameObject.Find(parent).transform);
		acivment.transform.localScale = new Vector3(1,1,1);
		acivment.transform.GetChild(0).GetComponent<Text>().text = title;
		acivment.transform.GetChild(1).GetComponent<Text>().text = acivments[title].Points.ToString();
		acivment.transform.GetChild(2).GetComponent<Image>().sprite = sprites[acivments[title].SpriteIndex];
	}

	public void ChangeCategory(GameObject button)

	{
		AcivmentButton acivmentButton = button.GetComponent<AcivmentButton>();

		scrollRect.content = acivmentButton.acivmentList.GetComponent<RectTransform>();

		acivmentButton.Click();
		activeButton.Click();
		activeButton = acivmentButton;

	}
}

