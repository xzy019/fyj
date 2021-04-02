using AutoMapper;
using Counselor.IService;
using Counselor.Model;
using Counselor.Model.DTO;
using Counselor_.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Counselor_.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NoticeInfoController : ControllerBase
    {
        private readonly INoticeInfoService _iNoticeInfoService;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public NoticeInfoController(INoticeInfoService iNoticeInfoService, IWebHostEnvironment webHostEnvironment)
        {
            this._iNoticeInfoService = iNoticeInfoService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("NoticeInfo")]
        public async Task<ApiResult> GetNoticeInfo()
        {
            //int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await _iNoticeInfoService.QueryAsync();
            if (data.Count == 0) return ApiResultHelper.Error("没有更多数据");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create([FromForm(Name ="title")] string title, [FromForm(Name = "category")] string category, [FromForm(Name = "file")] IFormFile file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var fileFolder = Path.Combine(dirPath, newFileName);
            using (var stream = new FileStream(fileFolder, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string url = $@"\{uploadPath}\{newFileName}";
                
            NoticeInfo noticeinfo = new NoticeInfo
            {
                Title = title,
                Time = DateTime.Now,
                Category = category,
                Path = "localhost:5000"+url,
                AdminId = 1 /*Convert.ToInt32(this.User.FindFirst("Id").Value)*/
            };
            bool b = await _iNoticeInfoService.CreateAsync(noticeinfo);
            if (!b) return ApiResultHelper.Error("添加失败，服务器发生错误");
            return ApiResultHelper.Success(noticeinfo);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iNoticeInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string title, string category, IFormFile file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var fileFolder = Path.Combine(dirPath, newFileName);
            using (var stream = new FileStream(fileFolder, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string url = $@"\{uploadPath}\{newFileName}";

            var noticeinfo = await _iNoticeInfoService.FindAsync(id);
            if (noticeinfo == null) return ApiResultHelper.Error("没有找到该通告");
            noticeinfo.Title = title;
            noticeinfo.Category = category;
            noticeinfo.Path = "localhost:5000"+url;
            bool b = await _iNoticeInfoService.EditAsync(noticeinfo);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(noticeinfo);
        }

        [HttpGet("NoticeInfoPage")]
        public async Task<ApiResult> GetNoticeInfoPage([FromServices] IMapper iMapper, int page, int size)
        {
            RefAsync<int> total = 0;
            var noticeinfo = await _iNoticeInfoService.QueryAsync(page, size, total);
            try
            {
                var data = await _iBlogNewsService.QueryAsync(c => c.WriterId == id);
                var noticeDTO = iMapper.Map<List<NoticeDTO>>(noticeinfo);
                return ApiResultHelper.Success(noticeDTO, total);
            }
            catch (Exception)
            {
                return ApiResultHelper.Error("AutoMapper映射错误");
            }
        }

        [HttpPost("Photos")]
        public async Task<IActionResult> UploadPhotosAsync([FromForm(Name = "file")]IFormFile file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if(file == null)
            {
                return Ok("错误");
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var fileFolder = Path.Combine(dirPath, newFileName);
            using (var stream = new FileStream(fileFolder, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string url = $@"\{uploadPath}\{newFileName}";

            return Ok(url);
        }
    }
}
