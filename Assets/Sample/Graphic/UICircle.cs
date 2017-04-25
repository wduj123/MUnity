// ========================================================
// 描 述：NewBehaviourScript.cs 
// 作 者：郑贤春 
// 时 间：2017/02/28 15:57:08 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICircle : Image
{
    [SerializeField]
    [Range(5, 100)]
    private int m_size = 5;

    private Vector2[] m_path;

    protected override void Start()
    {
        CaculateCirclePath();
        base.Start();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        CaculateCirclePath();
        var rect = GetPixelAdjustedRect();
        var uv = (overrideSprite != null) ? UnityEngine.Sprites.DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;
        vh.Clear();
        vh.AddVert(new Vector3(rect.x + rect.width * 0.5f, rect.y + rect.height * 0.5f), color, new Vector2(Mathf.Lerp(uv.x, uv.z, 0.5f), Mathf.Lerp(uv.y, uv.w, 0.5f)));
        for (int i = 0; i < this.m_size; i++)
        {
            Vector2 po = this.m_path[i];
            Vector2 pouv = new Vector2(Mathf.Lerp(uv.x, uv.z, (po.x + 1) * 0.5f), Mathf.Lerp(uv.y, uv.w, (po.y + 1) * 0.5f));
            vh.AddVert(new Vector3(rect.x + rect.width * 0.5f + po.x * rect.width * 0.5f, rect.y + rect.height * 0.5f + po.y * rect.height * 0.5f), color, pouv);
            if (i == this.m_size - 1)
            {
                vh.AddTriangle(0, 1, i + 1);
            }
            else
            {
                vh.AddTriangle(0, i + 2, i + 1);
            }

        }
    }

    void CaculateCirclePath()
    {
        if (this.m_path == null || this.m_path.Length != this.m_size)
        {
            this.m_path = CaculateCirclePath(this.m_size);
        }
    }

    Vector2[] CaculateCirclePath(int size)
    {
        Vector2[] path = new Vector2[size];
        for (int i = 0; i < size; i++)
        {
            float angle = 2 * Mathf.PI / size * i;
            path[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        return path;
    }
}
