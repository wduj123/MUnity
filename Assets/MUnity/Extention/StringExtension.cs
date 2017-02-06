using UnityEngine;
using System.Collections;

public static class StringExtension{

	public static string Extention(this string str)
    {
        return str.Substring(str.LastIndexOf("."));
    }

    public static string NameWithoutExtention(this string str)
    {
        return str.Substring(0,str.LastIndexOf("."));
    }
}
