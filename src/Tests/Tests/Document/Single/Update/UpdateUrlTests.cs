using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Single.Update
{
	public class UpdateUrlTests
	{
		[U] public async Task Urls()
		{
			var document = new Project { Name = "foo" };

			await POST($"/project/_update/foo")
					.Fluent(c => c.Update<Project>(document, u => u))
					.Request(c => c.Update(new UpdateRequest<Project, object>(document)))
					.FluentAsync(c => c.UpdateAsync<Project>(document, u => u))
					.RequestAsync(c => c.UpdateAsync(new UpdateRequest<Project, object>(document)))
				;

			var otherId = "other-id";

			await POST($"/project/_update/{otherId}")
					.Fluent(c => c.Update<Project>(otherId, u => u))
					.Request(c => c.Update(new UpdateRequest<Project, object>(typeof(Project), otherId)))
					.FluentAsync(c => c.UpdateAsync<Project>(otherId, u => u))
					.RequestAsync(c => c.UpdateAsync(new UpdateRequest<Project, object>(typeof(Project), otherId)))
				;

			var otherIndex = "other-index";

			await POST($"/{otherIndex}/_update/{otherId}")
					.Fluent(c => c.Update<Project>(otherId, u => u.Index(otherIndex)))
					.Request(c => c.Update(new UpdateRequest<Project, object>(otherIndex, otherId)))
					.FluentAsync(c => c.UpdateAsync<Project>(otherId, u => u.Index(otherIndex)))
					.RequestAsync(c => c.UpdateAsync(new UpdateRequest<Project, object>(otherIndex, otherId)))
				;
		}
	}
}
