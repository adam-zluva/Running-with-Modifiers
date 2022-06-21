using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Service Provider")]
public class ServiceProvider : ScriptableObject
{
    private Dictionary<Type, object> services;

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
        else UnityEngine.Debug.LogWarning($"Service Provider {this} already contains the type {type}!");
    }

    public T GetService<T>(Type type)
    {
        if (services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else UnityEngine.Debug.LogWarning($"Service Provider {this} does not contain a service of the type {type}!");

        return default;
    }
}
