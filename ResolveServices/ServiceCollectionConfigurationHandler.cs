using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ResolveServices.Attributes;

namespace ResolveServices
{
    public static class ServiceCollectionConfigurationHandler
    {
        /// <summary>
        /// Adds the services(interface/class) with attribute.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void ResolveServicesWithAttribute(this IServiceCollection serviceCollection)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var searchPattern = $"*.dll";
            var assemblyPaths = Directory.GetFiles(currentPath, searchPattern).ToList();

            if (!assemblyPaths.Any())
                return;

            var loadedAssemblies = new List<Assembly>();
            foreach (var path in assemblyPaths)
            {
                try
                {
                    // Load Assembly
                    loadedAssemblies.Add(Assembly.LoadFile(path));
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            var implementationToBeRegistered = loadedAssemblies.SelectMany(assembly => assembly.GetTypes()).Where(exp => !exp.IsGenericType && exp.IsClass && (exp.IsDefined(typeof(SingletonServiceAttribute)) || exp.IsDefined(typeof(ScopedServiceAttribute)) || exp.IsDefined(typeof(TransientServiceAttribute)))).ToList();

            if (!implementationToBeRegistered.Any())
                return;

            foreach (var implementation in implementationToBeRegistered)
            {
                // Get Attribute of type 
                var customAttribute = implementation.GetCustomAttributes()?.Where(exp => exp is SingletonServiceAttribute || exp is ScopedServiceAttribute || exp is TransientServiceAttribute).FirstOrDefault() ?? null;

                if (customAttribute == null)
                    continue;

                // Detect type of Attribute
                var lifeTime = customAttribute switch
                {
                    SingletonServiceAttribute => ServiceLifetime.Singleton,
                    TransientServiceAttribute => ServiceLifetime.Transient,
                    _ => ServiceLifetime.Scoped
                };

                // Get interfaces of type
                var allInterfaces = implementation.GetInterfaces()?.Except(implementation.BaseType?.GetInterfaces());
                allInterfaces = allInterfaces?.Except(allInterfaces.SelectMany(exp => exp.GetInterfaces())).ToList();
                var isAlreadyRegistered = false;
                if (allInterfaces == null || !allInterfaces.Any())
                {
                    isAlreadyRegistered = serviceCollection.Any(s => s.ImplementationType == implementation);
                    if (isAlreadyRegistered)
                        continue;

                    // Register service
                    serviceCollection.Add(new ServiceDescriptor(implementation, implementation, lifeTime));
                }
                else
                {
                    foreach (var interfaceService in allInterfaces)
                    {
                        isAlreadyRegistered = serviceCollection.Any(s => s.ServiceType == interfaceService && s.ImplementationType == implementation);
                        if (isAlreadyRegistered)
                            continue;

                        // Register service
                        var descriptor = new ServiceDescriptor(interfaceService, implementation, lifeTime);                        
                        serviceCollection.Add(descriptor);
                    }
                }
            }
        }
    }
}
