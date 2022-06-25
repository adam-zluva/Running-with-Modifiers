using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Service Provider")]
public class ServiceProvider : ScriptableObject
{
    private Dictionary<Type, object> _services;
    private Dictionary<Type, object> services
    {
        get
        {
            if (_services == null) Init();
            return _services;
        }

        set
        {
            _services = value;
        }
    }

    public void Init()
    {
        Clear();
    }

    public void Clear()
    {
        services = new Dictionary<Type, object>();
    }

    public void AddService<T>(T service)
    {
        var type = service.GetType();
        if (!services.ContainsKey(type))
            services.Add(type, service);
        else Debug.LogWarning($"Service Provider {this} already contains the type {type}!");
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else Debug.LogWarning($"Service Provider {this} does not contain a service of the type {type}!");

        return default;
    }
}
