using System;
using System.Threading.Tasks;
using Bogus.DataSets;
using Nest;

namespace Profiling.Async
{
	public class AnalyzeAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;
		private readonly Lorem _sentenceGenerator = new Lorem();

		public AnalyzeAsyncOperation(int iterations = 1000)
		{
			if (iterations < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(iterations));
			}

			_iterations = iterations;
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (int i = 0; i < _iterations; i++)
			{
				var analyzeResponse = await client.AnalyzeAsync(a => a
					.Analyzer("standard")
					.Text(_sentenceGenerator.Sentence())
				).ConfigureAwait(false);

				if (!analyzeResponse.IsValid)
					output.WriteOrange($"Invalid response from {nameof(AnalyzeAsyncOperation)} operation");
			}
		}
	}
}