// ========================================================
// 描 述：PointController.cs 
// 作 者：郑贤春 
// 时 间：2017/02/08 22:18:34 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BlinePointController
{
    private float[] pointT;
    private Vector3[] pointNormal;
    private Vector3[] pointList;
    private float lineWidth;

    private Vector3[] lineUpPath;
    private Vector3[] lineDowmPath;
    bool meshInitialized = false;

    /// <summary>
    /// 获取曲线上面的所有点
    /// </summary>
    /// <returns>The list.</returns>
    /// <param name="path">需要穿过的点列表</param>
    /// <param name="pointSize">两个点之间的节点数量</param>
    public Vector3[] PointList(Vector3[] path, int pointSize)
    {
        Vector3[] controlPointList = PathControlPointGenerator(path);

        int smoothAmount = path.Length * pointSize;
        Vector3[] pointList = new Vector3[smoothAmount];

        for (int index = 1; index <= smoothAmount; index++)
        {
            Vector3 currPt = FindPointPosition(controlPointList, (float)index / smoothAmount);
            pointList[index - 1] = currPt;
        }
        return pointList;
    }

    /// <summary>
    /// 获取曲线上面的所有点
    /// </summary>
    /// <returns>The list.</returns>
    /// <param name="path">需要穿过的点列表</param>
    /// <param name="pointSize">两个点之间的节点数量</param>
    public Vector3[] PointListAverage(Vector3[] path, int allSize)
    {
        Vector3[] controlPointList = PathControlPointGenerator(path);

        Vector3[] pointList = new Vector3[allSize];
        float[] weight = pathWeight(path, allSize);
        for (int index = 0; index < allSize; index++)
        {
            if (index == 0)
            {
                pointList[index] = path[0];
            }
            if(index == allSize - 1)
            {
                pointList[index] = path[path.Length - 1];
            }
            Vector3 currPt = FindPointPosition(controlPointList, weight[index]);
            pointList[index] = currPt;
        }
        return pointList;
    }

    /// <summary>
    /// 获取曲线上面的所有点
    /// </summary>
    /// <returns>The list.</returns>
    /// <param name="path">需要穿过的点列表</param>
    /// <param name="pointSize">两个点之间的节点数量</param>
    public Vector3[] PointListAverage(Vector3[] path, float dx)
    {
        meshInitialized = false;
        Vector3[] controlPointList = PathControlPointGenerator(path);

        List<Vector3> pointList = new List<Vector3>();
        List<float> pointT = new List<float>();
        List<Vector3> pointNormal = new List<Vector3>();
        float deviation = 0.01f;
        float dt = 0.01f;
        float t = 0;

        Vector3 prePt = path[0];
        pointList.Add(prePt);
        pointT.Add(t);
        pointNormal.Add(Velocity(controlPointList, t));
        while (t + dt < 1)
        {
            Vector3 tempPt = FindPointPosition(controlPointList, t + dt);
            float dd = Vector3.Distance(prePt, tempPt);
            float dmax = 0f;
            float dmin = 0f;
            while(Math.Abs(dd - dx) > dx * deviation)
            {
                if(dd > dx)
                {
                    dmax = dt;
                    dt = (dmax + dmin) / 2;
                }
                else
                {
                    dmin = dt;
                    if(dmax != 0)
                        dt = (dmax + dmin) / 2;
                    else
                        dt = dt * 2;
                }
                tempPt = FindPointPosition(controlPointList, t + dt);
                dd = Vector3.Distance(prePt, tempPt);
            }
            t = t + dt;
            prePt = tempPt;
            pointList.Add(prePt);
            pointT.Add(t);
            pointNormal.Add(Velocity(controlPointList, t));
        }
        pointList.Add(path[path.Length - 1]);
        pointT.Add(1);
        pointNormal.Add(Velocity(controlPointList, t));
        this.pointList = pointList.ToArray();
        this.pointT = pointT.ToArray();
        this.pointNormal = pointNormal.ToArray();
        return pointList.ToArray();
    }

    public Vector3[] GetPathUp(float lineWidth)
    {
        InitMeshPath(lineWidth);
        return this.lineUpPath;
    }

    public Vector3[] GetPathDown(float lineWidth)
    {
        InitMeshPath(lineWidth);
        return this.lineDowmPath;
    }

    private void InitMeshPath(float lineWidth)
    {
        if (this.lineWidth != lineWidth) meshInitialized = false;
        if (meshInitialized) return;
        meshInitialized = true;
        this.lineWidth = lineWidth;
        this.lineUpPath = new Vector3[this.pointList.Length];
        this.lineDowmPath = new Vector3[this.pointList.Length];
        for (int i = 0; i < this.pointList.Length; i++)
        {
            Vector3 normal = this.pointNormal[i];
            Vector3 ortho = Vector3.zero;
            Vector3.OrthoNormalize(ref normal, ref ortho);
            this.lineUpPath[i] = this.pointList[i] + ortho * this.lineWidth;
            this.lineDowmPath[i] = this.pointList[i] - ortho * this.lineWidth;
        }
    }

    private Vector3 GetNormal(Vector3[] path,Vector3 pt,float t)
    {
        float deviation = 0.00001f * Vector3.Distance(path[0],path[1]);
        float dt = 0.01f;
        Vector3 temp = Vector3.zero;
        if (t == 1)
        {
            temp = FindPointPosition(path, t - dt);
            while (Vector3.Distance(pt, temp) > deviation)
            {
                dt = dt / 2;
                temp = FindPointPosition(path, t - dt);
            }
            return pt - temp;
        }
        temp = FindPointPosition(path, t + dt);
        while(Vector3.Distance(pt,temp) > deviation)
        {
            dt = dt / 2;
            temp = FindPointPosition(path, t + dt);
        }
        return temp - pt;
    }

    public float[] pathWeight(Vector3[] path,int allSize)
    {
        float distance = 0f;
        float[] distances = new float[path.Length];
        float[] dDistances = new float[path.Length];
        for(int i=0; i < path.Length; i++)
        {
            float d = 0;
            if (i != 0) 
                d = Vector3.Distance(path[i - 1], path[i]);
            distance += d;
            distances[i] = distance;
            dDistances[i] = d;
        }
        float[] weight = new float[allSize];
        float averageD = distance / (allSize - 1);
        int index = 0;
        for (int i = 0; i < allSize; i++)
        {
            if (i == 0)
            {
                weight[i] = 0;
                continue;
            }
            if (i == allSize - 1)
            {
                weight[i] = 1;
                continue;
            }
            float d = i * averageD;
            while(d > distances[index] && index < distances.Length - 1)
            {
                index++;
            }
            weight[i] = (float)(index - 1) / (path.Length - 1) + (d - distances[index - 1]) / (distances[index] - distances[index - 1]) / (path.Length - 1);
        }
        return weight;
    }

    /// <summary>
    /// 获取控制点
    /// </summary>
    /// <returns>The control point generator.</returns>
    /// <param name="path">Path.</param>
    public Vector3[] PathControlPointGenerator(Vector3[] path)
    {
        int offset = 2;
        Vector3[] suppliedPath = path;
        Vector3[] controlPoint = new Vector3[suppliedPath.Length + offset];
        Array.Copy(suppliedPath, 0, controlPoint, 1, suppliedPath.Length);

        controlPoint[0] = controlPoint[1] + (controlPoint[1] - controlPoint[2]);
        controlPoint[controlPoint.Length - 1] = controlPoint[controlPoint.Length - 2] + (controlPoint[controlPoint.Length - 2] - controlPoint[controlPoint.Length - 3]);

        if (controlPoint[1] == controlPoint[controlPoint.Length - 2])
        {
            Vector3[] tmpLoopSpline = new Vector3[controlPoint.Length];
            Array.Copy(controlPoint, tmpLoopSpline, controlPoint.Length);
            tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
            tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
            controlPoint = new Vector3[tmpLoopSpline.Length];
            Array.Copy(tmpLoopSpline, controlPoint, tmpLoopSpline.Length);
        }

        return (controlPoint);
    }

    /// <summary>
    /// 根据 T 获取曲线上面的点位置
    /// </summary>
    /// <param name="pts">Pts.</param>
    /// <param name="t">T.</param>
    private Vector3 FindPointPosition(Vector3[] pts, float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);

        Vector3 a = pts[currPt];
        Vector3 b = pts[currPt + 1];
        Vector3 c = pts[currPt + 2];
        Vector3 d = pts[currPt + 3];

        float u = t * (float)numSections - (float)currPt;
        return .5f * (
            (-a + 3f * b - 3f * c + d) * (u * u * u)
            + (2f * a - 5f * b + 4f * c - d) * (u * u)
            + (-a + c) * u
            + 2f * b
            );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pts"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public Vector3 Velocity(Vector3[] pts, float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
        float u = t * (float)numSections - (float)currPt;

        Vector3 a = pts[currPt];
        Vector3 b = pts[currPt + 1];
        Vector3 c = pts[currPt + 2];
        Vector3 d = pts[currPt + 3];

        return 1.5f * (-a + 3f * b - 3f * c + d) * (u * u)
                + (2f * a - 5f * b + 4f * c - d) * u
                + .5f * c - .5f * a;
    }

    /// <summary>
    /// 根据 T 获取曲线上面的点切线
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private Vector3 FindPointTangent(Vector3[] pts, float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections - 1);
        float u = t * (float)numSections - (float)currPt;

        Vector3 p0 = pts[currPt];
        Vector3 p1 = pts[currPt + 1];
        Vector3 p2 = pts[currPt + 2];
        Vector3 p3 = pts[currPt + 3];

        float t2 = t * t;

        return 0.5f * (-p0 + p2) +
            (2.0f * p0 - 5.0f * p1 + 4 * p2 - p3) * t +
            (-p0 + 3.0f * p1 - 3.0f * p2 + p3) * t2 * 1.5f;
    }

    

}

public class BlinePoint
{
    public Vector3 position;
    public Vector3 normal;
    public Vector3 upPosition;
    public Vector3 dowmPosition;
}
