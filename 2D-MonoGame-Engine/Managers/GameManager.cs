using System;
using _2D_MonoGame_Engine.Interfaces;
using _2D_MonoGame_Engine.Utilities;

namespace _2D_MonoGame_Engine.Managers;

public class GameManager : IServiceLocatable
{

    private int _score;
    public GameManager()
    {
        //Add the service to the locator
        ServiceLocator.Instance.AddService(this);
    }



    public void OnServiceLocated()
    {
        Console.WriteLine("GameManager located");    
    }

    public void OnServiceAdded()
    {
        Console.WriteLine("GameManager added to locator");
    }


    public void IncrementScore()
    {
        _score++;
        Console.WriteLine($"Score: {_score}");
    }
    public void OnServiceRemoved()
    {
        
    }
}