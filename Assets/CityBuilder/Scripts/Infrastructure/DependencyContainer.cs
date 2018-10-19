using System;
using System.Collections.Generic;
using CityBuilder.Scripts.Application;
using UnityEditor;
using Object = System.Object;

namespace CityBuilder.Scripts.Infrastructure
{
    public class DependencyContainer
    {
        private static DependencyContainer _dependencyContainer = new DependencyContainer();
        private Dictionary<Type, Object> _instances = new Dictionary<Type, Object>(2);

        public UnityController GetUnityController()
        {
            return (UnityController) LoadInstanceOfType(
                typeof(UnityController),
                () => new UnityController(GetBuildUseCase())
            );
        }

        public BuildUseCase GetBuildUseCase()
        {
            return (BuildUseCase) LoadInstanceOfType(
                typeof(BuildUseCase),
                () => new BuildUseCase()
            );
        }

        private Object LoadInstanceOfType(Type t, Func<Object> instantiator)
        {
            Object instance;
            if (!_instances.TryGetValue(t, out instance))
            {
                instance = instantiator.Invoke();
                _instances.Add(t, instance);
            }

            return instance;
        }

        private DependencyContainer() {}

        public static DependencyContainer GetInstance()
        {
            return _dependencyContainer;
        }
    }
}