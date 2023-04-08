using System;
using System.IO;
namespace BoardGame
{
    public interface ICommand
    {
        public void Execute();
    }

    public class MoveCommond : ICommand

    {

        public void Execute()
        {

        }
    }
    public class UndoCommond : ICommand

    {

        public void Execute()
        {

        }
    }
    public class RedoCommond : ICommand

    {

        public void Execute()
        {

        }
    }
    public class SaveCommond : ICommand

    {

        public void Execute()
        {

        }
    }
    public class LoadCommond : ICommand

    {

        public void Execute()
        {

        }
    }
    public class QuitGame : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Thank you for playing");
            Environment.Exit(0);
        }
    }
}

