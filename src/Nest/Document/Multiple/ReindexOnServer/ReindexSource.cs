using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IReindexSource
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("sort")]
		IList<ISort> Sort { get; set; }

		[JsonProperty("index")]
		Indices Index { get; set; }

		[JsonProperty("type")]
		Types Type { get; set; }
	}

	public class ReindexSource : IReindexSource
	{
		public QueryContainer Query { get; set; }

		public IList<ISort> Sort { get; set; }

		public Indices Index { get; set; }

		public Types Type { get; set; }
	}

	public class ReindexSourceDescriptor : DescriptorBase<ReindexSourceDescriptor, IReindexSource>, IReindexSource
	{
		QueryContainer IReindexSource.Query { get; set; }
		IList<ISort> IReindexSource.Sort { get; set; }
		Indices IReindexSource.Index { get; set; }
		Types IReindexSource.Type { get; set; }

		public ReindexSourceDescriptor Query<T>(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) where T : class =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		public ReindexSourceDescriptor Sort<T>(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) where T : class =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		public ReindexSourceDescriptor Index(Indices indices) => Assign(a => a.Index = indices);

		public ReindexSourceDescriptor Type(Types types) => Assign(a => a.Type = types);

	}
}
