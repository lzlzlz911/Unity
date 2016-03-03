namespace Unity4AOP{

    using System;

    #region Using
    #endregion

    internal class Program{

        private static void Main(string[] args){
            ITalk talk = ServiceLocator.Instance.GetService<ITalk>();
            talk.Speak("Hello");
            talk.Speak2(new User(){Name = "username", mad = new Address{Name = "useraddress"}});

            Console.WriteLine("pandora");

            Console.ReadLine();
        }

    }

}