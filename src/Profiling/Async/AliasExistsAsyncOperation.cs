using System;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Profiling.Async
{
	public class AliasExistsAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;
		private string _aliasName = "dev-alias";

		public AliasExistsAsyncOperation(int iterations = 1000)
		{
			if (iterations < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(iterations));
			}

			_iterations = iterations;
		}

		public override async Task SetupAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			var addResponse = await client.AliasAsync(a => a
				.Add(add => add
					.Alias(_aliasName)
					.Index<Developer>()
				)).ConfigureAwait(false);

			if (!addResponse.IsValid)
				output.WriteOrange($"Invalid response when adding alias for {nameof(AliasExistsAsyncOperation)} operation");
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (int i = 0; i < _iterations; i++)
			{
				var aliasResponse = await client.AliasExistsAsync(a => a
					.Index(typeof(Developer))
					.Name(i % 2 == 0 ? _aliasName : "non-existent-alias")
				).ConfigureAwait(false);

				if (!aliasResponse.IsValid)
					output.WriteOrange($"Invalid response from {nameof(AliasExistsAsyncOperation)} operation");
			}
		}

		public override async Task TeardownAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			var removeResponse = await client.AliasAsync(a => a
				.Remove(remove => remove
					.Alias(_aliasName)
					.Index<Developer>()
				)).ConfigureAwait(false);

			if (!removeResponse.IsValid)
				output.WriteOrange($"Invalid response when adding alias for {nameof(AliasExistsAsyncOperation)} operation");
		}
	}
}