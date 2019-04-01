using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System;

public class God : MonoBehaviour {
	//Sound Button
	float Max_Buttons = 21;
	public AudioClip[] Sounds;
	//Bottom Button
	public GameObject Record;
	public GameObject Stop;

	void Start () {
		Sounds = new AudioClip[int.Parse(Max_Buttons.ToString())];
		/*var Sounds = Resources.LoadAll("Sound Bytes", typeof(AudioClip)).ToArray();

		if (PlayerPrefs.GetInt("Page_Number") <= 0 || PlayerPrefs.GetInt("Page_Number") == null) {
			PlayerPrefs.SetInt("Page_Number", 1);
		}else if (PlayerPrefs.GetInt("Page_Number") > Mathf.CeilToInt(Sounds.Length / Max_Buttons)) {
			PlayerPrefs.SetInt("Page_Number", Mathf.CeilToInt(Sounds.Length / Max_Buttons));
		}

		Starting_Sound = PlayerPrefs.GetInt("Page_Number") * 21 - 20; //Adjust with playerprefs in future!!!!

		for (int i = 1; i <= Max_Buttons; i++) {
			if (Sounds.Length <= Starting_Sound + i - 1) {
				Destroy(GameObject.Find(i.ToString()));
			}else {
				GameObject.Find(i.ToString()).transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = Sounds[Starting_Sound + i - 1].name;
				AudioSource Temp_Source = gameObject.AddComponent<AudioSource>();
				Sound_Sources[i] = Temp_Source;
			}
		}

		//Bottom Button
		if (PlayerPrefs.GetInt("Page_Number") == 1) {
			Button_Last.SetActive(false);
		}else if (PlayerPrefs.GetInt("Page_Number") > Sounds.Length / Max_Buttons) {
			Button_Next.SetActive(false);
		}*/
	}

	void Update () {
		for (int i = 0; i < Sounds.Length; i++) {
			if (Sounds[i]) {
				GameObject.Find((i+1).ToString()).transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Recording #" + (i+1);
			}else {
				GameObject.Find((i+1).ToString()).transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "";
			}
		}
	}

	public void Sound_Button () {
		int Sound_Number = int.Parse(EventSystem.current.currentSelectedGameObject.name)-1;
		GetComponent<AudioSource>().PlayOneShot(Sounds[Sound_Number],1);
		Microphone.End(Microphone.devices[0]);
	}

	//Bottom Buttons
	/*public void Page_Last () {
		PlayerPrefs.SetInt("Page_Number", PlayerPrefs.GetInt("Page_Number") - 1);
		Application.LoadLevel("God");
	}*/

	public void Mute () {
		GetComponent<AudioSource>().Stop();
	}

	public void Add_Sound () {
		Microphone.End(Microphone.devices[0]);
		for(int i = Sounds.Length - 1; i >= 0 && !Sounds[i]; i--) {
			if (i == 0 || Sounds[i-1]) {
				Sounds[i] = Microphone.Start(Microphone.devices[0], true, 10, 44100);
				Stop.SetActive(true);
				Record.SetActive(false);
				StartCoroutine(Recording());
			}
		}
	}

	public void Stop_Recording() {
		Microphone.End(Microphone.devices[0]);
		StopAllCoroutines();
		Stop.SetActive(false);
		Record.SetActive(true);
	}

	IEnumerator Recording () {
		yield return new WaitForSeconds(10);
		Microphone.End(Microphone.devices[0]);
		Stop.SetActive(false);
		Record.SetActive(true);
	}

	/*public void Page_Next () {
		PlayerPrefs.SetInt("Page_Number", PlayerPrefs.GetInt("Page_Number") + 1);
		Application.LoadLevel("God");
	}*/
}
