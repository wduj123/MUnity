using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;

public class StartLoad : MonoBehaviour {

    private string[] m_imageNames = new string[]
    {
            "image1","image2","image3"
    };
    private string[] m_textNames = new string[]
    {
         "点","击","屏","幕","继","续","游","戏"
    };
    private List<Image> m_imageList = new List<Image>();
    private List<Transform> m_textList = new List<Transform>();
    private Transform m_mainPanel;
    private bool m_loadSuccess = false;

	void Start ()
    {
        m_mainPanel = transform.Find("Canvas/Panel");
        RectTransform parentRect = m_mainPanel.GetComponent<RectTransform>();
        int count = this.m_imageNames.Length;
        for (int i = 0; i < count; i++)
        {
            GameObject imageObj = Instantiate(Resources.Load<GameObject>("Prefabs/Image"));
            RectTransform imageTransform = imageObj.GetComponent<RectTransform>();
            imageTransform.SetParent(m_mainPanel);
            imageTransform.anchorMax = new Vector2(0.5f, 1f);
            imageTransform.anchorMin = Vector2.right / 2;
            imageTransform.localPosition = Vector3.zero;
            imageTransform.localScale = Vector3.one;
            imageTransform.sizeDelta = new Vector2(parentRect.rect.width,0);
            imageTransform.localRotation = Quaternion.identity;
            Texture2D texture = Resources.Load<Texture2D>("Textures/" + this.m_imageNames[i]);
            Sprite startImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
            Image image = imageObj.GetComponent<Image>();
            image.sprite = startImage;
            imageObj.SetActive(false);
            m_imageList.Add(image);
        }
        StartCoroutine(FadeImage());
	}

    void initText()
    {
        float textWidth = 17f;
        float textDivider = 2f;
        float bottom = -258f;
        int textCount = m_textNames.Length;
        float halfWidth = (textWidth * (textCount-1) + textDivider * (textCount - 1))/2+0.5f;
        for (int i = 0; i < textCount; i++)
        {
            GameObject textObj = Instantiate(Resources.Load<GameObject>("Prefabs/Image"));
            RectTransform textTransform = textObj.GetComponent<RectTransform>();
            textObj.transform.SetParent(m_mainPanel);
            Texture2D texture = Resources.Load<Texture2D>("Textures/" + this.m_textNames[i]);
            Sprite textImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
            Image image = textObj.GetComponent<Image>();
            image.sprite = textImage;
            textTransform.anchorMin = new Vector2(0.5f,0f);
            textTransform.anchorMax = new Vector2(0.5f,0f);
            textTransform.localPosition = new Vector3((textWidth + textDivider) * i  - halfWidth,bottom,0);
            textTransform.localScale = Vector3.one;
            textTransform.sizeDelta = new Vector2(textWidth,textWidth);
            textTransform.localRotation = Quaternion.identity;
            m_textList.Add(textTransform.transform);
        }
        StartCoroutine(DoTextJump());
    }

    IEnumerator DoTextJump()
    {
        int count = m_textList.Count;
        for(int i = 0;i<count;i++)
        {
            Transform textTransform = m_textList[i];
            textTransform.DOLocalJump(textTransform.localPosition, 5f, 1, 0.5f, true).SetLoops(int.MaxValue).SetDelay(1f);
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator FadeImage()
    {
        int count = this.m_imageNames.Length;
        for (int i = 0; i < count; i++)
        {
            Image image = m_imageList[i];
            if (image == null) yield return null;
            image.gameObject.SetActive(true);
            Color newColor = image.color;
            newColor.a = 0f;
            image.color = newColor;
            image.DOFade(1f, 1f);
            if (i != 0)
            {
                Image imageNext = m_imageList[i - 1];
                if (imageNext == null) yield return null;
                imageNext.DOFade(0f, 1f);
            }
            if (i == 2)
            {
                initText();
                AudioSource  audio = image.gameObject.AddComponent<AudioSource>();
                audio.clip = Resources.Load<AudioClip>("BGM");
                audio.Play();
                audio.loop = true;
                this.m_loadSuccess = true;
            }
            yield return new WaitForSeconds(2f);
        }
    }
	
	void Update () {
        if (Input.GetMouseButtonUp(0) && this.m_loadSuccess)
        {
            for (int i = 0; i < m_textList.Count; i++)
            {
                m_textList[i].DOKill(true);
            }
            SceneManager.LoadScene("Test");
        }
	}
}
