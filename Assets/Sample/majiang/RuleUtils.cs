using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 手牌必须是排序的
/// </summary>
public class RuleUtils
{
    /// <summary>
    /// 判断是否为七对
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    public static bool CheckDouble7(Every[] everys)
    {
        for (int i = 0; i < 14; i = i + 2)
        {
            if (everys[i].getIf_Konged() == true || everys[i].getIf_Touched() == true)
                return false;
            if (everys[i].getColor() != everys[i + 1].getColor() || everys[i].getNumber() != everys[i + 1].getNumber())
                return false;
        }
        return true;
    }

    /// <summary>
    /// 判断是否可以碰牌
    /// </summary>
    /// <param name="everys"></param>
    /// <param name="every"></param>
    /// <returns></returns>
    public static int CheckPeng(Every[] everys, Every every)
    {
        for (int i = 0; i < everys.Length - 1; i++)
        {
            if (everys[i].getColor() == every.getColor() && everys[i].getNumber() == every.getNumber()
                    && everys[i + 1].getColor() == every.getColor() && everys[i + 1].getNumber() == every.getNumber())
            {
                if (everys[i].getIf_Touched() == false && everys[i + 1].getIf_Touched() == false)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// 判断能否暗杠 前提手牌已排序， 能，返回杠的牌的索引 不能，返回-1
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    public static int CheckGangUnSee(Every[] everys)
    {
        int sum;
        for (int i = 0; i < everys.Length - 3; i++)
        {
            if (everys[i].getIf_Konged() == true)
            {
                i += 3;
                continue;
            }
            sum = 0;
            for (int j = 1; j < 4; j++)
            {
                // 判断连续两个牌是否花色和数字一样
                if (everys[i].getColor() == everys[i + j].getColor()
                        && everys[i].getNumber() == everys[i + j].getNumber() && everys[i].getColor() < 4)
                {
                    sum++;
                }
                else
                {
                    i += j - 1;
                    break;
                }
            }
            // 如果sum==3，则是可以杠
            if (sum == 3)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 判断是否可以杠牌 前提手牌已排序 能，返回1 的牌的索引，返回-1
    /// </summary>
    /// <param name="everys"></param>
    /// <param name="every"></param>
    /// <returns></returns>
    public static int CheckKong(Every[] everys, Every every)
    {
        for (int i = 0; i < everys.Length - 2; i++)
        {
            if (everys[i].getColor() == every.getColor() && everys[i].getNumber() == every.getNumber()
                    && everys[i + 1].getColor() == every.getColor() && everys[i + 1].getNumber() == every.getNumber()
                    && everys[i + 2].getColor() == every.getColor() && everys[i + 2].getNumber() == every.getNumber())
            {
                if (everys[i].getIf_Touched() == true)
                    return -1;
                else
                    return i;
            }
        }
        return -1;
    }

    /// <summary>
    ///  排序规则1、有花色在前 2、未碰 未杠的在前 3、花色顺序 筒 条 万 4、数值由小到大
    /// </summary>
    /// <param name="everys"></param>
    public static void Sort_mj(Every[] everys)
    {
        Every temp;
        int[] wage = new int[18];
        for (int j = 0; j < 18; j++)
        {
            if (everys[j].getIf_Konged() == true || everys[j].getIf_Touched() == true)
            {
                wage[j] = 1;
            }
            else
                wage[j] = 0;
        }
        for (int j = 17; j >= 0; j--)
        {
            for (int k = 0; k < j; k++)
            {
                temp = everys[k];
                int x = wage[k];
                if (everys[k + 1].getColor() * 10 + wage[k + 1] * 100 + everys[k + 1].getNumber() < wage[k] * 100
                        + everys[k].getColor() * 10 + everys[k].getNumber())
                {
                    everys[k] = everys[k + 1];
                    everys[k + 1] = temp;
                    wage[k] = wage[k + 1];
                    wage[k + 1] = x;
                }
            }
        }
    }

    /// <summary>
    /// 是否有碰或杠的牌
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    private static bool IsOncePengOrGang(Every[] everys)
    {
        for (int i = 0; i < everys.Length; i++)
        {
            if (everys[i].getIf_Konged() || everys[i].getIf_Touched())
                return true;
        }
        return false;
    }

    /// <summary>
    /// 是否有杠的牌
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    private static bool IsOnceGang(Every[] everys)
    {
        for (int i = 0; i < everys.Length; i++)
        {
            if (everys[i].getIf_Konged())
                return true;
        }
        return false;
    }

    /// <summary>
    /// 是否碰的牌
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    private static bool IsOncePeng(Every[] everys)
    {
        for (int i = 0; i < everys.Length; i++)
        {
            if (everys[i].getIf_Touched())
                return true;
        }
        return false;
    }

    /// <summary>
    /// 判断听牌情况
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    public static Dictionary<int, Dictionary<Every, HuPaiResult>> IsTing(Every[] everys)
    {
        Dictionary<int, Dictionary<Every,HuPaiResult>> result = new Dictionary<int, Dictionary<Every, HuPaiResult>>();
        for (int i = 0; i < everys.Length; i++)
        {
            Dictionary<Every, HuPaiResult> item = new Dictionary<Every, HuPaiResult>();
            if (everys[i].getColor() <= 3 && !everys[i].getIf_Konged() && !everys[i].getIf_Touched())// 非空，非杠，非空
            {
                int oldColor = everys[i].getColor();
                int oldNumber = everys[i].getNumber();
                
                for (int color = 1; color <= 3; color++)
                {
                    for (int number = 1; number <= 9; number++)
                    {
                        everys[i].setColor(color);
                        everys[i].setNumber(number);
                        HuPaiResult huResult = IsHu(everys);
                        if (!huResult.type.Equals(HuBaseType.None))
                        {
                            item.Add(new Every(color,number,false,false),huResult);
                        }
                    }
                }
                everys[i].setColor(oldColor);
                everys[i].setNumber(oldNumber);
            }
            result.Add(i, item);
        }
        return result;
    }

    /// <summary>
    /// 判断是否胡牌 两种情况，满足 ： 基本胡牌型 、 七对
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    public static bool IsHuSimple(Every[] everys)
    {
        return false;
    }

    /// <summary>
    /// 判断自摸胡
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    public static HuPaiResult IsHu(Every[] everys)
    {
        return IsHu(everys, false);
    }

    /// <summary>
    /// 判断胡 flag标识自摸或胡其他玩家 特例：天胡 、地胡
    /// </summary>
    /// <param name="everys"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static HuPaiResult IsHu(Every[] everys, bool other)
    {
        int[] arrPaiAll = ToArrPaiAll(everys);
        int[] arrPai = ToArrPai(everys);
        int[] arrPaiGangOrPeng = ToArrPaiGangOrPeng(everys);

        HuPaiResult huResult = new HuPaiResult();
        huResult.type = HuBaseType.None;

        // 基本番
        if (IsHuBase(arrPai)) // 满足基本胡牌型
        {
            if (IsHuQingShiBaLuoHan(arrPaiAll))
            { // 清十八罗汉
                huResult.type = HuBaseType.QingShiBaLuoHan;
            }
            else if (IsHuShiBaLuoHan(arrPaiAll))
            { // 十八罗汉
                huResult.type = HuBaseType.ShiBaLuoHan;
            }
            else if (IsHuJinGouGou(arrPaiGangOrPeng) && IsHuQingYiSe(arrPaiAll))
            { // 清金钩钩
                huResult.type = HuBaseType.QingJingGouGou;
            }
            else if (IsHuJinGouGou(arrPaiGangOrPeng) && IsHuJiangJinGouGou(arrPaiGangOrPeng))
            {// 将金钩钩
                huResult.type = HuBaseType.JiangJinGouGou;
            }
            else if (IsHuDaiYaoJiu(arrPai, arrPaiGangOrPeng) && IsHuQingYiSe(arrPaiAll))
            { // 清幺九
                huResult.type = HuBaseType.QingYaoJiu;
            }
            else if (IsHuJinGouGou(arrPaiGangOrPeng))
            { // 金钩钩
                huResult.type = HuBaseType.JinGouGou;
            }
            else if (IsHuDaiYaoJiu(arrPai, arrPaiGangOrPeng))
            { // 带幺九
                huResult.type = HuBaseType.DaiYaoJiu;
            }
            else if (IsHuQingYiSe(arrPaiAll))
            { // 清一色
                huResult.type = HuBaseType.QingYiSe;
            }
            else if (IsPengPengHu(arrPaiAll))
            { // 碰碰胡
                huResult.type = HuBaseType.PengPengHu;
            }
            else
            { // 平胡
                huResult.type = HuBaseType.PingHu;
            }
            // 满足基本胡牌型 也有可能满足非基本胡牌型 两者皆满足取大番者
            HuPaiResult result = IsHuNoneBase(everys, arrPai);
            if (result.type.fan > huResult.type.fan)
            {
                huResult.type = result.type;
            }
        }
        else
        { // 不满足基本胡牌型
            HuPaiResult result = IsHuNoneBase(everys, arrPai);
            huResult.type = result.type;
        }
        // 另加番
        return huResult;
    }

    /// <summary>
    /// 非基本胡牌判断
    /// </summary>
    /// <param name="everys"></param>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static HuPaiResult IsHuNoneBase(Every[] everys, int[] arrPai)
    {
        HuPaiResult huResult = new HuPaiResult();
        huResult.type = HuBaseType.None;
        if (!IsOncePengOrGang(everys)) // 是否碰或杠过
        {
            if (IsHuQingLongQiDui(arrPai))
            { // 清龙七对
                huResult.type = HuBaseType.QingLongQiDui;
            }
            else if (IsHuQingQiDui(arrPai))
            { // 清七对
                huResult.type = HuBaseType.QingQiDui;
            }
            else if (IsHuLongQiDui(arrPai))
            { // 龙七对
                huResult.type = HuBaseType.LongQiDui;
            }
            else if (IsHuQiDui(arrPai))
            { // 七对
                huResult.type = HuBaseType.QiDui;
            }
        }
        return huResult;
    }

    /// <summary>
    /// 是否胡 清十八罗汉
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQingShiBaLuoHan(int[] arrPai)
    {
        if (!IsHuShiBaLuoHan(arrPai))
            return false;
        if (!IsSameColor(arrPai))
            return false;
        return true;
    }

    /// <summary>
    /// 是否胡 十八罗汉
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuShiBaLuoHan(int[] arrPai)
    {
        if (HasGang(arrPai) == 4) // 是否有四个杠
            return true;
        return false;
    }

    /// <summary>
    /// 是否胡 清龙七对
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQingLongQiDui(int[] arrPai)
    {
        if (!IsSameColor(arrPai)) // 是否清一色
            return false;
        if (!IsHuQiDui(arrPai)) // 是否七对
            return false;
        if (HasGang(arrPai) < 1) // 是否有杠
            return false;
        return true;
    }

    /// <summary>
    /// 胡地胡 特例
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuDiHu(int[] arrPai)
    {
        return false;
    }

    /// <summary>
    /// 胡天胡 特例
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuTianHu(int[] arrPai)
    {
        return false;
    }

    /// <summary>
    /// 胡清金钩钩 用胡金钩钩和胡清一色联合判定
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQingJinGouGou(int[] arrPai)
    {
        return false;
    }

    /// <summary>
    /// 胡将金钩钩 配合金钩钩判定
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuJiangJinGouGou(int[] arrPai)
    {
        for (int i = 0; i < arrPai.Length; i++)
        {
            if (i % 9 != 1 && i % 9 != 4 && i % 9 != 7)
            {// 当数值不为 2、5、8时
                if (arrPai[i] > 0)
                    return false;
            }
            if (i > 26 && arrPai[i] > 0)
                return false; // 不是筒、条、万时
        }
        return true;
    }

    /// <summary>
    /// 胡清幺九 与胡带幺九 和清一色一起判定
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQingYaoJiu(int[] arrPai)
    {
        return false;
    }

    /// <summary>
    /// 是否胡清七对
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQingQiDui(int[] arrPai)
    {
        if (!IsSameColor(arrPai))
            return false;
        if (!IsHuQiDui(arrPai))
            return false;
        return true;
    }

    /// <summary>
    /// 是否胡龙七对
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuLongQiDui(int[] arrPai)
    {
        if (!IsHuQiDui(arrPai))
            return false;
        if (HasGang(arrPai) < 1)
            return false;
        return true;
    }

    /// <summary>
    /// 是否胡金钩钩
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuJinGouGou(int[] arrPai)
    {
        int count = 0;
        for (int i = 0; i < arrPai.Length; i++)
        {
            if (arrPai[i] < 3 && arrPai[i] > 0)
            { // 有顺子牌
                return false;
            }
            else if (arrPai[i] >= 3)
            { // 累计杠和碰
                count++;
            }
        }
        if (count == 4)// 碰或杠过四次
            return true;
        return false;
    }

    /// <summary>
    /// 是否胡七对
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQiDui(int[] arrPai)
    {
        for (int i = 0; i < arrPai.Length; i++)
        {
            if (arrPai[i] == 1 || arrPai[i] == 3) // 是否对子
                return false;
        }
        return true;
    }

    /// <summary>
    /// 是否胡带幺九
    /// </summary>
    /// <param name="arrPai"></param>
    /// <param name="arrPaiGangOrPeng"></param>
    /// <returns></returns>
    private static bool IsHuDaiYaoJiu(int[] arrPai, int[] arrPaiGangOrPeng)
    {

        int[] newArrPai = new int[arrPai.Length];
        for (int i = 0; i < arrPaiGangOrPeng.Length; i++)
        {
            newArrPai[i] = arrPai[i];
            if (i % 9 != 0 && i % 9 != 8)
                if (arrPaiGangOrPeng[i] > 0)
                    return false;
        }
        return IsHuDaiYaoJiu(newArrPai, 0);
    }

    /// <summary>
    /// 是否由 1或9连牌组成的胡牌
    /// </summary>
    /// <param name="arrPai"></param>
    /// <param name="Jiang"></param>
    /// <returns></returns>
    private static bool IsHuDaiYaoJiu(int[] arrPai, int Jiang)
    {
        if (remain(arrPai) == 0)
            return true;
        int i = 0;
        while (i < arrPai.Length && arrPai[i] == 0)
        {
            i++;
        }
        if (arrPai[i] >= 3 && (i % 9 == 0 || i % 9 == 8))
        {
            arrPai[i] = arrPai[i] - 3;
            if (IsHuDaiYaoJiu(arrPai, Jiang))
                return true;
            arrPai[i] = arrPai[i] + 3;
        }
        if (Jiang == 0 && arrPai[i] >= 2)
        {
            Jiang = 1;
            arrPai[i] = arrPai[i] - 2;
            if (IsHuDaiYaoJiu(arrPai, Jiang))
                return true;
            arrPai[i] = arrPai[i] + 2;
            Jiang = 0;
        }
        if (i > 26)
            return false;
        if ((i % 9 == 0 || i % 9 == 6) && (arrPai[i + 1] > 0) && (arrPai[i + 2] > 0))
        {
            arrPai[i] = arrPai[i] - 1;
            arrPai[i + 1] = arrPai[i + 1] - 1;
            arrPai[i + 2] = arrPai[i + 2] - 1;
            if (IsHuDaiYaoJiu(arrPai, Jiang))
                return true;
            arrPai[i] = arrPai[i] + 1;
            arrPai[i + 1] = arrPai[i + 1] + 1;
            arrPai[i + 2] = arrPai[i + 2] + 1;
        }
        return false;
    }

    /// <summary>
    /// 是否胡清一色
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuQingYiSe(int[] arrPai)
    {
        return IsSameColor(arrPai);
    }

    /// <summary>
    /// 是否胡 碰碰胡
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsPengPengHu(int[] arrPai)
    {
        int count = 0;
        for (int i = 0; i < arrPai.Length; i++)
        {
            if (arrPai[i] > 2)
                count++;
        }
        return count == 4 ? true : false; // 是否有四个刻子
    }

    /// <summary>
    /// 杠的数量
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static int HasGang(int[] arrPai)
    {
        int count = 0;
        for (int i = 0; i < arrPai.Length; i++)
        {
            if (arrPai[i] == 4)
                count++;
        }
        return count;
    }

    /// <summary>
    /// 是否同色
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsSameColor(int[] arrPai)
    {
        int i = 0;
        while (i < 27 && arrPai[i] == 0)
        {
            i++;
        }
        int color = i / 9;
        for (int j = (color + 1) * 9; j < arrPai.Length; j++)
        {
            if (arrPai[j] > 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 判断基本胡牌 arrPai[34] 一维数组格式 9 + 9 + 9 + 7 对应 筒9 + 条9 + 万9 + （风牌 5、白板、发财）
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static bool IsHuBase(int[] arrPai)
    {
        int[] newArrPai = new int[arrPai.Length];
        for (int i = 0; i < arrPai.Length; i++)
        {
            newArrPai[i] = arrPai[i];
        }
        return IsHuBase(newArrPai, 0);
    }

    /// <summary>
    /// 判断基本胡牌 arrPai[] 一维数组格式 9 + 9 + 9 + 7 对应 筒9 + 条9 + 万9 + （风牌 5、白板、发财 将牌 是否有
    /// </summary>
    /// <param name="arrPai"></param>
    /// <param name="Jiang"></param>
    /// <returns></returns>
    private static bool IsHuBase(int[] arrPai, int Jiang)
    {
        if (remain(arrPai) == 0)
            return true;
        int i = 0;
        while (i < arrPai.Length && arrPai[i] == 0)
        {
            i++;
        }
        if (arrPai[i] >= 3)
        {
            arrPai[i] = arrPai[i] - 3;
            if (IsHuBase(arrPai, Jiang))
                return true;
            arrPai[i] = arrPai[i] + 3;
        }
        if (Jiang == 0 && arrPai[i] >= 2)
        {
            Jiang = 1;
            arrPai[i] = arrPai[i] - 2;
            if (IsHuBase(arrPai, Jiang))
                return true;
            arrPai[i] = arrPai[i] + 2;
            Jiang = 0;
        }
        if (i > 26)
            return false;
        if ((i % 9 < 9 - 2) && (arrPai[i + 1] > 0) && (arrPai[i + 2] > 0))
        {
            arrPai[i] = arrPai[i] - 1;
            arrPai[i + 1] = arrPai[i + 1] - 1;
            arrPai[i + 2] = arrPai[i + 2] - 1;
            if (IsHuBase(arrPai, Jiang))
                return true;
            arrPai[i] = arrPai[i] + 1;
            arrPai[i + 1] = arrPai[i + 1] + 1;
            arrPai[i + 2] = arrPai[i + 2] + 1;
        }
        return false;
    }

    /// <summary>
    /// 判断是否全零，全零则胡
    /// </summary>
    /// <param name="arrPai"></param>
    /// <returns></returns>
    private static int remain(int[] arrPai)
    {
        for (int i = 0; i < arrPai.Length; i++)
            if (arrPai[i] != 0)
                return 1;
        return 0;
    }

    /// <summary>
    /// 转为数组 只包括未杠 和 未碰 主要用于计算基本胡牌
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    private static int[] ToArrPai(Every[] everys)
    {
        int[] arrPai = new int[34];
        for (int i = 0; i < everys.Length; i++)
        {
            if (everys[i].getIf_Konged() || everys[i].getIf_Touched() || everys[i].getColor() > 3)
                continue;
            int index = (everys[i].getColor() - 1) * 9 + everys[i].getNumber() - 1;
            arrPai[index]++;
        }
        return arrPai;
    }

    /// <summary>
    /// 转为数组 全部 主要用于计算 杠的数量
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    public static int[] ToArrPaiAll(Every[] everys)
    {
        int[] arrPai = new int[34];
        for (int i = 0; i < everys.Length; i++)
        {
            if (everys[i].getColor() > 3)
                continue;
            int index = (everys[i].getColor() - 1) * 9 + everys[i].getNumber() - 1;
            arrPai[index]++;
        }
        return arrPai;
    }

    /// <summary>
    /// 转为数组 转为数组 只包括杠 和 碰
    /// </summary>
    /// <param name="everys"></param>
    /// <returns></returns>
    private static int[] ToArrPaiGangOrPeng(Every[] everys)
    {
        int[] arrPai = new int[34];
        for (int i = 0; i < everys.Length; i++)
        {
            if ((!everys[i].getIf_Konged() && !everys[i].getIf_Touched()) || everys[i].getColor() > 3)
                continue;
            int index = (everys[i].getColor() - 1) * 9 + everys[i].getNumber() - 1;
            arrPai[index]++;
        }
        return arrPai;
    }
}