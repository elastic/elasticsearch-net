using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public class LazyResponses : AsyncLazy<Dictionary<ClientMethod, IResponse>>
	{
		public LazyResponses(Func<Dictionary<ClientMethod, IResponse>> factory) : this("__ignored__", factory) {}

		public LazyResponses(Func<Task<Dictionary<ClientMethod, IResponse>>> factory) : this("__ignored__", factory) {}

		public LazyResponses(string name, Func<Dictionary<ClientMethod, IResponse>> factory) : base(factory) => Name = name;

		public LazyResponses(string name, Func<Task<Dictionary<ClientMethod, IResponse>>> factory) : base(factory) => Name = name;

		public static LazyResponses Empty { get; } = new LazyResponses("__empty__", () => new Dictionary<ClientMethod, IResponse>());

		public string Name { get; }
	}
}
