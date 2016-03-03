namespace Unity4AOP{
    #region Using
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity.InterceptionExtension;

    #endregion

    public class CachingBehavior : IInterceptionBehavior{

        public IEnumerable<Type> GetRequiredInterfaces() { return Type.EmptyTypes; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext){
            Console.WriteLine("CachingBehavior");
            return getNext().Invoke(input, getNext);
        }

        public bool WillExecute{
            get { return true; }
        }

    }

}