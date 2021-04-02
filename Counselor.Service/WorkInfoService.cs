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
    public class WorkInfoService:BaseService<WorkInfo>,IWorkInfoService
    {
        private readonly IWorkInfoRepository _iWorkInfoRepository;
        public WorkInfoService(IWorkInfoRepository iWorkInfoRepository)
        {
            base._iBaseRepository = iWorkInfoRepository;
            _iWorkInfoRepository = iWorkInfoRepository;
        }
    }
}
