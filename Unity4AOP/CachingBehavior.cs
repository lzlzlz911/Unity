namespace Unity4AOP{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Practices.Unity.InterceptionExtension;

    #endregion

    public class CachingBehavior : IInterceptionBehavior{

        public IEnumerable<Type> GetRequiredInterfaces() { return Type.EmptyTypes; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext){
            Console.WriteLine("CachingBehavior");

            var methodinput = (Microsoft.Practices.Unity.InterceptionExtension.VirtualMethodInvocation) input;

            Console.WriteLine(input.Target.ToString());
            Console.WriteLine(input.MethodBase.Name);

            var xx = input.Inputs.Cast<object>().ToArray()[0].ToString();

            var retvalue = getNext().Invoke(input, getNext);

            return retvalue;
        }

        public bool WillExecute{
            get { return true; }
        }

    }

}