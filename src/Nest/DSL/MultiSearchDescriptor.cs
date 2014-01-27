using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Domain;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("Msearch")]
	public partial class MultiSearchDescriptor 
		: FixedIndexTypePathDescriptor<MultiSearchDescriptor, MultiSearchQueryString>
		, IPathInfo<MultiSearchQueryString>
	{
		internal string _FixedIndex { get; set; }
		internal string _FixedType { get; set; }

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<string, SearchDescriptorBase> _Operations = new Dictionary<string, SearchDescriptorBase>();

		public MultiSearchDescriptor Search<T>(string name, Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			name.ThrowIfNull("name");
			searchSelector.ThrowIfNull("searchSelector");
			var descriptor = searchSelector(new SearchDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(name, descriptor);
			return this;
		}

		public MultiSearchDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
		{
			return this.Search(Guid.NewGuid().ToString(), searchSelector);
		}

		ElasticSearchPathInfo<MultiSearchQueryString> IPathInfo<MultiSearchQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<MultiSearchQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			return pathInfo;
		}
	}
}
