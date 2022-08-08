using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        print(1);
        var val = await new MyTask<int>(Coroutine());
        print(2);
        print(val);
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(2);
        yield return 11;
    }
}
