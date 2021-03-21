using BAYSOFT.Abstractions.Core.Application;
using BAYSOFT.Core.Domain.Entities.Default;
using BAYSOFT.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BAYSOFT.Core.Application.Default.Samples.Queries.GetSampleById
{
    public class GetSampleByIdQueryHandler : ApplicationRequestHandler<Sample, GetSampleByIdQuery, GetSampleByIdQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetSampleByIdQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public override async Task<GetSampleByIdQueryResponse> Handle(GetSampleByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Samples
                .Where(x => x.Id == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Sample not found!");
            }

            return new GetSampleByIdQueryResponse(request, data, resultCount: 1);
        }
    }
}
