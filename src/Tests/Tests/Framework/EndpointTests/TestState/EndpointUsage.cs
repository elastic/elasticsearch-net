using System;
using System.Collections.Concurrent;

namespace Tests.Framework.Integration
{
	public class EndpointUsage
	{
		private readonly object _lock = new object();
		private readonly ConcurrentDictionary<int, LazyResponses> _usages = new ConcurrentDictionary<int, LazyResponses>();

		public EndpointUsage() => CallUniqueValues = new CallUniqueValues();

		public bool CalledSetup { get; internal set; }
		public bool CalledTeardown { get; internal set; }

		public CallUniqueValues CallUniqueValues { get; }

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage, int k)
		{
			if (_usages.TryGetValue(k, out var r)) return r;

			lock (_lock)
			{
				if (_usages.TryGetValue(k, out r)) return r;

				var response = clientUsage();
				_usages.TryAdd(k, response);
				return response;
			}
		}
	}
}
