using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetImage : MonoBehaviour
{
    private WWW imageWWW;
	// Use this for initialization
    IEnumerator Start () {
        Transform imageTf = transform.Find("Canvas/Image");
        if (imageTf == null) yield break;
        string url = "http://img15.3lian.com/2015/f2/50/d/75.jpg";
        imageWWW = new WWW(url);
        yield return imageWWW;
        if (!string.IsNullOrEmpty(imageWWW.error))
        {
            Debug.Log(imageWWW.error);
            yield break;
        }
        Texture2D texture = imageWWW.texture;
        imageTf.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height),Vector2.one/2);
	}

	void Update () {
	
	}

    void OnGUI()
    {
        if(imageWWW != null)
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(0, 0, 1000, 30), "下载进度：" + Mathf.Round(imageWWW.progress * 100) + "%");
        }
    }
            
}
