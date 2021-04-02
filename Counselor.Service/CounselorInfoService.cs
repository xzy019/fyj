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
    public class CounselorInfoService:BaseService<CounselorInfo>,ICounselorInfoService
    {
        private readonly ICounselorInfoRepository _iCounselorInfoRepository;
        public CounselorInfoService(ICounselorInfoRepository iCounselorInfoRepository)
        {
            base._iBaseRepository = iCounselorInfoRepository;
            _iCounselorInfoRepository = iCounselorInfoRepository;
        }
    }
}
