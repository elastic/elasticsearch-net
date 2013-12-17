using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public interface IActAsQuery
	{
		BaseQuery _Query { get; set; }
	}

	[JsonConverter(typeof(ActAsQueryConverter))]
	public partial class ValidateQueryDescriptor<T> 
		:	QueryPathDescriptorBase<ValidateQueryDescriptor<T>, T, ValidateQueryQueryString>,
			IActAsQuery
		where T : class
	{
		BaseQuery IActAsQuery._Query { get; set; }

		public ValidateQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			((IActAsQuery)this)._Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		internal new ElasticSearchPathInfo<ValidateQueryQueryString> ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<ValidateQueryQueryString>(settings);
			pathInfo.QueryString = this._QueryString;
			var qs = this._QueryString;
			pathInfo.HttpMethod = (!qs._source.IsNullOrEmpty() || !qs._q.IsNullOrEmpty())
				? PathInfoHttpMethod.GET
				: PathInfoHttpMethod.POST;
				
			return pathInfo;
		}
	}
}
