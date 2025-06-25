using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public interface ICoroutineExecutor
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}