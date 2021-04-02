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
    public class RoadInfoService:BaseService<RoadInfo>,IRoadInfoService
    {
        private readonly IRoadInfoRepository _iRoadInfoRepository;
        public RoadInfoService(IRoadInfoRepository iRoadInfoRepository)
        {
            base._iBaseRepository = iRoadInfoRepository;
            _iRoadInfoRepository = iRoadInfoRepository;
        }
    }
}
