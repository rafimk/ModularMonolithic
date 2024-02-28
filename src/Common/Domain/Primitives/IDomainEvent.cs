﻿using MediatR;

namespace Domain.Primitives;
public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}