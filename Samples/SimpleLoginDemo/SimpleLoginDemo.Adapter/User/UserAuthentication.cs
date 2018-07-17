// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Threading.Tasks;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Adapter.User
{
    public class UserAuthentication : IUserAuthentication
    {
        public async Task<UserAuthenticationResult> Authenticate(string userId, string password)
            => await Task.Run(() => userId == password ? UserAuthenticationResult.Succeeded() : UserAuthenticationResult.Failed());
    }
}
