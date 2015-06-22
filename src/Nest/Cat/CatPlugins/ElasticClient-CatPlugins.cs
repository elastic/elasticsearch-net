using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null)
		{
			return this.DoCat<CatPluginsDescriptor, CatPluginsRequestParameters, CatPluginsRecord>(selector, this.RawDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);
		}

		public ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request)
		{
			return this.DoCat<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.RawDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);
		}

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null)
		{
			return this.DoCatAsync<CatPluginsDescriptor, CatPluginsRequestParameters, CatPluginsRecord>(selector, this.RawDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);
		}

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request)
		{
			return this.DoCatAsync<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.RawDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);
		}
	}
}