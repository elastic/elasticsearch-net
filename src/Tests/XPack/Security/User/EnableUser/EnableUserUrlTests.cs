using Nest_5_2_0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.User.EnableUser
{
	class EnableUserUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/security/user/ironman/_enable")
				.Fluent(c => c.DisableUser("ironman"))
				.Request(c => c.DisableUser(new DisableUserRequest("ironman")))
				.FluentAsync(c => c.DisableUserAsync("ironman"))
				.RequestAsync(c => c.DisableUserAsync(new DisableUserRequest("ironman")))
				;
		}
	}
}
