using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class ViewPager : MonoBehaviour {
    
    private RectTransform m_scrollViewRect;

    private List<RectTransform> m_viewsRects = new List<RectTransform>();



	// Use this for initialization
	void Start () {
        //初始化 scrollview
        GameObject obj = GameObject.Find ("Scroll View");
        if (obj == null) {
            Debug.Log ("没有找到 scrollview 初始化失败");
            return;
        }

        this.m_scrollViewRect = obj.GetComponent<RectTransform>();


        //初始化item
        for (int i = 0; i < 5; i++) {
            GameObject itemPanel = Instantiate (Resources.Load<GameObject> ("PanelDemo"))as GameObject;
            RectTransform itemRect = itemPanel.GetComponent<RectTransform> ();

            this.m_viewsRects.Add(itemRect);

        }

        MyViewPager pager = new MyViewPager(this.m_scrollViewRect, this.m_viewsRects);

	}

	
}

//m_scrollViewRect 对象ScorllView 必须移除 横向和竖向滚动条
public class MyViewPager
{
    private RectTransform m_scrollViewRect;
    private RectTransform m_viewportRect;
    private RectTransform m_contentRect;
    private ScrollRect m_scrollRect;

    private List<RectTransform> m_viewRects;

    private float m_viewHeight;
    private float m_viewWidth;


    public int Position
    {
        set;
        get;
    }

    public int Count
    {
        set;
        get;
    }


    public MyViewPager(RectTransform scrollViewRect,List<RectTransform> viewRects){
        this.m_scrollViewRect = scrollViewRect;
        this.m_viewRects = viewRects;
        init();
    }


    void init()
    {
        //初始化 Position
        this.Position = 0;
        //初始化 Count
        this.Count = this.m_viewRects.Count;
        //获取高宽值
        this.m_viewHeight = this.m_scrollViewRect.rect.height;
        this.m_viewWidth = this.m_scrollViewRect.rect.width;

        this.m_viewportRect = this.m_scrollViewRect.Find("Viewport").GetComponent<RectTransform>();
        this.m_contentRect = this.m_scrollViewRect.Find("Viewport/Content").GetComponent<RectTransform>();
        this.m_scrollRect = this.m_scrollViewRect.GetComponent<ScrollRect>();

        initScrollView();
        initItemView();

    }

    void initScrollView()
    {
        //监听 鼠标 按下和抬起动作
        EventTriggerListener.GetListener(m_scrollViewRect.gameObject).OnMouseDown = this.OnMouseDown;
        EventTriggerListener.GetListener(m_scrollViewRect.gameObject).OnMouseUp = this.OnMouseUp;

        //限制竖直滑动
        this.m_scrollRect.vertical = false;

        //scrollView
        this.m_scrollViewRect.pivot = new Vector2(0.5f,0.5f);

        //viewport
        this.m_viewportRect.sizeDelta = Vector2.zero;
        this.m_viewportRect.pivot = new Vector2(0,1);
        this.m_viewportRect.anchorMax = Vector2.one;
        this.m_viewportRect.anchorMin = Vector2.zero;
        this.m_viewportRect.localScale = Vector3.one;
        this.m_viewportRect.localPosition = new Vector3(-this.m_viewWidth/2,this.m_viewHeight/2,0);

        //content
        this.m_contentRect.sizeDelta = new Vector2(this.m_viewWidth * this.Count, 0);
        this.m_contentRect.pivot = new Vector2(0,1);
        this.m_contentRect.anchorMin = Vector2.zero;
        this.m_contentRect.anchorMax = new Vector2(0,1);
        this.m_contentRect.localScale = Vector3.one;
        this.m_contentRect.localPosition = Vector3.zero;



    }

    void initItemView()
    {
        //初始化item
        for (int i = 0; i < this.Count; i++) {
            RectTransform itemRect = this.m_viewRects[i];

            itemRect.SetParent (this.m_contentRect);
            itemRect.anchorMin = new Vector2 (0, 0);
            itemRect.anchorMax = new Vector2 (0, 1);
            itemRect.sizeDelta = new Vector2 (this.m_viewWidth, 0);
            itemRect.pivot = new Vector2(0,1);
            itemRect.transform.localScale = Vector3.one;
            itemRect.transform.localPosition = new Vector3((float)(this.m_viewWidth * i),0,0);
        }
    }


    void OnMouseDown(GameObject obj)
    {
        Debug.Log ("ondown");
    }


    void OnMouseUp(GameObject obj)
    {
        //移动时间
        float animTime = 0.3f;

        Debug.Log ("onup"  + this.m_scrollRect.velocity);

        //此处localpostion的值与  设置时的不同 为0
        float oldContentPositionX = - this.Position * this.m_viewWidth;
        float currentContentPositionX = this.m_contentRect.transform.localPosition.x;

        Debug.Log (oldContentPositionX);
        Debug.Log (currentContentPositionX);

        if (this.m_scrollRect.velocity.x < -300 && this.Position != this.Count-1) 
        {
            this.m_scrollRect.velocity = new Vector2 (0, 0);
            this.m_contentRect.transform.DOLocalMoveX (oldContentPositionX - this.m_viewWidth, animTime, false);
            this.Position++;

        } 
        else if (this.m_scrollRect.velocity.x < 0 ) 
        {
            this.m_scrollRect.velocity = new Vector2 (0, 0);
            if (oldContentPositionX - currentContentPositionX > this.m_viewWidth / 2 && this.Position != this.Count-1) {
                Debug.Log ("right");
                this.m_contentRect.transform.DOLocalMoveX (oldContentPositionX - this.m_viewWidth, animTime, false);
                this.Position++;
            } else {
                this.m_contentRect.transform.DOLocalMoveX (oldContentPositionX, animTime, false);
            }

        } 
        else if (this.m_scrollRect.velocity.x > 300 && this.Position != 0) 
        {
            this.m_scrollRect.velocity = new Vector2 (0, 0);
            this.m_contentRect.transform.DOLocalMoveX (oldContentPositionX + this.m_viewWidth, animTime, false);
            this.Position--;
        } 
        else 
        {
            this.m_scrollRect.velocity = new Vector2 (0, 0);
            if (currentContentPositionX - oldContentPositionX > this.m_viewWidth / 2 && this.Position != 0) {
                this.m_contentRect.transform.DOLocalMoveX (oldContentPositionX + this.m_viewWidth, animTime, false);
                this.Position--;
            } else {
                this.m_contentRect.transform.DOLocalMoveX (oldContentPositionX, animTime, false);
            }
        }

        Debug.Log ("position = " + this.Position);


    }



}
