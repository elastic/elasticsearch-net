using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("ClearScroll")]
	public partial class ClearScrollDescriptor : BasePathDescriptor<ClearScrollDescriptor>
		, IPathInfo<ClearScrollRequestParameters>
	{
		internal string _ScrollId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public ClearScrollDescriptor ScrollId(string scrollId)
		{
			this._ScrollId = scrollId;
			return this;
		}

		ElasticsearchPathInfo<ClearScrollRequestParameters> IPathInfo<ClearScrollRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			if (this._ScrollId.IsNullOrEmpty())
				throw new DslException("missing ScrollId()");

			var pathInfo = new ElasticsearchPathInfo<ClearScrollRequestParameters>();
			pathInfo.RequestParameters = this._QueryString;
			pathInfo.ScrollId = this._ScrollId;
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
			
			return pathInfo;
		}
	}
}
