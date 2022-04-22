using MediatR;
using NasaApi.Models.DTO;

namespace NasaApi.Library.Commands
{
    public record InsertNeosDatabaseCommand(List<NearEarthObjectDTO> Top3) : IRequest<List<NearEarthObjectDTO>>;
}
