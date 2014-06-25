using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Domain;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("Msearch")]
	public partial class MultiSearchDescriptor : FixedIndexTypePathDescriptor<MultiSearchDescriptor, MultiSearchRequestParameters>
	{
		private readonly ElasticInferrer _inferrer;

		public MultiSearchDescriptor(ElasticInferrer inferrer)
		{
			_inferrer = inferrer;
		}

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<string, ISearchRequest> _Operations = new Dictionary<string, ISearchRequest>();

		public MultiSearchDescriptor Search<T>(string name, Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			name.ThrowIfNull("name");
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>().Index(this._Index).Type(this._Type));
			if (descriptor == null)
				return this;
			descriptor.CreateCovarianceSelector<T>(_inferrer);
			this._Operations.Add(name, descriptor);
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
