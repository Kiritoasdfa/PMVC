using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebug : MonoBehaviour
{
	static List<string> mLines = new List<string>();
	static UIDebug mInstance = null;
	static public void Log(string text)
	{

		#if !UNITY_EDITOR
						return;
		#endif
		if (Application.isPlaying)
		{
			if (mLines.Count > 20) mLines.RemoveAt(0);
			mLines.Add(text);

			if (mInstance == null)
			{
				GameObject go = new GameObject("_NGUI Debug");
				mInstance = go.AddComponent<UIDebug>();
				DontDestroyOnLoad(go);
			}
		}
		else
		{
			Debug.Log(text);
		}
	}
}
