using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	public partial class GetDescriptor<T> : DocumentPathDescriptorBase<GetDescriptor<T>,T, GetQueryString>
		, IPathInfo<GetQueryString>
		where T : class
	{

		public GetDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public GetDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}

		ElasticsearchPathInfo<GetQueryString> IPathInfo<GetQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = this.ToPathInfo<GetQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;

		}
	}
}
