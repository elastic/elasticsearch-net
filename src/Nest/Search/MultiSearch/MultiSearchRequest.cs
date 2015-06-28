using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMultiSearchRequest : IFixedIndexTypePath<MultiSearchRequestParameters>
	{
		
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, ISearchRequest> Operations { get; set;}

	}

	internal static class MultiSearchPathInfo
	{
		public static void Update(ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo, IMultiSearchRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class MultiSearchRequest : FixedIndexTypePathBase<MultiSearchRequestParameters>, IMultiSearchRequest
	{
		public IDictionary<string, ISearchRequest> Operations { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo)
		{
			MultiSearchPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Msearch")]
	public partial class MultiSearchDescriptor : FixedIndexTypePathDescriptor<MultiSearchDescriptor, MultiSearchRequestParameters>, IMultiSearchRequest
	{
		private IMultiSearchRequest Self => this;

		internal IDictionary<string, ISearchRequest> _operations = new Dictionary<string, ISearchRequest>();

		IDictionary<string, ISearchRequest> IMultiSearchRequest.Operations
		{
			get { return _operations; } 
			set { _operations = value; }
		}

		public MultiSearchDescriptor Search<T>(string name, Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			name.ThrowIfNull("name");
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>().Index(Self.Index).Type(Self.Type));
			if (descriptor == null)
				return this;
			this._operations.Add(name, descriptor);
			return this;
		}

		public MultiSearchDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			return this.Search(Guid.NewGuid().ToString(), searchSelector);
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiSearchRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}

	}
}
