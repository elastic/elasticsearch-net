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
	public partial class SourceDescriptor<T> : DocumentPathDescriptorBase<SourceDescriptor<T>,T, SourceRequestParameters>
		, IPathInfo<SourceRequestParameters>
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
		
		
		ElasticsearchPathInfo<SourceRequestParameters> IPathInfo<SourceRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = this.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;

		}
	}
}
