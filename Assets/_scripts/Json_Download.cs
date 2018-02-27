using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//A class to hold incoming JSON Data Within the Session
public class parseJSON {
	// set of voice over links on the server 
	public List<string> url;
	//index from 0 to end of list used in renaming generated sound files on storage 
	public List<int> index;
	// size of each sound-clip that will be downloaded
	public List<float> FileSize;
	// size percentage of each file depend on the complete size of all sound-clips ex: 4.45 MB(size of clip)/25 Mb then ratio equal > 0.17  
	public List<float> FileSizeRatio;

}

public class Json_Download : MonoBehaviour {

	#region Class_GlobalVariables

	public GameObject contents, slider0, CheckCanvas, NetworkCanvas, CompleteCanvas, interceptor;
	WWW Json_www, Voice_Over_www;
	JsonData jsonvale;
	parseJSON parsejson;
	short urlList_Counter = 0, counter = 0;
	public Text Percentage;
	public Slider slider;
	float size, downloaded, AfterPercentage_Value;
	bool CachingFiles_Finished, Another_Download_In_Progress;
	string Selected_Language, Lan_temp, Current_Language, Current_Json;
	string Domain = "https://multilang.vrteek.com";

	#endregion

	#region Unity_OverrideMethods

	void Start () {
		// prevent Screen from sleep
		Screen.sleepTimeout = 0;
		// request server to get json and get data
		StartCoroutine (Download_JsonData ());
	}

	void Update () {
		//check if  request doesn't empty 
		if (Voice_Over_www != null) {
			//begin download progress of selected language files to inform user about download status
			DownloadProgress ();
			//check if server request do not return an error if it returns an error Network error Canvas will be shawn
			if (Voice_Over_www.error != null) {
				NetworkCanvas.SetActive (true);
				// A barrier to prevent user from hit any button while Any Dialouge canvases appeared
				interceptor.SetActive (true);
			}

		}
	}

	#endregion

	#region Used_Coroutines

	// request server to get json string
	IEnumerator Download_JsonData () {

		//url of Server to be requested 
		string url = "https://multilang.vrteek.com/api/v1/sounds/lungs";

		Json_www = new WWW (url);
		//wait server to respond with requested json
		yield return Json_www;

		//check if server request does not return an error if it returns an error Network-error Canvas will be shawn
		if (Json_www.error == null) {
			Processjson (Json_www.text);
		} else {
			Debug.Log ("ERROR: " + Json_www.error);
			NetworkCanvas.SetActive (true);
			// A barrier to prevent user from hit any button while Any Dialouge canvases appeared
			interceptor.SetActive (true);
		}
	}

	// Main Method That Perform Downloading 
	IEnumerator Download_VoiceOverClips (string LAN, string url, int index) {

		Lan_temp = LAN;
		//if voice over sound clip does'nt exist in storage files then Coroutine will start downloading and caching it 
		if (!File.Exists (Application.persistentDataPath + "/" + "Languages/" + LAN + "/" + index + ".mp3")) {
			//give a voice over sound clip url to www to download it  
			Voice_Over_www = new WWW (url);
			// wait until www finish download from server
			yield return Voice_Over_www;
			//if www return no error either from server or network itself begin caching downloaded file
			if (Voice_Over_www.error == null && Voice_Over_www.isDone) {
				print ("Caching....");
				// caching download file with a unique id and format
				File.WriteAllBytes (Application.persistentDataPath + "/" + "Languages/" + LAN + "/" + index + ".mp3", Voice_Over_www.bytes);
				//once download complete move to another url to be downloaded by increase url index indicator
				urlList_Counter++;
				//if url index indicator not equal size of the whole url List again start download the new file  
				if (urlList_Counter != parsejson.url.Count) {
					//(downloaded) is the value of what has been downloaded to continue download from thist point --> 
					//example [if first file or Clip ratio is "0.17" of the whole files size which is "1" ofcource this means if the file has already downloaded progressbar will begin the next download with a value of 17% and so on]
					downloaded += parsejson.FileSizeRatio[urlList_Counter];
					StartCoroutine (Download_VoiceOverClips (LAN, parsejson.url[urlList_Counter], parsejson.index[urlList_Counter]));

				}
				// but if url index indicator equal the whole url List then this means download complete 
				else if (urlList_Counter == parsejson.url.Count) {
					CachingFiles_Finished = true;
					Selected_Language = LAN;

				}

			}
			// if www return error either from server or network Network error canvas must be appear to let user refreash request and try again
			else {
				Debug.Log ("ERROR: " + Voice_Over_www.error);
				NetworkCanvas.SetActive (true);
				interceptor.SetActive (true);
			}

		}
		// if the file already exists 
		else {

			long length = new System.IO.FileInfo (Application.persistentDataPath + "/" + "Languages/" + LAN + "/" + index + ".mp3").Length;
			//must check the file was downloaded with its whole size and if it's not complete re-download it 
			if (length >= parsejson.FileSize[urlList_Counter]) {
				//the file downloaded with its complete size so url index indicator will be increased and start download another file
				urlList_Counter++;
				if (urlList_Counter != parsejson.url.Count) {

					downloaded += parsejson.FileSizeRatio[urlList_Counter];
					StartCoroutine (Download_VoiceOverClips (LAN, parsejson.url[urlList_Counter], parsejson.index[urlList_Counter]));
					if (urlList_Counter == parsejson.url.Count) {
						//if url index indicator equal size of the whole url List progress canvas will disappear and set the player prefs language to this compleated language 
						slider0.SetActive (false);
						PlayerPrefs.SetString ("Selected_Language", LAN);
						SceneManager.LoadScene ("Menu");
					}

				}
			}
			//if downloaded file not complete re-download it
			else {
				StartCoroutine (Download_VoiceOverClips (LAN, parsejson.url[urlList_Counter], parsejson.index[urlList_Counter]));
			}

		}
	}

