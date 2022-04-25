using MediatR;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Queries;
using NasaApi.Models.DTO;

namespace NasaApi.Library.Handlers
{
    public class GetNeosListHandler : IRequestHandler<GetNeosListQuery, List<NearEarthObjectDTO>>
    {
        private INearEarthObjectService _data;

        public GetNeosListHandler(INearEarthObjectService data)
        {
            _data = data;
        }

        public Task<List<NearEarthObjectDTO>> Handle(GetNeosListQuery request, CancellationToken cancellationToken)
        {
            return _data.GetAllNeosAsync(request.Days);
        }
    }
}
