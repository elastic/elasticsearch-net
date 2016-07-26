using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Reproduce
{
	public class GithubIssue2173
	{
		[I]
		public void UpdateByQueryWithInvalidScript()
		{
			var client = TestClient.GetClient();
			var response = client.UpdateByQuery<Project>(typeof(Project), typeof(Project), u => u
				.Script("invalid groovy")
			);
			response.IsValid.Should().BeFalse();
		}
	}
}
