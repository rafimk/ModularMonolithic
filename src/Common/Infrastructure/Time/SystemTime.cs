using Application.ServiceLifetimes;
using Application.Time;

namespace Infrastructure.Time;

public sealed class SystemTime : ISystemTime, ITransient
{
    public DateTime UtcNow => DateTime.UtcNow;
}