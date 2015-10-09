using System;
using System.Collections.Concurrent;

namespace Tests.Framework.Integration
{
	public class EndpointUsage
	{
		private readonly object _lock = new object();
		private readonly ConcurrentDictionary<int, LazyResponses> _usages = new ConcurrentDictionary<int, LazyResponses>();

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage, int? k = null)
		{
			var key = k ?? clientUsage.GetHashCode();
			LazyResponses r = null;
			if (_usages.TryGetValue(key, out r)) return r;

			lock (_lock)
			{
				LazyResponses lr = null;
				if (_usages.TryGetValue(key, out lr)) return lr;
				var response = clientUsage();
				_usages.TryAdd(key, response);
				return response;
			}
		}
	}
}