using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace Nest
{
	public class BasePathDescriptor<T>
		where T : BasePathDescriptor<T>
	{
		internal IRequestConfiguration _RequestConfiguration { get; set; }

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public T RequestConfiguration(Func<RequestConfiguration, IRequestConfiguration> configurationSelector)
		{
			configurationSelector.ThrowIfNull("configurationSelector");
			this._RequestConfiguration = configurationSelector(new RequestConfiguration());
			return (T)this;
		}
	}
}