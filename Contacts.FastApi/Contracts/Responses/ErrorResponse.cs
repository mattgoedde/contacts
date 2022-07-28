namespace Contacts.FastApi.Contracts.Responses;

public class ErrorResponse : Response
{
    public IEnumerable<string> Errors { get; init; } = default!;
}