using System;

public static class Program
{
    public static void Main(string[] args)
    {
        bool argLaunch = false;
        if (args.Length > 0)
        {
            for (int i = 0; i < args.Length; i++)
            {
                
                if (args[i] == "-editor")
                {
                    //Run the editor
                    using var editor = new _2D_MonoGame_Engine.Editor();
                    editor.Run();
                 
                    argLaunch = true;
                    //break for loop
                    break;
                }
                if (args[i] == "-cheater")
                {
                    Console.WriteLine("You are a dirty cheater >:C");
                    using var game = new _2D_MonoGame_Engine.MainGame();
                    game.Run();

                    argLaunch = true;
                    break;
                }
            }
            //if for whatever reason we get here, run the game normally
            if (argLaunch == false)
            {
                using var failSafe = new _2D_MonoGame_Engine.MainGame();
                failSafe.Run();
            }
        }
        else
        {
            //if there are NO arguements run the game normally
            using var game = new _2D_MonoGame_Engine.MainGame();
            game.Run();
        }
    }
}