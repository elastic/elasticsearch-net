using System;
using Elasticsearch.Net;

namespace Nest
{
	[DescriptorFor("Mlt")]
	public partial class MoreLikeThisDescriptor<T> : DocumentPathDescriptorBase<MoreLikeThisDescriptor<T>, T, MoreLikeThisRequestParameters>
		where T : class
	{
		internal ISearchRequest _Search { get; set; }
		
		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public MoreLikeThisDescriptor<T> Search(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchDescriptor)
		{
			searchDescriptor.ThrowIfNull("searchDescriptor");
			var d = searchDescriptor(new SearchDescriptor<T>());
			this._Search = d;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MoreLikeThisRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = this._Search == null ? PathInfoHttpMethod.GET : PathInfoHttpMethod.POST;
		}
	}
}
