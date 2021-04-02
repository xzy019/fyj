using Counselor.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counselor_.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkInfoController : ControllerBase
    {
        private readonly IWorkInfoService _iWorkInfoService;
        public WorkInfoController(IWorkInfoService iWorkInfoService)
        {
            _iWorkInfoService = iWorkInfoService;
        }
    }
}
