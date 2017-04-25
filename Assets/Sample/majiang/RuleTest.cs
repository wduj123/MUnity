using UnityEngine;
using System.Collections;

public class RuleTest : MonoBehaviour
{

    void Start()
    {
        test();
    }

    /// <summary>
    /// ����
    /// </summary>
    private static void test()
    {
        // ��λ��1ͬ��2����3��
        // ʮλ��0�ޣ�������ֵ
        // ��λ��0�ޣ�1����2��
        int[] everyData = new int[] { 110, 110, 110, 000, 230, 230, 230, 240, 240, 240, 250, 250, 250, 290, 290, 000,
                000, 000 };
        Every[] everys = new Every[18];
        for (int i = 0; i < everyData.Length; i++)
        {
            Every every = new Every();
            every.setNumber(everyData[i] / 10 % 10);
            every.setColor(everyData[i] / 100 == 0 ? 1000 : everyData[i] / 100);
            if (everyData[i] % 10 == 1)
            {
                every.setIf_Touched(true);
            }
            else if (everyData[i] % 10 == 2)
            {
                every.setIf_Konged(true);
            }
            everys[i] = every;
        }
        HuPaiResult result = RuleUtils.IsHu(everys);
        Debug.Log(result.type.huType);
    }
}
