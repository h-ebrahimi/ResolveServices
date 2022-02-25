using System;

namespace SadadPsp.SharedKernel.DependencyInjection
{
    [Serializable]
    public class ServiceConfigurationInformation
    {
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string AssemblyName { get; set; }
    }
}
