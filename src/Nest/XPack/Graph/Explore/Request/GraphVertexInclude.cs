using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class GraphVertexInclude
	{
		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("term")]
		public string Term { get; set; }
	}

	public class GraphVertexIncludeDescriptor : DescriptorPromiseBase<GraphVertexIncludeDescriptor, List<GraphVertexInclude>>
	{
		public GraphVertexIncludeDescriptor() : base(new List<GraphVertexInclude>()) { }

		public GraphVertexIncludeDescriptor Include(string term, double? boost = null) =>
			Assign(new GraphVertexInclude { Term = term, Boost = boost }, (a, v) => a.Add(v));

		public GraphVertexIncludeDescriptor IncludeRange(params string[] terms) =>
			Assign(terms.Select(t => new GraphVertexInclude { Term = t }), (a, v) => a.AddRange(v));

		public GraphVertexIncludeDescriptor IncludeRange(IEnumerable<string> terms) =>
			Assign(terms.Select(t => new GraphVertexInclude { Term = t }), (a, v) => a.AddRange(v));

		public GraphVertexIncludeDescriptor IncludeRange(IEnumerable<GraphVertexInclude> includes) =>
			Assign(includes, (a, v) => a.AddRange(v));
	}
}
