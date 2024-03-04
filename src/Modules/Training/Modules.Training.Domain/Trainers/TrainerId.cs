using Domain.Primitives;

namespace Modules.Training.Domain.Trainers;

public sealed record TrainerId(Guid Value) : IEntityId;