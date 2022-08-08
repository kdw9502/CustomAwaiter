using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class TaskCompletionSource : MonoBehaviour
{
    private async void Start()
    {
        await Run(ExampleCoroutine());
    }
    
    // 결국 tcs 때문에 Task를 생성한다.
    private async ValueTask Run(IEnumerator coroutine)
    {
        var tcs = new TaskCompletionSource<object>();
        StartCoroutine(Wrapper(coroutine, tcs));
        var result = await tcs.Task;
        print(result);
    }
    
    IEnumerator Wrapper(IEnumerator coroutine, TaskCompletionSource<object> tcs)
    {
        while (coroutine.MoveNext())
        {
            var val = coroutine.Current;
            if (val is YieldInstruction)
                yield return null;
            else
                tcs.SetResult(val);
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);
        yield return 1;
    }
    
}
