using System;
using ResolveServices.Attributes;

namespace WebApplication.Services
{
    [ScopedService]
    public class TestScopedService : ITestScopedService
    {
        public string Do()
        {
            return $"Service : TestScopedService";
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
            return $"Service : TestScopedServiceWithoutInterface";
        }
    }
}