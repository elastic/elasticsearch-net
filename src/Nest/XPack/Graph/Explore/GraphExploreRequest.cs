using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGraphExploreRequest : IHop
	{
		[JsonProperty("controls")]
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
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		public GraphExploreDescriptor<T> Vertices(Func<GraphVerticesDescriptor<T>, IPromise<IList<IGraphVertexDefinition>>> selector) =>
			Assign(a => a.Vertices = selector?.Invoke(new GraphVerticesDescriptor<T>())?.Value);

		public GraphExploreDescriptor<T> Connections(Func<HopDescriptor<T>, IHop> selector) =>
			Assign(a => a.Connections = selector?.Invoke(new HopDescriptor<T>()));

		public GraphExploreDescriptor<T> Controls(Func<GraphExploreControlsDescriptor<T>, IGraphExploreControls> selector) =>
			Assign(a => a.Controls = selector?.Invoke(new GraphExploreControlsDescriptor<T>()));
	}
}
