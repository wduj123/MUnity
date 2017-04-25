// ========================================================
// 描 述：HuBaseType.cs 
// 作 者：郑贤春 
// 时 间：2017/03/17 14:40:15 
// 版 本：5.4.1f1 
// ========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct HuBaseType
{
    public static HuBaseType None               = new HuBaseType(0,HuType.None);// 没胡 x 0
    public static HuBaseType PingHu             = new HuBaseType(1,HuType.PingHu);// 平胡 x 1
    public static HuBaseType PengPengHu         = new HuBaseType(2,HuType.PengPengHu);// 对对胡 x 2
    public static HuBaseType QingYiSe           = new HuBaseType(4,HuType.QingYiSe);// 清一色 x 4
    public static HuBaseType DaiYaoJiu          = new HuBaseType(4,HuType.DaiYaoJiu); // 带幺九 x 4
    public static HuBaseType QiDui              = new HuBaseType(4,HuType.QiDui); // 七对 x 4
    public static HuBaseType JinGouGou          = new HuBaseType(4,HuType.JinGouGou); // 金钩钩 x 4
    public static HuBaseType LongQiDui          = new HuBaseType(16,HuType.LongQiDui); // 龙七对 x 16
    public static HuBaseType QingQiDui          = new HuBaseType(16,HuType.QingQiDui); // 清七对 x 16
    public static HuBaseType QingYaoJiu         = new HuBaseType(16,HuType.QingYaoJiu); // 清幺九 x 16
    public static HuBaseType JiangJinGouGou     = new HuBaseType(16,HuType.JiangJinGouGou);// 将金钩钩 x 16
    public static HuBaseType QingJingGouGou     = new HuBaseType(16,HuType.QingJingGouGou); // 清金钩钩 x 16
    public static HuBaseType TianHu             = new HuBaseType(32,HuType.TianHu); // 天胡 x 32
    public static HuBaseType DiHu               = new HuBaseType(32,HuType.DiHu); // 地胡 x 32
    public static HuBaseType QingLongQiDui      = new HuBaseType(32,HuType.QingLongQiDui); // 清龙七对 x 32
    public static HuBaseType ShiBaLuoHan        = new HuBaseType(64,HuType.ShiBaLuoHan); // 十八罗汉 x 64
    public static HuBaseType QingShiBaLuoHan    = new HuBaseType(256,HuType.QingShiBaLuoHan); // 清十八罗汉 x 256

    public int fan;
    public HuType huType;

    private HuBaseType(int fan,HuType huType)
    {
        this.fan = fan;
        this.huType = huType;
    }
}

public enum HuType
{
    None,// 没胡 x 0
    PingHu,// 平胡 x 1
    PengPengHu,// 对对胡 x 2
    QingYiSe,// 清一色 x 4
    DaiYaoJiu,// 带幺九 x 4
    QiDui,// 七对 x 4
    JinGouGou,// 金钩钩 x 4
    LongQiDui,// 龙七对 x 16
    QingQiDui,// 清七对 x 16
    QingYaoJiu,// 清幺九 x 16
    JiangJinGouGou,// 将金钩钩 x 16
    QingJingGouGou,// 清金钩钩 x 16
    TianHu,// 天胡 x 32
    DiHu,// 地胡 x 32
    QingLongQiDui,// 清龙七对 x 32
    ShiBaLuoHan,// 十八罗汉 x 64
    QingShiBaLuoHan// 清十八罗汉 x 256
}
