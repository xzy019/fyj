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
    public class CounselorInfoController : ControllerBase
    {
        private readonly ICounselorInfoService _iCounselorInfoService;
        public CounselorInfoController(ICounselorInfoService iCounselorInfoService)
        {
            _iCounselorInfoService = iCounselorInfoService;
        }
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name, string sex, string telnumber, string address, string units, string Counselorname, string Counselorpwd)
        {
            CounselorInfo Counselor = new CounselorInfo
            {
                Name = name,
                Sex = sex,
                TelNumber = telnumber,
                Address = address,
                Units = units,
                UserName = Counselorname,
                UserPwd = MD5Helper.MD5Encrypt32(Counselorpwd)  //MD5加密
            };
            //判断数据库中是否已经存在账号跟要添加的账号相同的数据
            var oldCounselor = await _iCounselorInfoService.FindAsync(c => c.UserName == Counselorname);
            if (oldCounselor != null) return ApiResultHelper.Error("账号已经存在");
            bool b = await _iCounselorInfoService.CreateAsync(Counselor);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(Counselor);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name, string sex, string telnumber, string address, string units)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var Counselor = await _iCounselorInfoService.FindAsync(id);
            Counselor.Name = name;
            Counselor.Sex = sex;
            Counselor.TelNumber = telnumber;
            Counselor.Address = address;
            Counselor.Units = units;
            bool b = await _iCounselorInfoService.EditAsync(Counselor);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(Counselor);
        }

        [HttpGet("Find")]
        public async Task<ApiResult> Find([FromServices] IMapper iMapper)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var Counselor = await _iCounselorInfoService.FindAsync(id);
            var counselorDTO = iMapper.Map<CounselorDTO>(Counselor);
            return ApiResultHelper.Success(counselorDTO);
        }

        [HttpGet("FindCounselor")]
        public async Task<ApiResult> FindCounselor([FromServices] IMapper iMapper)
        {
            var Counselor = await _iCounselorInfoService.QueryAsync();
            var counselorDTO = iMapper.Map<CounselorDTO>(Counselor);
            return ApiResultHelper.Success(counselorDTO);
        }

        [HttpPut("EditCounselor")]
        public async Task<ApiResult> EditCounselor(int id, string name, string sex, string telnumber, string address, string units)
        {
            int findId = id;
            var Counselor = await _iCounselorInfoService.FindAsync(findId);
            Counselor.Name = name;
            Counselor.Sex = sex;
            Counselor.TelNumber = telnumber;
            Counselor.Address = address;
            Counselor.Units = units;
            bool b = await _iCounselorInfoService.EditAsync(Counselor);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(Counselor);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iCounselorInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
    }
}
