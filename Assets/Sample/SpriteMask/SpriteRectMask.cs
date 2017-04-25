// ========================================================
// 描 述：SpriteMask.cs 
// 作 者：郑贤春 
// 时 间：2017/03/06 14:44:05 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteRectMask : MonoBehaviour {

    private static List<SpriteRectMask> spriteRectMasks;

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Awake()
    {
        if (spriteRectMasks == null)
            spriteRectMasks = new List<SpriteRectMask>();
        spriteRectMasks.Add(this);
    }

    void Start()
    {

    }

    public Rect GetClipRect(ClipableSprite sprite)
    {
        return new Rect(0, 0, 0, 0);
    }
}

public class ClipableSprite
{
    private SpriteRenderer m_renderer;
}
