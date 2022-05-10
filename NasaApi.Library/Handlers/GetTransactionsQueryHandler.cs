using MediatR;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Queries;

namespace NasaApi.Library.Handlers
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, string>
    {
        private IPaypalService _data;

        public GetTransactionsQueryHandler(IPaypalService data)
        {
            _data = data;
        }

        public Task<string> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return _data.GetTransactionsByDate(request.start_date, request.end_date);
        }
    }
}
