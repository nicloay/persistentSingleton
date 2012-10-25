using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(PlayerPreferences))]
public class PlayerPreferencesEditor:Editor{
	PlayerPreferences targetObject;
	
	public void OnEnable(){
		targetObject=(PlayerPreferences) target;
		targetObject.readPropertiesFromPlayerPrefs();
		EditorUtility.SetDirty(target);
	}
	
	public override void OnInspectorGUI(){
		 
		DrawDefaultInspector ();
		
		
		if(GUILayout.Button ("Save To PlayerPrefs")) 
        	 targetObject.savePropertiesToPlayerPrefs();
		if(GUILayout.Button ("Read From PlayerPrefs")){ 
			
    		targetObject.readPropertiesFromPlayerPrefs();
			EditorUtility.SetDirty(target);
		}
		if(GUILayout.Button ("Reset All Properties")){ 
        	if(EditorUtility.DisplayDialog("!!! ACHTUNG !!!", "It will remove all stored properties in PlayerPrefs, are you sure?", "Yes, I am","No")){
				targetObject.resetAllProperties();
				targetObject.readPropertiesFromPlayerPrefs();
				EditorUtility.SetDirty(target);					
			}
		}
		
		
	}
}
