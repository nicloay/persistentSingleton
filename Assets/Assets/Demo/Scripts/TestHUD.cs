using UnityEngine;
using System.Collections;
using System;

public class TestHUD : MonoBehaviour {

	// Update is called once per frame
	void OnGUI () {
		
		int startY=10;
		int offset=25;
		
		
		try{
			GUI.Label(new Rect(10, startY, 100, 20),"myIntVar");
			int myNewIntVar = int.Parse( GUI.TextField(new Rect(110, startY, 200, 20), ""+PlayerPreferences.instance.myIntVar , 25));
			PlayerPreferences.instance.myIntVar=myNewIntVar;
		} catch {}
		
		startY+=offset;		
		try{
			GUI.Label(new Rect(10, startY, 100, 20),"myFloatVar");
			float myNewFloatVar = float.Parse( GUI.TextField(new Rect(110, startY, 200, 20), ""+PlayerPreferences.instance.myFloatVar , 25));
			PlayerPreferences.instance.myFloatVar=myNewFloatVar;
		} catch {}
		
		startY+=offset;		
		GUI.Label(new Rect(10, startY, 100, 20),"myString");
		string myNewString = GUI.TextField(new Rect(110, startY, 200, 20), PlayerPreferences.instance.myString , 25);
		PlayerPreferences.instance.myString = myNewString;
		
		
		startY+=offset;		
		GUI.Label(new Rect(10, startY, 100, 20),"myBool");
		bool myBool=GUI.Toggle(new Rect(110, startY, 200, 20), PlayerPreferences.instance.myBool, "");						
		PlayerPreferences.instance.myBool = myBool;
		
		
		
		
		
		startY+=offset;
		if (GUI.Button(new Rect(10, startY, 90, 20),"Save")){
			PlayerPreferences.instance.savePropertiesToPlayerPrefs();	
		}
		if (GUI.Button(new Rect(110, startY, 90, 20),"Read")){
			PlayerPreferences.instance.readPropertiesFromPlayerPrefs();
		}
		if (GUI.Button(new Rect(210, startY, 90, 20),"Reset")){
			PlayerPreferences.instance.resetAllProperties();
		}
	}
}
