using UnityEngine;
using System.Collections;

public class Every
{
    private int number;// 麻将的数值
    private int color;// 麻将的花色，1 代表筒，2 代表条， 3 代表 万
    private bool If_Touched;
    private bool If_Konged;

    public Every(int n, int c, bool If_Touched, bool If_Konged)
    {
        setNumber(n);
        setColor(c);
        setIf_Touched(If_Touched);
        setIf_Konged(If_Konged);
    }

    public Every()
    {
        number = 0;
        color = 1000;
        If_Touched = false;
        If_Konged = false;
    }

    /// <summary>
    /// 设置数值
    /// </summary>
    /// <param name="n"></param>
    public void setNumber(int n)
    {
        number = n;
    }

    /// <summary>
    /// 设置花色
    /// </summary>
    /// <param name="a"></param>
    public void setColor(int a)
    {
        color = a;
    }

    /// <summary>
    /// 设置碰
    /// </summary>
    /// <param name="a"></param>
    public void setIf_Touched(bool a)
    {
        If_Touched = a;
    }

    /// <summary>
    /// 设置杠
    /// </summary>
    /// <param name="a"></param>
    public void setIf_Konged(bool a)
    {
        If_Konged = a;
    }

    /// <summary>
    /// 获取麻将数值
    /// </summary>
    /// <returns></returns>
    public int getNumber()
    {
        return number;
    }

    /// <summary>
    /// 获取麻将花色：1-筒；2-条；3-萬
    /// </summary>
    /// <returns></returns>
    public int getColor()
    {
        return color;
    }

    /// <summary>
    /// 碰
    /// </summary>
    /// <returns></returns>
    public bool getIf_Touched()
    {
        return If_Touched;
    }

    /// <summary>
    /// 杠
    /// </summary>
    /// <returns></returns>
    public bool getIf_Konged()
    {
        return If_Konged;
    }

    public override string ToString()
    {
        if (getColor() == 1)
            return getNumber() + "筒  ";
        else if (getColor() == 2)
            return getNumber() + "条  ";
        else if (getColor() == 3)
            return getNumber() + "万 ";
        return null;
    }
}

