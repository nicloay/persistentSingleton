using System;
using UnityEngine;


public class FloatPlayerPrefsStrategyImpl:PlayerPrefsStrategyInterface
{
	#region PlayerPrefsStrategyInterface implementation
	void PlayerPrefsStrategyInterface.writeToPlayerPrefs (string key, object obj)
	{
		PlayerPrefs.SetFloat(key,(float)obj);
	}

	object PlayerPrefsStrategyInterface.readFromPlayerPrefs (string key)
	{
		return PlayerPrefs.GetFloat(key);
	}
	#endregion
	
}

