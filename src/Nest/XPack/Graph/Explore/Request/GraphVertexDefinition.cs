// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGraphVertexDefinition
	{
		[DataMember(Name ="exclude")]
		IEnumerable<string> Exclude { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="include")]
		IEnumerable<GraphVertexInclude> Include { get; set; }

		[DataMember(Name ="min_doc_count")]
		long? MinimumDocumentCount { get; set; }

		[DataMember(Name ="shard_min_doc_count")]
		long? ShardMinimumDocumentCount { get; set; }

		[DataMember(Name ="size")]
		int? Size { get; set; }
	}

	public class GraphVertexDefinition : IGraphVertexDefinition
	{
		public IEnumerable<string> Exclude { get; set; }
		public Field Field { get; set; }
		public IEnumerable<GraphVertexInclude> Include { get; set; }
		public long? MinimumDocumentCount { get; set; }
		public long? ShardMinimumDocumentCount { get; set; }
		public int? Size { get; set; }
	}

	public class GraphVertexDefinitionDescriptor : DescriptorBase<GraphVertexDefinitionDescriptor, IGraphVertexDefinition>, IGraphVertexDefinition
	{
		public GraphVertexDefinitionDescriptor(Field field) => Assign(field, (a, v) => a.Field = v);

		IEnumerable<string> IGraphVertexDefinition.Exclude { get; set; }
		Field IGraphVertexDefinition.Field { get; set; }
		IEnumerable<GraphVertexInclude> IGraphVertexDefinition.Include { get; set; }
		long? IGraphVertexDefinition.MinimumDocumentCount { get; set; }
		long? IGraphVertexDefinition.ShardMinimumDocumentCount { get; set; }
		int? IGraphVertexDefinition.Size { get; set; }

		public GraphVertexDefinitionDescriptor Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public GraphVertexDefinitionDescriptor MinimumDocumentCount(int? minDocCount) => Assign(minDocCount, (a, v) => a.MinimumDocumentCount = v);

		public GraphVertexDefinitionDescriptor ShardMinimumDocumentCount(int? shardMinDocCount) =>
			Assign(shardMinDocCount, (a, v) => a.ShardMinimumDocumentCount = v);

		public GraphVertexDefinitionDescriptor Exclude(params string[] excludes) => Assign(excludes, (a, v) => a.Exclude = v);

		public GraphVertexDefinitionDescriptor Exclude(IEnumerable<string> excludes) => Assign(excludes, (a, v) => a.Exclude = v);

		public GraphVertexDefinitionDescriptor Include(params string[] includes) => Include(i => i.IncludeRange(includes));

		public GraphVertexDefinitionDescriptor Include(IEnumerable<string> includes) => Include(i => i.IncludeRange(includes));

		public GraphVertexDefinitionDescriptor Include(Func<GraphVertexIncludeDescriptor, IPromise<List<GraphVertexInclude>>> selector) =>
			Assign(selector, (a, v) => a.Include = v?.Invoke(new GraphVertexIncludeDescriptor())?.Value);
	}

	public class GraphVerticesDescriptor<T> : DescriptorPromiseBase<GraphVerticesDescriptor<T>, IList<IGraphVertexDefinition>>
		where T : class
	{
		public GraphVerticesDescriptor() : base(new List<IGraphVertexDefinition>()) { }

		public GraphVerticesDescriptor<T> Vertex<TValue>(Expression<Func<T, TValue>> field,
			Func<GraphVertexDefinitionDescriptor, IGraphVertexDefinition> selector = null
		) =>
			Assign(selector.InvokeOrDefault(new GraphVertexDefinitionDescriptor(field)), (a, v) => a.Add(v));

		public GraphVerticesDescriptor<T> Vertex(Field field, Func<GraphVertexDefinitionDescriptor, IGraphVertexDefinition> selector = null) =>
			Assign(selector.InvokeOrDefault(new GraphVertexDefinitionDescriptor(field)), (a, v) => a.Add(v));
	}
}
