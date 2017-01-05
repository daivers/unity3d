using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
	public AudioSource AudioSource;
	public AudioClip ClickSound;

	public void Awake()
	{
		GetComponent<Button>().onClick.AddListener(PlayClickSound);
	}

	private void PlayClickSound()
	{
		AudioSource.PlayOneShot(ClickSound);
	}
}