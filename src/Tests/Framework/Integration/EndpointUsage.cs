using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static Tests.Framework.Integration.ClientMethod;

namespace Tests.Framework.Integration
{
	public class EndpointUsage
	{
		private readonly object _lock = new object();
		private readonly ConcurrentDictionary<int, LazyResponses> _usages = new ConcurrentDictionary<int, LazyResponses>();

		public IDictionary<ClientMethod, string> CallUniqueValues { get; } 		
		private string UniqueValue => "nest-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		public EndpointUsage()
		{
			this.CallUniqueValues = new Dictionary<ClientMethod,string>
			{ 
				{ Fluent, this.UniqueValue },
				{ FluentAsync, this.UniqueValue },
				{ Initializer, this.UniqueValue },
				{ InitializerAsync, this.UniqueValue },
			};
		}

		public LazyResponses CallOnce(Func<LazyResponses> clientUsage, int? k = null)
		{
			var key = k ?? clientUsage.GetHashCode();
			LazyResponses r;
			if (_usages.TryGetValue(key, out r)) return r;
			lock (_lock)
			{
				if (_usages.TryGetValue(key, out r)) return r;
				var response = clientUsage();
				_usages.TryAdd(key, response);
				return response;
			}
		}
	}

	public enum ClientMethod
	{
		Fluent,
		FluentAsync,
		Initializer,
		InitializerAsync
	}
}