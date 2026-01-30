using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class ServiceLocator
    {
        public static Dictionary<Type, IService> _services = new();

        public static void Register<T>(T serive) where T : IService
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
            {
                throw new ArgumentException("Service already registered");
            }
            _services.Add(type, serive);
        }

        public static void UnRegister<T>() where T : IService
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                throw new ArgumentException("Service wasn't registered");
            }
            _services.Remove(type);
        }

        public static T Get<T>() where T : IService
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                throw new ArgumentException("Service wasn't registered");
            }
            return (T)_services[type];
        }

        public static void Clear()
        {
            _services.Clear();
        }
    }
}
