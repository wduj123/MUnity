using UnityEngine;
using System.Collections;
using System.Threading;

public class AutoResetEventTest : MonoBehaviour {

    private static AutoResetEvent event_1 = new AutoResetEvent(false);
    private static AutoResetEvent event_2 = new AutoResetEvent(false);

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "BeginTest"))
        {
            BeginTest();
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "SetEvent_1"))
        {
            SetEvent_1();
        }
        if (GUI.Button(new Rect(200, 0, 100, 50), "SetEvent_2"))
        {
            SetEvent_2();
        }
    }

    static void BeginTest()
    {
        Debug.Log("Press Enter to create three threads and start them.\r\n" +
                          "The threads wait on AutoResetEvent #1, which was created\r\n" +
                          "in the signaled state, so the first thread is released.\r\n" +
                          "This puts AutoResetEvent #1 into the unsignaled state.");
        for (int i = 1; i < 4; i++)
        {
            Thread t = new Thread(ThreadProc);
            t.Name = "Thread_" + i;
            t.Start();
        }
    }

    static void SetEvent_1()
    {
        event_1.Set();
    }

    static void SetEvent_2()
    {
        event_2.Set();
    }

    static void ThreadProc()
    {
        string name = Thread.CurrentThread.Name;

        Debug.Log(string.Format("{0} waits on AutoResetEvent #1.", name));
        event_1.WaitOne(5000 * int.Parse(name.Substring(name.Length - 1)), false);
        Debug.Log(string.Format("{0} is released from AutoResetEvent #1.", name));

        Debug.Log(string.Format("{0} waits on AutoResetEvent #2.", name));
        event_2.WaitOne();
        Debug.Log(string.Format("{0} is released from AutoResetEvent #2.", name));

        Debug.Log(string.Format("{0} ends.", name));
    }
}
