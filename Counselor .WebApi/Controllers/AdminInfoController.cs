using AutoMapper;
using Counselor.IService;
using Counselor.Model;
using Counselor.Model.DTO;
using Counselor_.WebApi.Utility._MD5;
using Counselor_.WebApi.Utility.ApiResult;
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
    public class AdminInfoController : ControllerBase
    {
        private readonly IAdminInfoService _iAdminInfoService;
        public AdminInfoController(IAdminInfoService iAdminInfoService)
        {
            _iAdminInfoService = iAdminInfoService;
        }
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name,string sex,string telnumber,string address,string units,string adminname,string adminpwd)
        {
            AdminInfo admin = new AdminInfo
            {
                Name = name,
                Sex = sex,
                TelNumber = telnumber,
                Address = address,
                Units = units,
                AdminName = adminname,
                AdminPwd = MD5Helper.MD5Encrypt32(adminpwd)  //MD5加密
            };
            //判断数据库中是否已经存在账号跟要添加的账号相同的数据
            var oldAdmin = await _iAdminInfoService.FindAsync(c => c.AdminName == adminname);
            if (oldAdmin != null) return ApiResultHelper.Error("账号已经存在");
            bool b = await _iAdminInfoService.CreateAsync(admin);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(admin);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name, string sex, string telnumber, string address, string units)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var admin = await _iAdminInfoService.FindAsync(id);
            admin.Name = name;
            admin.Sex = sex;
            admin.TelNumber = telnumber;
            admin.Address = address;
            admin.Units = units;
            bool b = await _iAdminInfoService.EditAsync(admin);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(admin);
        }

        [HttpGet("Find")]
        public async Task<ApiResult> Find([FromServices] IMapper iMapper)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var admin = await _iAdminInfoService.FindAsync(id);
            var adminDTO = iMapper.Map<AdminDTO>(admin);
            return ApiResultHelper.Success(adminDTO);
        }

        [HttpGet("FindAdmin")]
        public async Task<ApiResult> FindAdmin([FromServices] IMapper iMapper)
        {
            var admin = await _iAdminInfoService.QueryAsync();
            var adminDTO = iMapper.Map<AdminDTO>(admin);
            return ApiResultHelper.Success(adminDTO);
        }

        [HttpPut("EditAdmin")]
        public async Task<ApiResult> EditAdmin(int id,string name, string sex, string telnumber, string address, string units)
        {
            int findid = id;
            var admin = await _iAdminInfoService.FindAsync(findid);
            admin.Name = name;
            admin.Sex = sex;
            admin.TelNumber = telnumber;
            admin.Address = address;
            admin.Units = units;
            bool b = await _iAdminInfoService.EditAsync(admin);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(admin);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iAdminInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
    }
}
