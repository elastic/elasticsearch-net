using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.ChangePassword
{
	public class DeleteUserUrlTests : IUrlTests
	{
		[U]
		public async Task Urls()
		{
			await PUT("/_xpack/security/user/forloop/_password")
				.Fluent(c => c.ChangePassword(p => p.Username("forloop").Password("password")))
				.Request(c => c.ChangePassword(new ChangePasswordRequest("forloop") { Password = "password" }))
				.FluentAsync(c => c.ChangePasswordAsync(p => p.Username("forloop").Password("password")))
				.RequestAsync(c => c.ChangePasswordAsync(new ChangePasswordRequest("forloop") { Password = "password" }))
				;

			await PUT("/_xpack/security/user/_password")
				.Fluent(c => c.ChangePassword(p => p.Password("password")))
				.Request(c => c.ChangePassword(new ChangePasswordRequest() { Password = "password" }))
				.FluentAsync(c => c.ChangePasswordAsync(p => p.Password("password")))
				.RequestAsync(c => c.ChangePasswordAsync(new ChangePasswordRequest() { Password = "password" }))
				;
		}
	}
}
