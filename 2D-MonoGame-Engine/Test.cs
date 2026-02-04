using System.Collections.Generic;
using System.Linq;
using _2D_MonoGame_Engine.Utilities;

namespace _2D_MonoGame_Engine;

public class Test
{ 
    List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };


    public void Foo()
    {
        numbers.ShuffleList();
        numbers.ShuffleList();
        
        string testString = "Hello World";
        testString.AddToString("!");
    }

    public bool GetNumber(int i)
    {
        return numbers.First(x => x == i) != null;
        numbers.Where(x => x > 0).First(x => x == i);
    }
}