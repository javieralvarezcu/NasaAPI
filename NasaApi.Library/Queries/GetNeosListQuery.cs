using MediatR;
using NasaApi.Models.DTO;

namespace NasaApi.Library.Queries
{
    public record GetNeosListQuery(int Days) : IRequest<List<NearEarthObjectDTO>>;
}
