using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class NodesHotThreadsOverrides : EndpointOverridesBase
	{
		public override IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "type", "thread_type"}
		};
	}
}
