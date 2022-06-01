using ResolveServices.Attributes;
using System;

namespace WebApplication.Services
{
    [SingletonService]
    public class TestSingletonService : ITestSingletonService
    {
        public string Do()
        {
            return $"Service : TestSingletonService";
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
            return $"Service : TestSingletonServiceWithoutInterface";
        }
    }
}
