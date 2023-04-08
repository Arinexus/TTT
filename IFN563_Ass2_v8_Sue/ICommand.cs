using System;
using System.IO;
namespace BoardGame
{
    public interface ICommond
    {
        public void Execute();
    }

    public class MoveCommond : ICommond

    {

        public void Execute()
        {

        }
    }
    public class UndoCommond : ICommond

    {

        public void Execute()
        {

        }
    }
    public class RedoCommond : ICommond

    {

        public void Execute()
        {

        }
    }
    public class SaveCommond : ICommond

    {

        public void Execute()
        {

        }
    }
    public class LoadCommond : ICommond

    {

        public void Execute()
        {

        }
    }
}

