using System.Linq;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Profiling.Async
{
	public class SearchAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;
		private Developer _developer;

		public SearchAsyncOperation(int iterations = 1000)
		{
			_iterations = iterations;
		}

		public override async Task SetupAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			_developer = Developer.Generator.Generate();

			var indexResponse = await client.IndexAsync(_developer, d => d.Index<Developer>().Refresh()).ConfigureAwait(false);

			if (!indexResponse.IsValid)
				output.WriteOrange($"error with id {indexResponse.Id}. message: {indexResponse.CallDetails.OriginalException}");
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (var i = 0; i < _iterations; i++)
			{
				// TODO generate random search descriptors
				var searchResponse = await client.SearchAsync<Developer>(s => s
					.Index<Developer>()
					.Query(q => q
						.Bool(b => b
							.Must(
								mc => mc.Match(m => m.Field(d => d.FirstName).Query(_developer.FirstName)),
								mc => mc.Match(m => m.Field(d => d.LastName).Query(_developer.LastName)),
								mc => mc.Match(m => m.Field(d => d.JobTitle).Query(_developer.JobTitle))
							)
						)
					)
					).ConfigureAwait(false);

				if (!searchResponse.IsValid)
					output.WriteOrange(
						$"error searching for {nameof(Developer)}. message: {searchResponse.CallDetails.OriginalException}");

				if (!searchResponse.Documents.Any())
					output.WriteOrange($"did not find matching {nameof(Developer)} for search.");
			}
		}

		public override async Task TeardownAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			var deleteResponse = await client.DeleteAsync<Developer>(_developer, d => d.Index<Developer>()).ConfigureAwait(false);

			if (!deleteResponse.IsValid)
				output.WriteOrange($"error with id {deleteResponse.Id}. message: {deleteResponse.CallDetails.OriginalException}");
		}
	}
}