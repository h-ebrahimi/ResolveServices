using System;
using ResolveServices.Attributes;

namespace WebApplication.Services
{
    [TransientService]
    public class TestTransientService : ITestTransientService
    {
        public string Do()
        {
            return $"Service : TestTransientService";
        }
    }

    public interface ITestTransientService
    {
        string Do();
    }

    [TransientService]
    public class TestTransientServiceWithoutInterface
    {
        public string Do()
        {
            return $"Service : TestTransientServiceWithoutInterface";
        }
    }
}