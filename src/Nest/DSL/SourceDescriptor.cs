using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	[DescriptorFor("GetSource")]
	public partial class SourceDescriptor<T> : DocumentPathDescriptorBase<SourceDescriptor<T>,T, SourceQueryString>
		, IPathInfo<SourceQueryString>
		where T : class
	{

		public SourceDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}

		public SourceDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}
		
		
		ElasticsearchPathInfo<SourceQueryString> IPathInfo<SourceQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = this.ToPathInfo<SourceQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;

		}
	}
}
