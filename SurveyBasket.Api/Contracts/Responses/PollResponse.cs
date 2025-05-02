using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Contracts.Responses;

public record PollResponse(
    int Id,
    string Title,
    string Notes
    );
