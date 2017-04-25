using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InitLoad : MonoBehaviour {


    public int xLength = 8;
    public int yLength = 8;

    private float divider = 1f;

    private float size;

    private float padding = 30f;

    private bool IsMouseMove = false;


    public Dictionary<Vector2,LoadItem> map = new Dictionary<Vector2,LoadItem>();

	// Use this for initialization
    //地图初始化
	void Start () {

        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        size = (rectTransform.rect.height - padding *2 - yLength * divider) / yLength;

        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                GameObject obj = new GameObject("LoadItem");
                obj.transform.SetParent(transform);
                Image loadImage = obj.AddComponent<Image>();
                Vector2 loadPosition = new Vector2(i, j);
                LoadItem loadItem = new LoadItem(loadImage,loadPosition);
                map.Add(loadPosition, loadItem);


                OnLoadClickListener onLoadClickListener = OnLoadClickListener.GetLoadClickListenr(loadItem);
                onLoadClickListener.Load = loadItem;
                onLoadClickListener.OnClick = this.OnClick; 
                onLoadClickListener.OnEnter = this.OnEnter;
                onLoadClickListener.OnMouseDown = this.OnMouseDown;
                onLoadClickListener.OnMouseUp = this.OnMouseUp;

                RectTransform objRect = obj.GetComponent<RectTransform>();
                objRect.anchorMin = Vector2.one/2;
                objRect.anchorMax = Vector2.one/2;
                objRect.sizeDelta = new Vector2(size, size);
                objRect.localScale = Vector2.one;
                objRect.localPosition = new Vector3((size + divider) * i + size/2 - (rectTransform.rect.width - padding *2)/2,(size + divider) * j + size/2 - (rectTransform.rect.height - padding *2)/2);
                objRect.localRotation = Quaternion.identity;

            }
        }
	
	}

    //获取是道路的集合
    public List<Vector2> GetMap()
    {
        List<Vector2> loadMap = new List<Vector2>();
        foreach (KeyValuePair<Vector2,LoadItem> kv in map)
        {
            if (kv.Value.IsLoad)
            {
                loadMap.Add(kv.Key);
            }
        }
        return loadMap;
    }
	


    //道路格子类
    public class LoadItem
    {
        public Vector2 position;

        public Image image;

        private bool _isLoad= true;

        private LoadItem myload ;




        public bool IsLoad
        {
            set{
                if (value)
                {
                    image.color = Color.white;
                }
                else
                {
                    image.color = Color.red;   
                }
                _isLoad = value;
            }
            get{
                return _isLoad;
            }
        }

        public LoadItem(Image image,Vector2 position)
        {
            this.image = image;
            this.position = position;

            this.myload = this;
        }


    }

    //点击事件 处理 是否道路
    public void OnClick(LoadItem loadItem)
    {


    }

    public void OnMouseDown(LoadItem loadItem)
    {
        IsMouseMove = true;
        if (loadItem.IsLoad)
        {
            loadItem.IsLoad = false;
        }
        else
        {
            loadItem.IsLoad = true;
        }
    }

    public void OnMouseUp(LoadItem loadItem)
    {
        IsMouseMove = false;
    }

    public void OnEnter(LoadItem loadItem)
    {
        Debug.Log("enter");
        if (!IsMouseMove)
            return;
        if (loadItem.IsLoad)
        {
            loadItem.IsLoad = false;
        }
        else
        {
            loadItem.IsLoad = true;
        }
    }

    public void OnExit(LoadItem loadItem)
    {

    }

    //单机事件监听
    public class OnLoadClickListener:EventTrigger
    {
        public delegate void VoidDelegate(LoadItem load);

        public VoidDelegate OnClick;
        public VoidDelegate OnEnter;
        public VoidDelegate OnExit;
        public VoidDelegate OnMouseUp;
        public VoidDelegate OnMouseDown;


        public LoadItem _load;


        public static OnLoadClickListener GetLoadClickListenr(LoadItem load)
        {
            OnLoadClickListener listener = load.image.gameObject.GetComponent<OnLoadClickListener>();
            if (listener == null)
                listener = load.image.gameObject.AddComponent<OnLoadClickListener>();
            return listener;
        }

        public LoadItem Load
        {
            set{
                _load = value;
            }

            get{
                return _load;
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (this.OnMouseUp != null && this.Load != null)
                OnMouseUp(this.Load);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (this.OnMouseDown != null && this.Load != null)
                OnMouseDown(this.Load);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (this.OnClick != null && this.Load != null)
                OnClick(this.Load);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (this.OnEnter != null && this.Load != null)
                OnEnter(this.Load);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (this.OnExit != null && this.Load != null)
                OnExit(this.Load);
        }
    }



}
