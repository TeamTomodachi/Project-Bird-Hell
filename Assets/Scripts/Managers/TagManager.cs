using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour {

	public List<string> Tags = new List<string>();
	public string SceneTag { get { return gameObject.tag; } }

	public bool ContainsTag(string checkTag)
	{
		if (SceneTag.Equals(checkTag))
			return true;

		foreach (var tag in Tags)
		{
			if (tag.Equals(checkTag))
				return true;
		}

		return false;
	}
}
