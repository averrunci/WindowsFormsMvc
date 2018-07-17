// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Threading.Tasks;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    public interface IUserAuthentication
    {
        Task<UserAuthenticationResult> Authenticate(string userId, string password);
    }
}
