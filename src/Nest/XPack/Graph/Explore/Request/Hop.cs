using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IHop
	{
		[DataMember(Name ="connections")]
		IHop Connections { get; set; }

		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		[DataMember(Name ="vertices")]
		IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
	}

	public class Hop : IHop
	{
		public IHop Connections { get; set; }
		public QueryContainer Query { get; set; }
		public IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
	}

	public class HopDescriptor<T> : DescriptorBase<HopDescriptor<T>, IHop>, IHop
		where T : class
	{
		IHop IHop.Connections { get; set; }
		QueryContainer IHop.Query { get; set; }
		IEnumerable<IGraphVertexDefinition> IHop.Vertices { get; set; }

		public HopDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		public HopDescriptor<T> Vertices(Func<GraphVerticesDescriptor<T>, IPromise<IList<IGraphVertexDefinition>>> selector) =>
			Assign(a => a.Vertices = selector?.Invoke(new GraphVerticesDescriptor<T>())?.Value);

		public HopDescriptor<T> Connections(Func<HopDescriptor<T>, IHop> selector) =>
			Assign(a => a.Connections = selector?.Invoke(new HopDescriptor<T>()));
	}
}
