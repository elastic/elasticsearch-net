using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesPutWarmer")]
	[JsonConverter(typeof(CustomJsonConverter))]
	public partial class PutWarmerDescriptor :
		IndicesOptionalTypesNamePathDecriptor<PutWarmerDescriptor, PutWarmerRequestParameters>
		, IPathInfo<PutWarmerRequestParameters>
		, IActAsSearchDescriptor
		, ICustomJson
	{
		SearchDescriptorBase IActAsSearchDescriptor._SearchDescriptor { get; set; }

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			((IActAsSearchDescriptor)this)._SearchDescriptor = selector(new SearchDescriptor<T>());
			return this;
		}

		ElasticsearchPathInfo<PutWarmerRequestParameters> IPathInfo<PutWarmerRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<PutWarmerRequestParameters>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;

			return pathInfo;
		}
		object ICustomJson.GetCustomJson()
		{
			return ((IActAsSearchDescriptor)this)._SearchDescriptor;
		}
	}
}
