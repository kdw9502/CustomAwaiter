using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Threading.Tasks;

public class MyTask<T>
{
    public IEnumerator coroutine;

    public MyTask(IEnumerator coroutine)
    {
        this.coroutine = coroutine;
    }
    public MyTaskAwaiter<T> GetAwaiter()
    {
        Debug.Log("GetAwaiter");
        var awaiter = new MyTaskAwaiter<T>();
        awaiter.StartCoroutine(coroutine);
        return awaiter;
    }
}


public class MyTaskAwaiter<T> :  INotifyCompletion
{
    private bool isCompleted;
    private T result;
    private List<Action> onFinished;
    public bool IsCompleted => isCompleted;

    public T GetResult() => result;

    public void StartCoroutine(IEnumerator coroutine)
    {
        isCompleted = false;
        onFinished = new List<Action>();
        CoroutineContainer.Instance.StartCoroutine(Wrapper(coroutine));
    }
    
    public void OnCompleted(Action continuation)
    {
        if (IsCompleted)
        {
            continuation?.Invoke();
        }
        else
        {
            Debug.Log("OnCompleted " + continuation);
            onFinished.Add(continuation);
        }
    }

    
    IEnumerator Wrapper(IEnumerator coroutine)
    {
        Debug.Log("Wrapper");
        
        while (coroutine.MoveNext())
        {
            Debug.Log("coroutine");

            var val = coroutine.Current;
            if (val is YieldInstruction)
                yield return null;
            else
                result = (T) val;
        }
        Debug.Log("isCompleted true");

        isCompleted = true;
        foreach (var action in onFinished)
        {
            action?.Invoke();
        }
    }

}
