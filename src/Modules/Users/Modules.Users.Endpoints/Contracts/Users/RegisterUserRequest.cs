﻿namespace Modules.Users.Endpoints.Contracts.Users;

public sealed record RegisterUserRequest(string Email, string FirstName, string LastName);