using System;
using ResolveServices.Attributes;

namespace WebApplication.Services
{
    [ScopedService]
    public class TestScopedService : ITestScopedService
    {
        public string Do()
        {
            var name = this.GetType().Name;
            Console.WriteLine(this.GetType().Name);
            return $"Service : {name}";
        }
    }

    public interface ITestScopedService
    {
        string Do();
    }

    [ScopedService]
    public class TestScopedServiceWithoutInterface
    {
        public string Do()
        {
            var name = this.GetType().Name;
            Console.WriteLine(this.GetType().Name);
            return $"Service : {name}";
        }
    }
}