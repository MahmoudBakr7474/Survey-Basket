﻿using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Contracts.Polla;

public record PollRequest(
    string Title,
    string Summary,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt
);

