using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Shared.Extensions;
using System;

namespace Nest
{
	public interface IPathDescriptor
	{
		RequestConfiguration RequestConfiguration { get; set; }
	}

	public class BasePathDescriptor<TDescriptor> : IPathDescriptor
		where TDescriptor : BasePathDescriptor<TDescriptor>
	{
		RequestConfiguration IPathDescriptor.RequestConfiguration { get; set; }

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfiguration, RequestConfiguration> configurationSelector)
		{
			configurationSelector.ThrowIfNull("configurationSelector");
			((IPathDescriptor)this).RequestConfiguration =
				configurationSelector(((IPathDescriptor)this).RequestConfiguration ?? new RequestConfiguration());
			return (TDescriptor)this;
		}
		internal ElasticsearchPathInfo<TParameters> ToPathInfo<TParameters>(TParameters queryString)
			where TParameters : FluentRequestParameters<TParameters>, new()
		{
			var pathInfo = new ElasticsearchPathInfo<TParameters>();
			pathInfo.RequestParameters = queryString ?? new TParameters();
			var config = ((IPathDescriptor)this).RequestConfiguration;
			if (config != null)
				pathInfo.RequestParameters.RequestConfiguration(r => config);
			return pathInfo;
		}
	}
}