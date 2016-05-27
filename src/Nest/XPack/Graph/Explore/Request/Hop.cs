using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IHop
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("vertices")]
		IEnumerable<IGraphVertexDefinition> Vertices { get; set; }

		[JsonProperty("connections")]
		IHop Connections { get; set; }
	}

	public class Hop : IHop
	{
		public QueryContainer Query { get; set; }
		public IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
		public IHop Connections { get; set; }
	}

	public class HopDescriptor<T> : DescriptorBase<HopDescriptor<T>, IHop>, IHop
		where T : class
	{
		QueryContainer IHop.Query { get; set; }
		IEnumerable<IGraphVertexDefinition> IHop.Vertices { get; set; }
		IHop IHop.Connections { get; set; }

		public HopDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		public HopDescriptor<T> Vertices(Func<GraphVerticesDescriptor<T>, IPromise<IList<IGraphVertexDefinition>>> selector) =>
			Assign(a => a.Vertices = selector?.Invoke(new GraphVerticesDescriptor<T>())?.Value);

		public HopDescriptor<T> Connections(Func<HopDescriptor<T>, IHop> selector) =>
			Assign(a => a.Connections = selector?.Invoke(new HopDescriptor<T>()));

	}

}
