using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

/*
 * Base class for persistent singleton
 *    Just derrive from this classs, and your class will suport [Persistent] attributes and 
 * basic operation (save/read/reset)
 *
 * If you want to see values in editor, attach your new class to any game object in the scene,
 * if you don't have any instances of your singleton class in editor, new gameObject will be created automaticaly.
 *
 */



/*static Dictionary<Type, > _dict = new Dictionary<string, string>
    {
	{"entry", "entries"},
	{"image", "images"},
	{"view", "views"},
	{"file", "files"},
	{"result", "results"},
	{"word", "words"},
	{"definition", "definitions"},
	{"item", "items"},
	{"megabyte", "megabytes"},
	{"game", "games"}
    };

 */







struct PlayerPrefEntry{
	public PlayerPrefEntry (string attributeName, string fieldName, object fieldValue,Type fieldType)
	{
		this.attributeName = attributeName;
		this.fieldName = fieldName;
		this.fieldValue = fieldValue;
		this.fieldType = fieldType;
	}

	public string attributeName;
	public string fieldName;
	public object fieldValue;
	public Type fieldType;
}



public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T:MonoSingleton<T>
{		
	private static Dictionary<Type,PlayerPrefsStrategyInterface> strategyDictionary=new Dictionary<Type, PlayerPrefsStrategyInterface>()
	{
		{typeof(int),new IntPlayerPrefsStrategyImpl()},
		{typeof(float),new FloatPlayerPrefsStrategyImpl()},
		{typeof(string),new StringPlayerPrefsStrategyImpl()}
	};
	
	
	
	
   	BindingFlags BINDING_FLAGS=BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
	private static Type EXPECTED_TYPE=typeof(Persistent);
	
	
	public override void Init(){		
		Attribute attr = System.Attribute.GetCustomAttribute(typeof(T),EXPECTED_TYPE);
		if (attr!=null && ((Persistent)attr).updateOnInstantiation){
			readPropertiesFromPlayerPrefs();	
		}			
	}
	
	
	private string getPreferenceKey(string classAttribute, PlayerPrefEntry ppe){
		return classAttribute + "." + 
			(ppe.attributeName.Equals("") ? ppe.fieldName : ppe.attributeName);	
	}
	
	string getClassAtributeProperty()
	{
		string result="";
		Attribute attr = System.Attribute.GetCustomAttribute(typeof(T),EXPECTED_TYPE);
		if (attr!=null)
			result=((Persistent)attr).name;
		return result;
	}
	
	private List<PlayerPrefEntry> getFieldsEntryFromClass(){
		List<PlayerPrefEntry> result=new List<PlayerPrefEntry>();
		MemberInfo[] infos=typeof(T).GetMembers(BINDING_FLAGS);
		foreach (MemberInfo info in infos) {
			object[] attributes=info.GetCustomAttributes(EXPECTED_TYPE, false);
			foreach (object attribute in attributes) {
				
				Persistent pa=(Persistent) attribute;
				string attributeName=pa.name;
				string fieldName=info.Name;
				
				FieldInfo fieldInfo = typeof(T).GetField(fieldName, BINDING_FLAGS);				
				object fieldValue = fieldInfo.GetValue(this);
				Type fieldType= fieldInfo.FieldType;
				result.Add(new PlayerPrefEntry(attributeName, fieldName,fieldValue,fieldType));				
			}
		}
		return result;
	}
	
	
	
	public void savePropertiesToPlayerPrefs(){
		string classAtribute=getClassAtributeProperty();
		List<PlayerPrefEntry> ppe=getFieldsEntryFromClass();
		foreach (PlayerPrefEntry item in ppe) {
			string key=getPreferenceKey(classAtribute,item);
			object fieldValue=item.fieldValue;
			
			PlayerPrefsContext context=new PlayerPrefsContext(strategyDictionary[fieldValue.GetType()]);
			context.writeToPlayerPrefs(key,fieldValue);	//FIXME try catch handler		
		}
		
		PlayerPrefs.Save();
		
	}
	
	public void readPropertiesFromPlayerPrefs(){
		string classAtribute=getClassAtributeProperty();
		List<PlayerPrefEntry> ppe=getFieldsEntryFromClass();
		foreach (PlayerPrefEntry item in ppe) {
			
			
			string key=getPreferenceKey(classAtribute,item);				
			Type fieldType=item.fieldType;
			
			
			PlayerPrefsContext context=new PlayerPrefsContext(strategyDictionary[fieldType]);
			object newValue=context.readFromPlayerPrefs(key); //FIXME try catch handle
			
			FieldInfo fieldInfo = typeof(T).GetField(item.fieldName, BINDING_FLAGS);	
			fieldInfo.SetValue(this,newValue);
		}
		
		PlayerPrefs.Save();
	}
	
	public void resetAllProperties(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
}