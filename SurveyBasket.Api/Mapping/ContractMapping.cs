using SurveyBasket.Contracts.Requests;
using SurveyBasket.Contracts.Responses;

namespace SurveyBasket.Mapping;

public static class ContractMapping
{
    public static PollResponse MapToResponse(this Poll poll)
    {
        return new()
        {
            Id = poll.Id,
            Title = poll.Title,
            Description = poll.Description,

        };
    }
    public static IEnumerable<PollResponse>  MapToResponse(this IEnumerable<Poll> polls)
    {
        return polls.Select(MapToResponse);
    }
    public static Poll MapToPoll(this CreatePollRequst requst)
    {
        return new()
        {
            Title = requst.Title,
            Description = requst.Description,

        };
    }
  
}
