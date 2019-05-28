using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("graph.explore.json")]
	public partial interface IGraphExploreRequest : IHop
	{
		[DataMember(Name ="controls")]
		IGraphExploreControls Controls { get; set; }
	}

	public partial interface IGraphExploreRequest<T> where T : class { }

	public partial class GraphExploreRequest
	{
		public IHop Connections { get; set; }
		public IGraphExploreControls Controls { get; set; }
		public QueryContainer Query { get; set; }
		public IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
	}

	public partial class GraphExploreRequest<T> where T : class { }

	public partial class GraphExploreDescriptor<T> where T : class
	{
		IHop IHop.Connections { get; set; }
		IGraphExploreControls IGraphExploreRequest.Controls { get; set; }
		QueryContainer IHop.Query { get; set; }
		IEnumerable<IGraphVertexDefinition> IHop.Vertices { get; set; }

		public GraphExploreDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public GraphExploreDescriptor<T> Vertices(Func<GraphVerticesDescriptor<T>, IPromise<IList<IGraphVertexDefinition>>> selector) =>
			Assign(selector, (a, v) => a.Vertices = v?.Invoke(new GraphVerticesDescriptor<T>())?.Value);

		public GraphExploreDescriptor<T> Connections(Func<HopDescriptor<T>, IHop> selector) =>
			Assign(selector, (a, v) => a.Connections = v?.Invoke(new HopDescriptor<T>()));

		public GraphExploreDescriptor<T> Controls(Func<GraphExploreControlsDescriptor<T>, IGraphExploreControls> selector) =>
			Assign(selector, (a, v) => a.Controls = v?.Invoke(new GraphExploreControlsDescriptor<T>()));
	}
}
