using MediatR;
using Microsoft.EntityFrameworkCore;
using BAYSOFT.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BAYSOFT.Core.Application.Default.Samples.Commands.DeleteSample
{
    public class DeleteSampleCommandHandler : IRequestHandler<DeleteSampleCommand, DeleteSampleCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        public DeleteSampleCommandHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<DeleteSampleCommandResponse> Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.SampleID);

            var data = await Context.Samples.SingleOrDefaultAsync(x => x.SampleID == id);

            if (data == null)
                throw new Exception("Sample not found!");

            Context.Samples.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteSampleCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
