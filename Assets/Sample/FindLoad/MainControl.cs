using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class MainControl : MonoBehaviour
{


    private Button reSetBtn;
    private Button findLoadBtn;


    private InitLoad scriptLoad;

    private static Dictionary<Vector2,InitLoad.LoadItem> map;

    // Use this for initialization
    void Start()
    {

        scriptLoad = transform.Find("Canvas/MapPanel").GetComponent<InitLoad>();
        map = scriptLoad.map;
	
        Transform reSetBtnTransform = transform.Find("Canvas/ControlPanel/ReSetBtn");
        Transform findLoadBtnTransform = transform.Find("Canvas/ControlPanel/FindLoadBtn");
        if (reSetBtnTransform != null)
            reSetBtn = reSetBtnTransform.GetComponent<Button>();
        if (findLoadBtnTransform != null)
            findLoadBtn = findLoadBtnTransform.GetComponent<Button>();
        reSetBtn.onClick.AddListener(() =>
            {
                ResetMap();
            });
        findLoadBtn.onClick.AddListener(() =>
            {
                FindLoad();
            });
    }

    void ResetMap()
    {
        foreach (var item in map)
        {
            item.Value.IsLoad = true;
        }
    }

    void ShowLoad(Vector2 point)
    {
        InitLoad.LoadItem item;
        if (map.TryGetValue(point, out item))
        {
            item.image.color = Color.blue;
        }
    }

    void FindLoad()
    {
        

//        LoadSystem.LoadPoint end = new LoadSystem.LoadPoint(2, 2);
//        LoadSystem.LoadPoint.endPoint = end;
//        LoadSystem.LoadPoint start = new LoadSystem.LoadPoint(4, 4);
//        LoadSystem.LoadPoint.startPoint = start;
//
//        List<LoadSystem.LoadPoint> map = new List<LoadSystem.LoadPoint>();
//        for (int i = 0; i < 7; i++)
//        {
//            for (int j = 0; j < 7; j++)
//            {
//                if((i<3 || i > 3) || j<2 || j> 5)
//                map.Add(new LoadSystem.LoadPoint(i, j));
//            }
//        }
//        LoadSystem loadSystem = new LoadSystem(start,end,map);


        Vector2 end = new Vector2(18f,18f);
        Vector2 start = new Vector2(1f,1f);
        List<Vector2> map = scriptLoad.GetMap();

        LoadSystem loadSystem = new LoadSystem(start,end,map);


        List<LoadSystem.LoadPoint> path = loadSystem.FindLoadPath();
        if (path != null)
        {
            foreach (LoadSystem.LoadPoint p in path)
            {
                Debug.Log(p.X + "  " +p.Y );
                ShowLoad(p.ToVector2());
            }
        }
        else
        {
            Debug.Log(null);
        }



    }


    public class LoadSystem
    {

        private LoadPoint startPoint;
        private LoadPoint endPoint;
        private List<LoadPoint> map;

        public LoadSystem(LoadPoint startPoint, LoadPoint endPoint, List<LoadPoint> map)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.map = map;
        }

        public LoadSystem(Vector2 startPoint,Vector2 endPoint,List<Vector2> map)
        {
            this.endPoint = new LoadPoint((int)endPoint.x,(int)endPoint.y);
            LoadPoint.endPoint = this.endPoint;
            this.startPoint = new LoadPoint((int)startPoint.x,(int)startPoint.y);
            LoadPoint.startPoint = this.startPoint;
            this.map = new List<LoadPoint>();
            foreach(Vector2 point in map)
            {
                this.map.Add(new LoadPoint((int)point.x,(int)point.y));
            }
        }


        public List<LoadPoint> openPoints = new List<LoadPoint>();
        public List<LoadPoint> closePoints = new List<LoadPoint>();


        public class LoadPoint
        {
            public static LoadPoint startPoint;
            public static LoadPoint endPoint;
            private LoadPoint _parentPoint;
            public LoadPoint ParentPoint{ 
                set{
                    this._parentPoint = value;
                    if (this._parentPoint == null)
                    {
                        this.G = 0;
                        this._F = this.G + this.H;
                    }
                    else
                    {
                        this.G = this._parentPoint.G + 1;
                        this._F = this.G + this.H;
                    }
                     

                } 
                get{ return this._parentPoint;}
            }

            public int X{ set; get;}
            public int Y{ set; get;}

            public int G{ set; get; }
            //距离起点距离
            private int _H;
            public int H
            { 
                get{ 
                    return this._H;
                } 
            }
            //距离终点距离
            private int _F;
            public int F{ 
                get{
                    return this._F;
                } 
            }
            //两者距离之和


            public LoadPoint(int x, int y, LoadPoint parentPoint = null)
            {
                this.X = x;
                this.Y = y;
                this.ParentPoint = parentPoint;
                if (parentPoint == null)
                {
                    this.G = 0;
                }else{
                    this.G = parentPoint.G + 1;
                }
               
                if(endPoint != null)
                {
                    this._H = Mathf.Abs(endPoint.X - this.X) + Mathf.Abs(endPoint.Y- this.Y);
                    this._F = this.G + this.H;
                }

            }

            public LoadPoint Up()
            {
                return new LoadPoint(X, Y + 1,this);
            }

            public LoadPoint Down()
            {
                return new LoadPoint(X, Y - 1,this);
            }

            public LoadPoint Right()
            {
                return new LoadPoint(X + 1, Y,this);
            }

            public LoadPoint Left()
            {
                return new LoadPoint(X - 1, Y,this);
            }

            public Vector2 ToVector2()
            {
                return new Vector2((float)X, (float)Y);
            }

            public List<LoadPoint> SurroundPoints()
            {
                List<LoadPoint> surroundPoints = new List<LoadPoint>();
                surroundPoints.Add(this.Up());
                surroundPoints.Add(this.Down());
                surroundPoints.Add(this.Left());
                surroundPoints.Add(this.Right());
                return surroundPoints;
            }

            public override string ToString()
            {
                return string.Format("[X={1}, Y={2}]", X, Y);
            }
        }


        public List<LoadPoint> FindLoadPath()
        {
            openPoints.Add(startPoint);

            while (openPoints.Count > 0)
            {
                //列表排序
                openPoints.Sort(delegate(LoadPoint x, LoadPoint y)
                    {
                        return Comparer<int>.Default.Compare(x.F, y.F);
                    });

                LoadPoint minPoint = openPoints[0];


                // H=1 说明查找结束
                if (minPoint.H == 1)
                {
                    List<LoadPoint> path = new List<LoadPoint>();
                    path.Add(endPoint);
                    endPoint.ParentPoint = minPoint;
                    while (path[path.Count-1].ParentPoint != null)
                    {
                        path.Add(path[path.Count-1].ParentPoint);
                    }
                    path.Reverse();

                    return path;
                }

                //添加到关闭列表
                closePoints.Add(minPoint);
                //从打开别表删除
                openPoints.Remove(minPoint);

                foreach (LoadPoint point in minPoint.SurroundPoints())
                {
                    //关闭列表是否存在 关闭列表中已存在 说明已经处理过 不做处理 
                    if (closePoints.Exists(delegate(LoadPoint p)
                        {
                            return p.X == point.X && p.Y == point.Y;
                        }))
                    {
                        continue;
                    }
                    //判断 是否存在在地图中 不存在不做处理
                    if (!map.Exists(delegate(LoadPoint mp)
                        {
                            return mp.X == point.X && mp.Y == point.Y;
                        }))
                    {
                        continue;
                    }
                    //打开列表是否存在 
                    LoadPoint oldPoint = openPoints.Find(delegate(LoadPoint po)
                        {
                            return po.X == point.X && po.Y == point.Y;
                        });
                    //存在 则比较两个 的G值 存在的G值大于则  替换 
                    if (oldPoint != null && oldPoint.G > point.G)
                    {
                        oldPoint.ParentPoint = minPoint;
                    }
                    else
                    {
                        openPoints.Add(point);
                    }

                }

            }

            //跳出循环说明查找结束 没有找到路径
            return null;

        }


    }
}
