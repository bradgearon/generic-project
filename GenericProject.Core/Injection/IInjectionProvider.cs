using System;

namespace GenericProject.Core.Injection
{
    public interface IInjectionProvider
    {
        T Get<T>();

        T Get<T>(string name);

        T Get<T>(params ConstructorParameter[] constructorParameters);

        object Get(Type type);
    }
}