using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ITestScopedService _scopedService;
        private TestScopedServiceWithoutInterface _scopedServiceWithoutInterface;
        private ITestSingletonService _singletonService;
        private TestSingletonServiceWithoutInterface _singletonServiceWithoutInterface;
        private ITestTransientService _transientService;
        private TestTransientServiceWithoutInterface _transientServiceWithoutInterface;

        public WeatherForecastController(ITestScopedService scopedService, TestScopedServiceWithoutInterface scopedServiceWithoutInterface, ITestSingletonService singletonService, TestSingletonServiceWithoutInterface singletonServiceWithoutInterface, ITestTransientService transientService, TestTransientServiceWithoutInterface transientServiceWithoutInterface)
        {
            _scopedService = scopedService;
            _scopedServiceWithoutInterface = scopedServiceWithoutInterface;
            _singletonService = singletonService;
            _singletonServiceWithoutInterface = singletonServiceWithoutInterface;
            _transientService = transientService;
            _transientServiceWithoutInterface = transientServiceWithoutInterface;
        }

        [HttpGet]
        public IActionResult Get()
        {           
           
            var obj = new
            {
                Service1 = _singletonServiceWithoutInterface.Do(),
                Service2 = _singletonService.Do(),

                Service3 = _scopedServiceWithoutInterface.Do(),
                Service4 = _scopedService.Do(),

                Service5 = _transientServiceWithoutInterface.Do(),
                Service6 = _transientService.Do()
            };

            return Ok(obj);
        }
    }
}
