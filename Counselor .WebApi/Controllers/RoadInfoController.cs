using AutoMapper;
using Counselor.IService;
using Counselor.Model;
using Counselor.Model.DTO;
using Counselor_.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counselor_.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadInfoController : ControllerBase
    {
        private readonly IRoadInfoService _iRoadInfoService;
        public RoadInfoController(IRoadInfoService iRoadInfoService)
        {
            _iRoadInfoService = iRoadInfoService;
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(string location, string state)
        {
            RoadInfo road = new RoadInfo
            {
                Location = location,
                ReportTime = DateTime.Now,
                State = state,
                CounselorId = Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool b = await _iRoadInfoService.CreateAsync(road);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(road);
        }

        [HttpDelete("Delete")]
        public async Task<ApiResult> Detele(int id)
        {
            bool b = await _iRoadInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        [HttpGet("Find")]
        public async Task<ApiResult> Find()
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await _iRoadInfoService.QueryAsync(c => c.CounselorId == id);
            if (data.Count == 0) return ApiResultHelper.Error("没有更多的数据");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("FindAll")]
        public async Task<ApiResult> FindAll([FromServices] IMapper iMapper, int page, int size)
        {
            RefAsync<int> total = 0;
            var road = await _iRoadInfoService.QueryAsync(page, size, total);
            try
            {
                var roadDTO = iMapper.Map<List<RoadDTO>>(road);
                return ApiResultHelper.Success(roadDTO, total);
            }
            catch (Exception)
            {
                return ApiResultHelper.Error("AutoMapper映射错误");
            }
        }

        [HttpPost("CounselorEdit")]
        public async Task<ApiResult> CounselorEdit(int id,string location,string state)
        {
            var road = await _iRoadInfoService.FindAsync(id);
            if (road == null) return ApiResultHelper.Error("没有找到该条数据");
            if(road.CounselorId != Convert.ToInt32(this.User.FindFirst("Id").Value))
            {
                return ApiResultHelper.Error("你没有权限修改该条数据");
            }
            road.Location = location;
            road.State = state;
            bool b = await _iRoadInfoService.EditAsync(road);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(road);
        }

        [HttpPost("AdminEdit")]
        public async Task<ApiResult> AdminEdit(int id, string location, string state)
        {
            var road = await _iRoadInfoService.FindAsync(id);
            if (road == null) return ApiResultHelper.Error("没有找到该条数据");
            road.Location = location;
            road.State = state;
            bool b = await _iRoadInfoService.EditAsync(road);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(road);
        }
    }
}
