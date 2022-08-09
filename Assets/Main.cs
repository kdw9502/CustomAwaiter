using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        print(1);
        var myTask = new MyTask<int>(Coroutine());
        var val = await myTask;
        print(2);
        print(val);
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(1);
        yield return 11;
    }
}
