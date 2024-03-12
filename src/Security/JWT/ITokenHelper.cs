﻿using Domain.Entities;

namespace Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IList<Role> roles, IList<OperationClaim> operationClaims);
}
