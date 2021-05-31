using System;
using System.Collections.Generic;


public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services =
            new Dictionary<Type, object>();

    public static void AddService<T>(T value) where T : class
    {
        var typeValue = typeof(T);
        if (!_services.ContainsKey(typeValue))
        {
            _services[typeValue] = value;
        }
    }

    public static T GetService<T>()
    {
        var type = typeof(T);

        if (_services.ContainsKey(type))
        {
            return (T)_services[type];
        }

        return default;
    }

}
