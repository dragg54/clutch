using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clutch_identity.Controllers
{
    [ApiController]
    [Authorize(Roles ="User")]
    [Route("/test")]
    public class TestController: ControllerBase
    {
        [HttpGet]
        public string GetTest(){
            return "Hello my dear";
        }
    }
}