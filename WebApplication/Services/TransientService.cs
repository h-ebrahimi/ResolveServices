using System;
using ResolveServices.Attributes;

namespace WebApplication.Services
{
    [TransientService]
    public class TestTransientService : ITestTransientService
    {
        public string Do()
        {
            var name = this.GetType().Name;
            Console.WriteLine(this.GetType().Name);
            return $"Service : {name}";
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
            var name = this.GetType().Name;
            Console.WriteLine(this.GetType().Name);
            return $"Service : {name}";
        }
    }
}