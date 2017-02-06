using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MUnity.Coroutine
{
    

    public class CoroutineYieldInstruction
    {

        public virtual bool IsDone()
        {

            return true;

        }

    }

    public class CoroutineWaitForSeconds : CoroutineYieldInstruction
    {

        float m_waitTime;

        float m_startTime;

        public CoroutineWaitForSeconds(float waitTime)
        {

            m_waitTime = waitTime;

            m_startTime = -1;

        }

        public override bool IsDone()
        {

            // NOTE: a little tricky here

            if (m_startTime < 0)
            {

                m_startTime = Time.time;

            }

            // check elapsed time

            return (Time.time - m_startTime) >= m_waitTime;

        }

    }

    

    //接着便是我们自己的WaitForSeconds了，不过在此之前我们先来实现WaitForSeconds的基类，CoroutineYieldInstruction：

    //

    //    <maintainer>Hugo</maintainer>

    //    <summary>coroutine yield instruction base class</summary>

    //
}
