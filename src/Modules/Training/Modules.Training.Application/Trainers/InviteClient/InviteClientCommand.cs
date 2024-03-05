using Application.Messaging;

namespace Modules.Training.Application.Trainers.InviteClient;

public sealed record InviteClientCommand(Guid TrainerId, string Email) : ICommand<Guid>;