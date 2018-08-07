using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Suggesters
{
	public class SuggestUrlTests
	{
		[U]
		public async Task Urls()
		{
#pragma warning disable 618
			await POST("/project/_suggest")
					.Fluent(c => c.Suggest<Project>(s => s))
					.Request(c => c.Suggest<Project>(new SuggestRequest<Project>()))
					.FluentAsync(c => c.SuggestAsync<Project>(s => s))
					.RequestAsync(c => c.SuggestAsync<Project>(new SuggestRequest<Project>()))
				;

			await POST("/_suggest")
					.Fluent(c => c.Suggest<Project>(s => s.AllIndices()))
					.Request(c => c.Suggest<Project>(new SuggestRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.SuggestAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.SuggestAsync<Project>(new SuggestRequest<Project>(Nest.Indices.All)))
				;

			// Non-generic Object Initializer syntax
			await POST("/_suggest")
					.Request(c => c.Suggest<Project>(new SuggestRequest()))
					.RequestAsync(c => c.SuggestAsync<Project>(new SuggestRequest()))
				;
#pragma warning restore 618
		}
	}
}
