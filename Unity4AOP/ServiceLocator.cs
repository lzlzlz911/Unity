namespace Unity4AOP{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    #endregion

    public class ServiceLocator : IServiceProvider{
        #region Private Static Fields
        private static readonly ServiceLocator instance = new ServiceLocator();
        #endregion

        #region Private Fields
        private readonly IUnityContainer container;
        #endregion

        #region Ctor
        /// <summary>
        ///   Initializes a new instance of <c>ServiceLocator</c> class.
        /// </summary>
        private ServiceLocator(){

            container = new UnityContainer();

            container.AddNewExtension<Interception>();

            container.RegisterType<ITalk, Talk>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<ExceptionLoggingBehavior>(),
                new InterceptionBehavior<CachingBehavior>());
        }
        #endregion

        #region Public Static Properties
        /// <summary>
        ///   Gets the singleton instance of the <c>ServiceLocator</c> class.
        /// </summary>
        public static ServiceLocator Instance{
            get { return instance; }
        }
        #endregion

        #region IServiceProvider Members
        /// <summary>
        ///   Gets the service instance with the given type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The service instance.</returns>
        public object GetService(Type serviceType) { return container.Resolve(serviceType); }
        #endregion

        #region Private Methods
        private IEnumerable<ParameterOverride> GetParameterOverrides(object overridedArguments){
            List<ParameterOverride> overrides = new List<ParameterOverride>();
            Type argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(
                    property =>{
                        var propertyValue = property.GetValue(overridedArguments, null);
                        var propertyName = property.Name;
                        overrides.Add(new ParameterOverride(propertyName, propertyValue));
                    });
            return overrides;
        }
        #endregion

        #region Public Methods
        /// <summary>
        ///   Gets the service instance with the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service instance.</returns>
        public T GetService<T>() { return container.Resolve<T>(); }

        /// <summary>
        ///   Gets the service instance with the given type by using the overrided arguments.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="overridedArguments">The overrided arguments.</param>
        /// <returns>The service instance.</returns>
        public T GetService<T>(object overridedArguments){
            var overrides = GetParameterOverrides(overridedArguments);
            return container.Resolve<T>(overrides.ToArray());
        }

        /// <summary>
        ///   Gets the service instance with the given type by using the overrided arguments.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="overridedArguments">The overrided arguments.</param>
        /// <returns>The service instance.</returns>
        public object GetService(Type serviceType, object overridedArguments){
            var overrides = GetParameterOverrides(overridedArguments);
            return container.Resolve(serviceType, overrides.ToArray());
        }
        #endregion
    }

}