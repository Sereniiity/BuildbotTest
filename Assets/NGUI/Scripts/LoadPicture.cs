using UnityEngine;
using System.Collections;

public class LoadPicture : MonoBehaviour {

	public string url;

	private IEnumerator LoadImage(UITexture texture) {
		WWW www = new WWW( url );
		yield return www;
		texture.mainTexture = www.texture;
	}
	
	void Start ()
	{
		UITexture texture = gameObject.AddComponent<UITexture> ();

		StartCoroutine( LoadImage(texture) );

	}
}