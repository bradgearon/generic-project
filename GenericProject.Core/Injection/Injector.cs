using System;

namespace GenericProject.Core.Injection
{
    public class Injector
    {
        private static Injector _Instance;

        private readonly IInjectionProvider _InjectionProvider;

        protected Injector(IInjectionProvider injectionProvider)
        {
            if (injectionProvider == null)
                throw new ArgumentNullException("injectionProvider");

            _InjectionProvider = injectionProvider;
        }

        public static Injector Instance
        {
            get
            {
                lock (typeof (Injector))
                {
                    if (_Instance == null)
                        throw new InvalidOperationException("Injector has not been initialized.");

                    return _Instance;
                }
            }
        }

        public static void Initialize(IInjectionProvider injectionProvider) { _Instance = new Injector(injectionProvider); }

        public static object Get(Type type) { return Instance._InjectionProvider.Get(type); }

        public static T Get<T>() { return Instance._InjectionProvider.Get<T>(); }

        public static T Get<T>(string name) { return Instance._InjectionProvider.Get<T>(name); }

        public static T Get<T>(params ConstructorParameter[] constructionParameters) { return Instance._InjectionProvider.Get<T>(constructionParameters); }
    }
}