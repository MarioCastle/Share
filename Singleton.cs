using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T :Component
{
    public static T Instance { get; private set;}

    protected virtual void Awake(){ //让继承他的类能重写这个函数
        Instance = this as T;

    }
}
