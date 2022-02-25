using Microsoft.Extensions.DependencyInjection;
using ResolveServices.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SadadPsp.SharedKernel.DependencyInjection
{
    public static class ServiceCollectionConfigurationHandler
    {
        /// <summary>
        /// Adds the services(interface/class) with attribute.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void AddServicesWithAttribute(this IServiceCollection serviceCollection)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string searchPattern = $"*.dll";
            var assemblyPaths = Directory.GetFiles(currentPath, searchPattern)?.ToList() ?? null;

            if (assemblyPaths == null || !assemblyPaths.Any())
                return;

            var loadedAssemblies = new List<Assembly>();
            foreach (string path in assemblyPaths)
            {
                try
                {
                    // Load Assembly
                    loadedAssemblies.Add(Assembly.LoadFile(path));
                }
                catch (Exception)
                {
                    continue;
                }
            }

            var implementationToBeRegistered = loadedAssemblies.SelectMany(assembly => assembly.GetTypes()).Where(exp => !exp.IsGenericType && exp.IsClass && (exp.IsDefined(typeof(SingletonServiceAttribute)) || exp.IsDefined(typeof(ScopedServiceAttribute)) || exp.IsDefined(typeof(TransientServiceAttribute)))).ToList();

            if (implementationToBeRegistered == null || !implementationToBeRegistered.Any())
                return;

            IEnumerable<Type> allInterfaces;
            Attribute customAttribute;
            ServiceLifetime lifeTime;
            bool isAlreadyRegistered;
            foreach (var implementation in implementationToBeRegistered)
            {
                // Get Attribute of type 
                customAttribute = implementation.GetCustomAttributes()?.Where(exp => exp is SingletonServiceAttribute || exp is ScopedServiceAttribute || exp is TransientServiceAttribute).FirstOrDefault() ?? null;

                if (customAttribute == null)
                    continue;

                // Detect type of Attribute
                lifeTime = ServiceLifetime.Scoped;
                if (customAttribute is SingletonServiceAttribute)
                    lifeTime = ServiceLifetime.Singleton;
                else if (customAttribute is TransientServiceAttribute)
                    lifeTime = ServiceLifetime.Transient;

                // Get interfaces of type
                allInterfaces = implementation.GetInterfaces()?.Except(implementation.BaseType?.GetInterfaces());
                allInterfaces = allInterfaces?.Except(allInterfaces.SelectMany(exp => exp.GetInterfaces())).ToList();
                isAlreadyRegistered = false;
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
                        serviceCollection.Add(new ServiceDescriptor(interfaceService, implementation, lifeTime));
                    }
                }
            }
        }
    }
}
