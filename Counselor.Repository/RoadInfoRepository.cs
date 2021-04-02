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
    public class RoadInfoRepository:BaseRepository<RoadInfo>,IRoadInfoRepository
    {
        public async override Task<List<RoadInfo>> QueryAsync()
        {
            return await base.Context.Queryable<RoadInfo>()
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToListAsync();
        }
        public async override Task<List<RoadInfo>> QueryAsync(Expression<Func<RoadInfo, bool>> func)
        {
            return await base.Context.Queryable<RoadInfo>()
                .Where(func)
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToListAsync();
        }

        public async override Task<List<RoadInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<RoadInfo>()
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<RoadInfo>> QueryAsync(Expression<Func<RoadInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<RoadInfo>()
                .Where(func)
                .Mapper(c => c.CounselorInfo, c => c.CounselorId, c => c.CounselorInfo.Id)
                .ToPageListAsync(page, size, total);
        }
    }
}
