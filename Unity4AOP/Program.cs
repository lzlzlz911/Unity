namespace Unity4AOP{

    using System;

    #region Using
    #endregion

    internal class Program{

        private static void Main(string[] args){
            ITalk talk = ServiceLocator.Instance.GetService<ITalk>();
            talk.Speak("Hello");

            Console.WriteLine("pandora");

            Console.ReadLine();
        }

    }

}