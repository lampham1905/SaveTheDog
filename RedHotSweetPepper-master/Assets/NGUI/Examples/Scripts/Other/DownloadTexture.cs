//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2016 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
/// <summary>
/// Simple script that shows how to download a remote texture and assign it to be used by a UITexture.
/// </summary>

[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	public string url = "http://www.yourwebsite.com/logo.png";
	public bool pixelPerfect = true;

	Texture2D mTex;

	IEnumerator Start ()
	{
#if UNITY_2018_3_OR_NEWER
		var www = UnityEngine.Networking.UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		//mTex = UnityEngine.Networking.DownloadHandlerTexture.GetContent(www);
#else
		var www = new WWW(url);
		yield return www;
		mTex = www.texture;
#endif
		if (mTex != null)
		{
			UITexture ut = GetComponent<UITexture>();
			ut.mainTexture = mTex;
			if (pixelPerfect) ut.MakePixelPerfect();
		}
		www.Dispose();
	}

	void OnDestroy ()
	{
		if (mTex != null) Destroy(mTex);
	}
}
