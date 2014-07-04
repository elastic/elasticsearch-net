using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMoreLikeThisRequest : IDocumentOptionalPath<MoreLikeThisRequestParameters>
	{
		ISearchRequest Search { get; set; }
	}

	public interface IMoreLikeThisRequest<T> : IMoreLikeThisRequest where T : class { }

	internal static class MoreLikeThisPathInfo
	{
		public static void Update(ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo, IMoreLikeThisRequest request)
		{
			pathInfo.HttpMethod = request.Search == null ? PathInfoHttpMethod.GET : PathInfoHttpMethod.POST;
		}
	}

	public partial class MoreLikeThisRequest : DocumentPathBase<MoreLikeThisRequestParameters>, IMoreLikeThisRequest
	{
		public MoreLikeThisRequest(IndexNameMarker indexName, TypeNameMarker typeName, string id) : base(indexName, typeName, id) { }

		public ISearchRequest Search { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo)
		{
			MoreLikeThisPathInfo.Update(pathInfo, this);
		}
	}

	public partial class MoreLikeThisRequest<T> : DocumentPathBase<MoreLikeThisRequestParameters, T>, IMoreLikeThisRequest<T>
		where T : class
	{
		public ISearchRequest Search { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo)
		{
			MoreLikeThisPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Mlt")]
	public partial class MoreLikeThisDescriptor<T> : DocumentPathDescriptor<MoreLikeThisDescriptor<T>, MoreLikeThisRequestParameters, T>
		, IMoreLikeThisRequest
		where T : class
	{
		private IMoreLikeThisRequest Self { get { return this; } }

		ISearchRequest IMoreLikeThisRequest.Search { get; set; }

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public MoreLikeThisDescriptor<T> Search(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchDescriptor)
		{
			searchDescriptor.ThrowIfNull("searchDescriptor");
			var d = searchDescriptor(new SearchDescriptor<T>());
			Self.Search = d;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo)
		{
			MoreLikeThisPathInfo.Update(pathInfo, this);
		}
	}
}
