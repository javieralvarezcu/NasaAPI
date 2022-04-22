using MediatR;
using NasaApi.Library.Models;

namespace NasaApi.Library.Queries
{
    public record GetNeosListQuery(int Id) : IRequest<List<NearEarthObjectDTO>>;
}
