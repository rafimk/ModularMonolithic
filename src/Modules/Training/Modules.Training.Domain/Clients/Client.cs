using Domain.Primitives;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Training.Domain.Clients;

public sealed class Client : Entity<ClientId>, IAuditable
{

    public const string Role = "Client";
    private Client(ClientId id, TrainerId trainerId, string email, string firstName, string lastName)
        : base(id)
    {
        TrainerId = trainerId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public TrainerId TrainerId { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public void Change(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    internal static Result<Client> Create(ClientId id, TrainerId trainerId, string email, string firstName, string lastName)
    {
        var client = new Client(id, trainerId, email, firstName, lastName);

        return client;
    }
}
