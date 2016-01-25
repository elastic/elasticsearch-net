using System;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Profiling.Async
{
	public class AliasAsyncOperation : AsyncProfiledOperationBase
	{
		private readonly int _iterations;

		public AliasAsyncOperation(int iterations = 1000)
		{
			if (_iterations < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(iterations));
			}

			_iterations = iterations;
		}

		public override async Task ProfileAsync(IElasticClient client, ColoredConsoleWriter output)
		{
			for (int i = 0; i < _iterations; i++)
			{
				IBulkAliasResponse aliasResponse;
				if (i == 0)
				{
					aliasResponse = await client.AliasAsync(a => a
						.Add(add => add
							.Alias($"dev{i}").Index<Developer>()
						)).ConfigureAwait(false);
				}
				else if (i == _iterations - 1)
				{
					aliasResponse = await client.AliasAsync(a => a
						.Remove(remove => remove
							.Alias($"dev{i - 1}")
							.Index(typeof (Developer))
						)).ConfigureAwait(false);
				}
				else
				{
					aliasResponse = await client.AliasAsync(a => a
						.Add(add => add
							.Alias($"dev{i}").Index<Developer>()
						)
						.Remove(remove => remove
							.Alias($"dev{i - 1}")
							.Index(typeof (Developer))
						)).ConfigureAwait(false);
				}

				if (!aliasResponse.IsValid)
					output.WriteOrange($"Invalid response from {nameof(AliasAsyncOperation)} operation");
			}
		}
	}
}