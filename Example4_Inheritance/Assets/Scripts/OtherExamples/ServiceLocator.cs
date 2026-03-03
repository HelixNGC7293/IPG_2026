using System.Collections.Generic;
using System;

public static class ServiceLocator
{
	// The registry holding all our active services
	private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

	/// Registers a service to the locator.
	public static void Register<T>(T service)
	{
		Type type = typeof(T);
		if (!_services.ContainsKey(type))
		{
			_services[type] = service;
		}
		else
		{
			// Optional: Handle overwriting or log a warning if a service already exists
			_services[type] = service;
		}
	}

	/// Unregisters a service (important for scene changes/cleanup).
	public static void Unregister<T>()
	{
		Type type = typeof(T);
		if (_services.ContainsKey(type))
		{
			_services.Remove(type);
		}
	}

	/// Retrieves a service from the locator.
	public static T Get<T>()
	{
		Type type = typeof(T);
		if (_services.TryGetValue(type, out object service))
		{
			return (T)service;
		}

		throw new Exception($"Service Locator Error: Service of type {type} is not registered.");
	}
}