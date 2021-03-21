using BAYSOFT.Abstractions.Core.Application;
using BAYSOFT.Core.Domain.Entities.Default;
using BAYSOFT.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using System.Threading;
using System.Threading.Tasks;

namespace BAYSOFT.Core.Application.Default.Samples.Queries.GetSamplesByFilter
{
    public class GetSamplesByFilterQueryHandler : ApplicationRequestHandler<Sample, GetSamplesByFilterQuery, GetSamplesByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetSamplesByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public override async Task<GetSamplesByFilterQueryResponse> Handle(GetSamplesByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.Samples
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetSamplesByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
