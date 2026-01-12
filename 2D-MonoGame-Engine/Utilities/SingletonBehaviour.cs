using System;

namespace _2D_MonoGame_Engine.Utilities;

public class SingletonBehaviour<T> where T : SingletonBehaviour<T>, new()
{


    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            return _instance = new T();
        }
    }
    
    public void OnIntialize()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            throw new Exception("Singleton instance already initialized");
        }
    }
}