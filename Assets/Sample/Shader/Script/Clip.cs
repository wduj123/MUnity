using UnityEngine;
using System.Collections;

public class Clip : MonoBehaviour
{
    [HideInInspector]
    private Material m_Material;
    public Material Material
    {
        get
        {
            return this.m_Material;
        }
        set
        {
            this.m_Material = value;
            GetComponent<SpriteRenderer>().material = this.m_Material;
        }
    }

    public float rangeX;

}
