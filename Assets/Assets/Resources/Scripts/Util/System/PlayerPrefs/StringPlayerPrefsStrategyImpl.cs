using System;
using UnityEngine;


public class StringPlayerPrefsStrategyImpl:PlayerPrefsStrategyInterface
{
	#region PlayerPrefsStrategyInterface implementation
	void PlayerPrefsStrategyInterface.writeToPlayerPrefs (string key, object obj)
	{
		PlayerPrefs.SetString(key,(string)obj);
	}

	object PlayerPrefsStrategyInterface.readFromPlayerPrefs (string key)
	{
		return PlayerPrefs.GetString(key);
	}
	#endregion
	
}


