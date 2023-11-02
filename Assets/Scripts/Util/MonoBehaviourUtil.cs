using System;
using System.Collections;
using UnityEngine;

namespace Land.Utils
{
    public static class MonoBehaviourUtil
    {
        public static Coroutine Delay(this MonoBehaviour obj, float delay, Action action)
        {
            return obj.StartCoroutine(DoDelay());

            IEnumerator DoDelay()
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }
        }
    }
}