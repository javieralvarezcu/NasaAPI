using MediatR;
using NasaApi.Library.Commands;
using NasaApi.Models.DTO;
using NasaApi.Persistence.Context;

namespace NasaApi.Library.Handlers
{
    public class InsertNeosDatabaseCommandHandler : IRequestHandler<InsertNeosDatabaseCommand, List<NearEarthObjectDTO>>
    {
        private readonly MyDbContext _context;

        public InsertNeosDatabaseCommandHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<NearEarthObjectDTO>> Handle(InsertNeosDatabaseCommand request, CancellationToken cancellationToken)
        {
            var top3 = request.Top3;
            try
            {
                foreach (var neo in top3)
                {
                    if (!_context.near_earth_objects.Contains(neo))
                    {
                        _context.near_earth_objects.Add(neo);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch(Exception e) { }
            return await Task.FromResult(top3);
        }
    }
}
