using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public class LazyResponses : AsyncLazy<Dictionary<ClientMethod, IResponse>>
	{
		public static LazyResponses Empty { get; } = new LazyResponses(() => new Dictionary<ClientMethod, IResponse>());

		public LazyResponses(Func<Dictionary<ClientMethod, IResponse>> factory) : base(factory) { }

		public LazyResponses(Func<Task<Dictionary<ClientMethod, IResponse>>> factory) : base(factory) { }
	}
}