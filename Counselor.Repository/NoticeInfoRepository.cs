using Counselor.IRepository;
using Counselor.Model;
using Counselor.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Counselor.Repository
{
    public class NoticeInfoRepository:BaseRepository<NoticeInfo>,INoticeInfoRepository
    {
        public async override Task<List<NoticeInfo>> QueryAsync()
        {
            return await base.Context.Queryable<NoticeInfo>()
                .Mapper(c => c.AdminInfo, c => c.AdminId, c => c.AdminInfo.Id)
                .ToListAsync();
        }
        public async override Task<List<NoticeInfo>> QueryAsync(Expression<Func<NoticeInfo, bool>> func)
        {
            return await base.Context.Queryable<NoticeInfo>()
                .Where(func)
                .Mapper(c => c.AdminInfo, c => c.AdminId, c => c.AdminInfo.Id)
                .ToListAsync();
        }

        public async override Task<List<NoticeInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<NoticeInfo>()
                .Mapper(c => c.AdminInfo, c => c.AdminId, c => c.AdminInfo.Id)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<NoticeInfo>> QueryAsync(Expression<Func<NoticeInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<NoticeInfo>()
                .Where(func)
                .Mapper(c => c.AdminInfo, c => c.AdminId, c => c.AdminInfo.Id)
                .ToPageListAsync(page, size, total);
        }
    }
}
