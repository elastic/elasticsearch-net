using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGraphVertexDefinition
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("min_doc_count")]
		long? MinimumDocumentCount { get; set; }

		[JsonProperty("shard_min_doc_count")]
		long? ShardMinimumDocumentCount { get; set; }

		[JsonProperty("exclude")]
		IEnumerable<string> Exclude { get; set; }

		[JsonProperty("include")]
		IEnumerable<GraphVertexInclude> Include { get; set; }
	}
	public class GraphVertexDefinition : IGraphVertexDefinition
	{
		public Field Field { get; set; }
		public int? Size { get; set; }
		public long? MinimumDocumentCount { get; set; }
		public long? ShardMinimumDocumentCount { get; set; }
		public IEnumerable<string> Exclude { get; set; }
		public IEnumerable<GraphVertexInclude> Include { get; set; }
	}
	public class GraphVertexDefinitionDescriptor : DescriptorBase<GraphVertexDefinitionDescriptor, IGraphVertexDefinition>, IGraphVertexDefinition
	{
		Field IGraphVertexDefinition.Field { get; set; }
		int? IGraphVertexDefinition.Size { get; set; }
		long? IGraphVertexDefinition.MinimumDocumentCount { get; set; }
		long? IGraphVertexDefinition.ShardMinimumDocumentCount { get; set; }
		IEnumerable<string> IGraphVertexDefinition.Exclude { get; set; }
		IEnumerable<GraphVertexInclude> IGraphVertexDefinition.Include { get; set; }

		public GraphVertexDefinitionDescriptor(Field field) { Assign(a => a.Field = field); }

		public GraphVertexDefinitionDescriptor Size(int? size) => Assign(a => a.Size = size);

		public GraphVertexDefinitionDescriptor MinimumDocumentCount(int? minDocCount) => Assign(a => a.MinimumDocumentCount = minDocCount);

		public GraphVertexDefinitionDescriptor ShardMinimumDocumentCount(int? shardMinDocCount) => Assign(a => a.ShardMinimumDocumentCount = shardMinDocCount);

		public GraphVertexDefinitionDescriptor Exclude(params string[] excludes) => Assign(a => a.Exclude = excludes);

		public GraphVertexDefinitionDescriptor Exclude(IEnumerable<string> excludes) => Assign(a => a.Exclude = excludes);

		public GraphVertexDefinitionDescriptor Include(params string[] includes) => Include(i=>i.IncludeRange(includes));

		public GraphVertexDefinitionDescriptor Include(IEnumerable<string> includes) => Include(i=>i.IncludeRange(includes));

		public GraphVertexDefinitionDescriptor Include(Func<GraphVertexIncludeDescriptor, IPromise<List<GraphVertexInclude>>> selector) =>
			Assign(a => a.Include = selector?.Invoke(new GraphVertexIncludeDescriptor())?.Value);
	}

	public class GraphVerticesDescriptor<T> : DescriptorPromiseBase<GraphVerticesDescriptor<T>, IList<IGraphVertexDefinition>>
		where T : class
	{
		public GraphVerticesDescriptor() : base(new List<IGraphVertexDefinition>()) { }

		public GraphVerticesDescriptor<T> Vertex(Expression<Func<T, object>> field, Func<GraphVertexDefinitionDescriptor, IGraphVertexDefinition> selector = null) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new GraphVertexDefinitionDescriptor(field))));

		public GraphVerticesDescriptor<T> Vertex(Field field, Func<GraphVertexDefinitionDescriptor, IGraphVertexDefinition> selector = null) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new GraphVertexDefinitionDescriptor(field))));
	}
}
