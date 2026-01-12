using System;
using System.Collections.Generic;
using _2D_MonoGame_Engine.Interfaces;

namespace _2D_MonoGame_Engine.Utilities;

public class ServiceLocator : SingletonBehaviour<ServiceLocator>
{
    //string - key name
    //Object
    //int - specific id
    //Type
    private Dictionary<Type, IServiceLocatable> _services = new Dictionary<Type, IServiceLocatable>();
    
    public ServiceLocator()
    {
    }

    public bool AddService(IServiceLocatable service)
    {
        //If service already exists, return false
        if (_services.ContainsKey(service.GetType()))
        {

            Console.WriteLine($"Service {service.GetType()} already exists");
            return false;
        }

        //Add the service
        _services.Add(service.GetType(), service);
        service.OnServiceAdded();
        return true;
    }
    
    
    //Remove a service by type
    public bool RemoveService(Type serviceType)
    {
        if (_services.ContainsKey(serviceType))
        {
            _services[serviceType].OnServiceRemoved();
            return _services.Remove(serviceType);
        }
        return false;
    }

    //Remove a service by object
    public bool RemoveService(IServiceLocatable service)
    {
        return RemoveService(service.GetType());
    }



    public bool GetService<TService>(Type serviceType, out TService service)  where TService : IServiceLocatable
    {

        if (_services.ContainsKey(serviceType))
        {
            service = (TService)_services[serviceType];
            service.OnServiceLocated();
            return true;
        }


        //Just null.
        service = default(TService);
        return false;
    }


}