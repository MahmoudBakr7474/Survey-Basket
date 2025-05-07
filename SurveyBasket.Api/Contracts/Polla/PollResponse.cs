using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Contracts.Polla;

public record PollResponse(
    int Id,
    string Title,
    string Summary,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt
);
