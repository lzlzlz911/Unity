using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace Artech.UnityExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection unitySettings = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
            unitySettings.Configure(container);
            IFoo foo = container.Resolve<IFoo>();
            foo.DoSth();
        }
    }

    public interface IFoo
    {
        void DoSth();
    }

    public class Foo : IFoo
    {
        public Bar Bar { get; private set; }
        public Foo(Bar bar)
        {
            this.Bar = bar;
        }
        public virtual void DoSth()
        {
            this.Bar.DoSth();
        }
    }
    public class Bar : MarshalByRefObject
    {
        public Baz Baz { get; private set; }
        public Bar(Baz baz)
        {
            this.Baz = baz;
        }
        public virtual void DoSth()
        {
            this.Baz.DoSth();
        }
    }
    public class Baz : MarshalByRefObject
    {
        public void DoSth()
        {
            Console.WriteLine("Done...");
        }
    }

    public class SimpleCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("The CallHandler applied to \"{0}\" is invoked.", input.Target.GetType().Name);
            return getNext()(input, getNext);
        }
        public int Order { get; set; }
    }
    public class SimpleCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new SimpleCallHandler { Order = this.Order };
        }
    }


}
