using MediatR;
namespace NasaApi.Library.Queries
{
    public record GetTransactionsQuery(DateTimeOffset start_date, DateTimeOffset end_date) : IRequest<string>;
}
