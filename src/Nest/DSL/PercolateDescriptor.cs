using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPercolateRequest<TDocument> : IIndexTypePath<PercolateRequestParameters>
		where TDocument : class
	{
		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }
	}

	internal static class PercolatePathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<PercolateRequestParameters> pathInfo, IPercolateRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class PercolateRequest<TDocument> : IndexTypePathBase<PercolateRequestParameters, TDocument>, IPercolateRequest<TDocument>
		where TDocument : class
	{
		public QueryContainer Query { get; set; }

		public TDocument Document { get; set; }

		public PercolateRequest(TDocument document)
		{
			this.Document = document;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateRequestParameters> pathInfo)
		{
			PercolatePathInfo.Update(pathInfo, this);
		}

	}
	public partial class PercolateDescriptor<T> : IndexTypePathDescriptor<PercolateDescriptor<T>, PercolateRequestParameters, T>, IPercolateRequest<T>
		where T : class
	{
		private IPercolateRequest<T> Self { get { return this; } }

		QueryContainer IPercolateRequest<T>.Query { get; set; }

		T IPercolateRequest<T>.Document { get; set; }

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateDescriptor<T> Object(T @object)
		{
			Self.Document = @object;
			return this;
		}

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public PercolateDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryDescriptor<T>());
			Self.Query = d;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateRequestParameters> pathInfo)
		{
			PercolatePathInfo.Update(pathInfo, this);
		}
	}
}
