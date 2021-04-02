using Counselor.IRepository;
using Counselor.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Counselor.Repository
{
    public class WorkInfoRepository : BaseRepository<WorkInfo>, IWorkInfoRepository
    {
        public async override Task<List<WorkInfo>> QueryAsync()
        {
            return await base.Context.Queryable<WorkInfo>()
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToListAsync();
        }
        public async override Task<List<WorkInfo>> QueryAsync(Expression<Func<WorkInfo, bool>> func)
        {
            return await base.Context.Queryable<WorkInfo>()
                .Where(func)
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToListAsync();
        }

        public async override Task<List<WorkInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<WorkInfo>()
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<WorkInfo>> QueryAsync(Expression<Func<WorkInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<WorkInfo>()
                .Where(func)
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToPageListAsync(page, size, total);
        }
    }
}
