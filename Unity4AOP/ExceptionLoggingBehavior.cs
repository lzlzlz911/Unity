namespace Unity4AOP{
    #region Using
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity.InterceptionExtension;

    #endregion

    public class ExceptionLoggingBehavior : IInterceptionBehavior{

        public IEnumerable<Type> GetRequiredInterfaces() { return Type.EmptyTypes; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext){
            Console.WriteLine("ExceptionLoggingBehavior");

            var retvalue = getNext().Invoke(input, getNext);

            if (null == retvalue.Exception)
            {
                
            }
            else
            {
                Console.WriteLine(retvalue.Exception.ToString());
                retvalue.Exception = null;
            }

            return retvalue;
        }

        public bool WillExecute{
            get { return true; }
        }

    }

}