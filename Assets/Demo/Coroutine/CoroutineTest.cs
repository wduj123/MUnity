using UnityEngine;
using System.Collections;
using MUnity.Coroutine;

public class CoroutineTest : MonoBehaviour
{

    void Start()
    {

        // start unity coroutine

        StartCoroutine(UnityCoroutine());

        // start self coroutine

        CoroutineManager.Instance.StartCoroutineSimple(SelfCoroutine());

    }

    IEnumerator UnityCoroutine()
    {

        Debug.Log("Unity coroutine begin at time : " + Time.time);

        yield return new WaitForSeconds(1);

        Debug.Log("Unity coroutine begin at time : " + Time.time);

    }

    IEnumerator SelfCoroutine()
    {

        while(true)
        {
            Debug.Log("Self coroutine begin at time : " + Time.time);

            yield return new CoroutineWaitForSeconds(1);

            Debug.Log("Self coroutine begin at time : " + Time.time);
        }
        

    }

}
