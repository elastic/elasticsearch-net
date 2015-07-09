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
		public ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null) =>
			this.DoCat<CatPluginsDescriptor, CatPluginsRequestParameters, CatPluginsRecord>(selector, this.LowLevelDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);

		public ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request) =>
			this.DoCat<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.LowLevelDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null) =>
			this.DoCatAsync<CatPluginsDescriptor, CatPluginsRequestParameters, CatPluginsRecord>(selector, this.LowLevelDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request) =>
			this.DoCatAsync<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.LowLevelDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);

	}
}