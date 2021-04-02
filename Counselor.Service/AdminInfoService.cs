using Counselor.IRepository;
using Counselor.IService;
using Counselor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counselor.Service
{
    public class AdminInfoService:BaseService<AdminInfo>,IAdminInfoService
    {
        private readonly IAdminInfoRepository _iAdminInfoRepository;
        public AdminInfoService(IAdminInfoRepository iAdminInfoRepository)
        {
            base._iBaseRepository = iAdminInfoRepository;
            _iAdminInfoRepository = iAdminInfoRepository;
        }
    }
}