	#endregion

	#region (Process Json , Choosing_Language , Download Progress) Methods

	// process json string by converting it into normal string and then extract wanted data from it 	
	private void Processjson (string jsonString) {
		// process json string
		jsonvale = JsonMapper.ToObject (jsonString);
		// loop over json-string List Of Language to extract data
		for (int i = 0; i < jsonvale["data"]["languages"].Count; i++) {
			// print (jsonvale["data"]["languages"][i].Count);
			short index = (short) jsonvale["data"]["languages"][i].Count;
			Button btn = null;
			if (index > 1) {
				// load prefabs of languages list from resources folder
				GameObject Prefab = (GameObject) Resources.Load (jsonvale["data"]["languages"][i][0]["language"].ToString ());
				//instantiate and Remove (Clone) word  
				GameObject Object_After_Instantiate = (GameObject) Instantiate (Prefab, Vector3.zero, Quaternion.identity, contents.transform);
				Object_After_Instantiate.name = Prefab.name;
				// adjust buttons RectTransform to be shawn in wanted position
				Object_After_Instantiate.transform.localPosition = Vector3.zero;
				btn = Object_After_Instantiate.GetComponent<Button> ();

				btn.onClick.AddListener (() => Caching_VoiceOver (btn.name, jsonString));
				contents.GetComponent<RectTransform> ().position = new Vector3 (140, contents.GetComponent<RectTransform> ().position.y, contents.GetComponent<RectTransform> ().position.z);
			} else {
				// load prefabs of languages list from resources folder
				GameObject Prefab = (GameObject) Resources.Load (jsonvale["data"]["languages"][i][0]["language"].ToString ());
				//instantiate and Remove (Clone) word  
				GameObject Object_After_Instantiate = (GameObject) Instantiate (Prefab, Vector3.zero, Quaternion.identity, contents.transform);
				Object_After_Instantiate.name = Prefab.name;
				// adjust buttons RectTransform to be shawn in wanted position
				Object_After_Instantiate.transform.localPosition = Vector3.zero;
				btn = Object_After_Instantiate.GetComponent<Button> ();

				btn.onClick.AddListener (() => Caching_VoiceOver (btn.name, jsonString));
				contents.GetComponent<RectTransform> ().position = new Vector3 (140, contents.GetComponent<RectTransform> ().position.y, contents.GetComponent<RectTransform> ().position.z);
				print (btn.name + " is Disabled No Containd Sound-Clips");
				btn.enabled = false;
			}

		}
	}

