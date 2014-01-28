using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("IndicesPutWarmer")]
	[JsonConverter(typeof(ActAsQueryConverter))]
	public partial class PutWarmerDescriptor :
		IndicesOptionalTypesNamePathDecriptor<PutWarmerDescriptor, PutWarmerQueryString>
		, IPathInfo<PutWarmerQueryString>
		, IActAsSearchDescriptor
	{
		SearchDescriptorBase IActAsSearchDescriptor._SearchDescriptor { get; set; }

		public PutWarmerDescriptor Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> selector)
			where T : class
		{
			((IActAsSearchDescriptor)this)._SearchDescriptor = selector(new SearchDescriptor<T>());
			return this;
		}

		ElasticSearchPathInfo<PutWarmerQueryString> IPathInfo<PutWarmerQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<PutWarmerQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;

			return pathInfo;
		}

	}
}
