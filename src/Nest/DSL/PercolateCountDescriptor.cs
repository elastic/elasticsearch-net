using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPercolateCountRequest<TDocument> : IIndexTypePath<PercolateCountRequestParameters>
		where TDocument : class
	{
		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }
	}

	internal static class PercolateCountPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo, IPercolateCountRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class PercolateCountRequest<TDocument> : IndexTypePathBase<PercolateCountRequestParameters, TDocument>, IPercolateCountRequest<TDocument>
		where TDocument : class
	{
		public QueryContainer Query { get; set; }

		public TDocument Document { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo)
		{
			PercolateCountPathInfo.Update(pathInfo, this);
		}

	}
	
	[DescriptorFor("CountPercolate")]
	public partial class PercolateCountDescriptor<T> : IndexTypePathDescriptor<PercolateCountDescriptor<T>, PercolateCountRequestParameters, T>
		, IPercolateCountRequest<T>
		where T : class
	{

		private IPercolateCountRequest<T> Self { get { return this; } }

		QueryContainer IPercolateCountRequest<T>.Query { get; set; }

		T IPercolateCountRequest<T>.Document { get; set; }

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateCountDescriptor<T> Object(T @object)
		{
			Self.Document = @object;
			return this;
		}

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public PercolateCountDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryDescriptor<T>());
			Self.Query = d;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo)
		{
			PercolateCountPathInfo.Update(pathInfo, this);
		}
	}
}
