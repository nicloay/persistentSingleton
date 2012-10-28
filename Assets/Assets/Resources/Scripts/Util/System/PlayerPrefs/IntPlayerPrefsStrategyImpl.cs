using System;
using UnityEngine;


public class IntPlayerPrefsStrategyImpl:PlayerPrefsStrategyInterface
{
	#region PlayerPrefsStrategyInterface implementation
	void PlayerPrefsStrategyInterface.writeToPlayerPrefs (string key, object obj)
	{
		PlayerPrefs.SetInt(key, (int)obj);
	}

	object PlayerPrefsStrategyInterface.readFromPlayerPrefs (string key)
	{
		return PlayerPrefs.GetInt(key);
	}
	#endregion
	
	
}


