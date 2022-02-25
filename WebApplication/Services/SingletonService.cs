using ResolveServices.Attributes;
using System;

namespace WebApplication.Services
{
    [SingletonService]
    public class TestSingletonService : ITestSingletonService
    {
        public string Do()
        {
            var name = this.GetType().Name;
            Console.WriteLine(this.GetType().Name);
            return $"Service : {name}";
        }
    }

    public interface ITestSingletonService
    {
        string Do();
    }

    [SingletonService]
    public class TestSingletonServiceWithoutInterface
    {
        public string Do()
        {
            var name = this.GetType().Name;
            Console.WriteLine(this.GetType().Name);
            return $"Service : {name}";
        }
    }
}
