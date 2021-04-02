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
    public class NoticeInfoService:BaseService<NoticeInfo>,INoticeInfoService
    {
        private readonly INoticeInfoRepository _iNoticeInfoRepository;
        public NoticeInfoService(INoticeInfoRepository iNoticeInfoRepository)
        {
            base._iBaseRepository = iNoticeInfoRepository;
            _iNoticeInfoRepository = iNoticeInfoRepository;
        }
    }
}
