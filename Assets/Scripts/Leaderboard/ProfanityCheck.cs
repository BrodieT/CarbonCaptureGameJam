using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProfanityHolder", menuName = "Profanity", order = 0)]
public class ProfanityCheck : ScriptableObject
{
	[SerializeField]
	List<string> profanities = new List<string>();

	public bool CheckProfanities(string a)
	{
		if(profanities.Contains(a))
		{
			return true;
		}

		return false;
	}
}