	// these method will be called when user hit language button 
	void LanguageChoice (string Json, String LanguagePressed) {

		#region  set used Variables values to zero
		urlList_Counter = 0;
		slider.value = 0;
		AfterPercentage_Value = 0;
		downloaded = 0;
		Percentage.text = "0 ";
		string url = null;
		size = 0.0f;
		float sizePerFile = 0;
		#endregion

		// JsonData Object That holds Json String To process it 
		jsonvale = JsonMapper.ToObject (Json);
		parsejson = new parseJSON ();
		// set of voice over links on the server 
		parsejson.url = new List<string> ();
		//index from 1 to end of list used in renaming generated sound files on storage
		parsejson.index = new List<int> ();
		// size percentage of each file depend on the complete size of all sound-clips ex: 4.45 MB(size of clip)/25 Mb then ratio equal > 0.17
		parsejson.FileSizeRatio = new List<float> ();
		// size of each sound-clip that will be downloaded
		parsejson.FileSize = new List<float> ();

		//if Language Directory Doesn't exist create it depend on language button pressed 
		if (!File.Exists (Application.dataPath + "/Resources/" + "Languages/" + LanguagePressed + "/")) {
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/" + "Languages/" + LanguagePressed + "/");
		}

		// fill Data Class'lists with extracted data from Json String 
		for (int i = 1; i < jsonvale["data"]["languages"][LanguagePressed].Count; i++) {
			url = Domain + jsonvale["data"]["languages"][LanguagePressed][i]["url"].ToString ();
			//url = jsonvale["data"]["languages"][LanguagePressed][i]["url"].ToString ();
			size += float.Parse (jsonvale["data"]["languages"][LanguagePressed][i]["size"].ToString ());
			sizePerFile = float.Parse (jsonvale["data"]["languages"][LanguagePressed][i]["size"].ToString ());
			parsejson.url.Add (url);
			parsejson.index.Add (i);
			parsejson.FileSize.Add (sizePerFile);

		}
		// fill list of file size ratio depend on division of (size per file / final size of all files )
		for (int i = 0; i < parsejson.FileSize.Count; i++) {
			parsejson.FileSizeRatio.Add (parsejson.FileSize[i] / size);

		}
		// show progressbar when begin the download 
		slider0.SetActive (true);
		StartCoroutine (Download_VoiceOverClips (LanguagePressed, parsejson.url[0], parsejson.index[0]));

	}

	// download progressbar configuration 
	void DownloadProgress () {
		//if url index indicator not equal size of the whole url List
		if (urlList_Counter != parsejson.url.Count) {
			// if the first url is in progress slider value will be just increased by www progress of file multiplied by its size ratio --> ex [0.1(www progress) * 0.17(file size ratio) = 0.017 ] once first file complete slider value will be 17%  
			if (urlList_Counter == 0) {

				slider.value = Voice_Over_www.progress * parsejson.FileSizeRatio[0];

			}
			// if used url now is Post to first index  slider value will be increased by www progress of file multiplied by its size ratio plus downloaded ratio of previous files 
			if (urlList_Counter > 0 && slider.value < Voice_Over_www.progress * parsejson.FileSizeRatio[urlList_Counter] + downloaded) {

				slider.value = Voice_Over_www.progress * parsejson.FileSizeRatio[urlList_Counter] + downloaded;

			}
		}
		// if all files downloaded completely percentage text will be 100% and Download-complete canvas will be appeared 
		if (CachingFiles_Finished == true) {
			Percentage.text = "" + 100;
			slider.value += 0.1f;

			//if slider value = 1 that ensure that download successsfully completed then the player prefs language will be set to this compleated language
			if (slider.value == 1) {
				PlayerPrefs.SetString ("Selected_Language", Selected_Language);
				CompleteCanvas.SetActive (true);
				interceptor.SetActive (true);

			}
		} else {
			AfterPercentage_Value = slider.value * 100;
			Percentage.text = "" + (int) AfterPercentage_Value;
		}

	}

	#endregion

	#region Buttons_public_Methods

	//When Hit a language Button
	public void Caching_VoiceOver (String LanguagePressed, String Json) {
		Current_Language = LanguagePressed;
		Current_Json = Json;
		// first time  
		if (counter == 0) {
			LanguageChoice (Json, LanguagePressed);
			counter++;
		}
		// later times within same session with different languages if user choose another language than language that already being downloaded check canvas will be shawn to ask either he want to continue language in progress or cancel and start the other one 
		else if (counter > 0 && Lan_temp != LanguagePressed) {
			CheckCanvas.SetActive (true);
			interceptor.SetActive (true);
		}
		// later times within same session with same language if user choose language that already being download > No Need To shaw any Dialogues >
		else if (counter > 0 && Lan_temp == LanguagePressed) {
			return;
		}

	}

	// when hit plus button browser will be opened to site that the user can add his voice over clips  
	public void Add_New_Language () {
		Application.OpenURL ("http://www.vrteek.com/");
	}
	// when hit cancel button that appeared on Checkcanvas language in progress will be canceled and start download another language
	public void OnCancelDownload () {
		CheckCanvas.SetActive (false);
		interceptor.SetActive (false);
		LanguageChoice (Current_Json, Current_Language);
	}
	// when hit resume it simply hide check-canvas and let language in progress finish download  
	public void OnResumeDownload () {
		CheckCanvas.SetActive (false);
		interceptor.SetActive (false);
	}
	// when hit finish button main scene which contain (VR & AR Modes) will be opened
	public void finishDownload () {
		SceneManager.LoadScene ("Menu");
	}
	// when hit refresh button scene will be re-opened the send new request to the server 
	public void Refresh () {
		NetworkCanvas.SetActive (false);
		interceptor.SetActive (false);
		SceneManager.LoadScene ("JSON_Download");
	}

	#endregion

}