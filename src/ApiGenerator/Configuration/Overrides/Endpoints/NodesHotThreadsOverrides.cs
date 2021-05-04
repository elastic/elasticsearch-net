// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class NodesHotThreadsOverrides : EndpointOverridesBase
	{
		public override IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "type", "thread_type" }
		};
	}
}
