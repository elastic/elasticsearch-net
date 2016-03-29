using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class SearchDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams => new []
		{
			"size", 
			"from",
			"timeout",
			"explain",
			"version",
			"q", //we dont support GET searches
			"fields",
			"indices_boost",
			"source",
			"sort",
			"_source",
			"_source_include",
			"_source_exclude",
			"track_scores",
			"terminate_after",
			"fielddata_fields"
		};

		public IDictionary<string, string> RenameQueryStringParams => null;
	}
}
