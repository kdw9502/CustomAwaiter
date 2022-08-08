using System;
using UnityEngine;

public class CoroutineContainer : MonoBehaviour
{
    private static readonly Lazy<CoroutineContainer> lazy = new(() => new GameObject().AddComponent<CoroutineContainer>());
    public static CoroutineContainer Instance = lazy.Value;

}