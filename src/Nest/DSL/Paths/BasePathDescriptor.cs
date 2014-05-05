using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace Nest
{
	public class BasePathDescriptor<TDescriptor>
		where TDescriptor : BasePathDescriptor<TDescriptor>
	{
		internal RequestConfiguration _RequestConfiguration { get; set; }

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfiguration, RequestConfiguration> configurationSelector)
		{
			configurationSelector.ThrowIfNull("configurationSelector");
			this._RequestConfiguration = configurationSelector(new RequestConfiguration());
			return (TDescriptor)this;
		}
	}
}