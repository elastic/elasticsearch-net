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

	public interface IGraphExploreRequest<T> : IGraphExploreRequest where T : class { }

	public partial class GraphExploreRequest
	{
		public QueryContainer Query { get; set; }
		public IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
		public IHop Connections { get; set; }
		public IGraphExploreControls Controls { get; set; }
	}

	public partial class GraphExploreRequest<T> : IGraphExploreRequest<T>
		where T : class
	{
		public GraphExploreRequest() : this(typeof(T)){}

		public QueryContainer Query { get; set; }
		public IEnumerable<IGraphVertexDefinition> Vertices { get; set; }
		public IHop Connections { get; set; }
		public IGraphExploreControls Controls { get; set; }
	}

	public partial class GraphExploreDescriptor<T> : IGraphExploreRequest<T>
		where T : class
	{
		QueryContainer IHop.Query { get; set; }
		IEnumerable<IGraphVertexDefinition> IHop.Vertices { get; set; }
		IHop IHop.Connections { get; set; }
		IGraphExploreControls IGraphExploreRequest.Controls { get; set; }

		public GraphExploreDescriptor() : this(typeof(T)){}

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
