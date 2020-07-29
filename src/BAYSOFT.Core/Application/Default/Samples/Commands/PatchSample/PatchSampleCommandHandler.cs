using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using BAYSOFT.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BAYSOFT.Core.Application.Default.Samples.Commands.PatchSample
{
    public class PatchSampleCommandHandler : IRequestHandler<PatchSampleCommand, PatchSampleCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        public PatchSampleCommandHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<PatchSampleCommandResponse> Handle(PatchSampleCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.SampleID);

            var data = await Context.Samples.SingleOrDefaultAsync(x => x.SampleID == id);

            if (data == null)
            {
                throw new Exception("Sample not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchSampleCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
