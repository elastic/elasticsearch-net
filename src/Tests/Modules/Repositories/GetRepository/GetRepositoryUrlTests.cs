using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.GetRepository.GetRepository
{
	public class GetRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await GET($"/_snapshot/{repository}")
				.Fluent(c => c.GetRepository(repository))
				.Request(c => c.GetRepository(new GetRepositoryRequest(repository)))
				.FluentAsync(c => c.GetRepositoryAsync(repository))
				.RequestAsync(c => c.GetRepositoryAsync(new GetRepositoryRequest(repository)))
				;
		}
	}
}
