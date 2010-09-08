﻿using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IAuthenticationContext
    {
        void Authenticate(IPrincipal principal);
    }
}