using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;

namespace Nest
{
	public abstract class BasePathDescriptor<TDescriptor, TParameters> : BaseRequest<TParameters>
		where TDescriptor : BasePathDescriptor<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			configurationSelector.ThrowIfNull("configurationSelector");
			this.Request.RequestConfiguration = configurationSelector(new RequestConfigurationDescriptor());
			return (TDescriptor)this;
		}
		
		
	}
}