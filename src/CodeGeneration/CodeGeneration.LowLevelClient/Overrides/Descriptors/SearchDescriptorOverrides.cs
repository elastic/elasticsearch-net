using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	/// <summary>
	/// Tweaks the generated descriptors
	/// </summary>
	public interface IDescriptorOverrides
	{
		/// <summary>
		/// Sometimes params can be defined on the body as well as on the querystring
		/// We favor specifying params on the body so here we can specify params we don't want on the querystring.
		/// </summary>
		IEnumerable<string> SkipQueryStringParams { get; }
	}


	public class SearchDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
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
					"_source_exclude"
				};
			}
		}
	}
}
