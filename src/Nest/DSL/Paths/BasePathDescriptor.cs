using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;

namespace Nest
{
	
	public abstract class BaseRequest<TParameters> : IRequest<TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		protected IRequest<TParameters> Request { get { return this; } }
		
		[JsonIgnore]
		IRequestConfiguration IRequest<TParameters>.RequestConfiguration { get; set; }

		private TParameters _requestParameters = new TParameters();

		[JsonIgnore]
		TParameters IRequest<TParameters>.RequestParameters  
		{ 
			get { return _requestParameters; }
			set { _requestParameters = value; }
		}

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(
			IConnectionSettingsValues settings, 
			TParameters queryString
			)
		{
			var pathInfo = new ElasticsearchPathInfo<TParameters>();
			pathInfo.RequestParameters = queryString ?? new TParameters();
			var config = this.Request.RequestConfiguration;
			if (config != null)
			{
				IRequestParameters p = pathInfo.RequestParameters;
				p.RequestConfiguration = config;
			}

			SetRouteParameters(settings, pathInfo);

			UpdatePathInfo(settings, pathInfo);
			return pathInfo;
		}

		protected virtual void SetRouteParameters(
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
		{
			
		}

		protected abstract void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo);
		
		ElasticsearchPathInfo<TParameters> IPathInfo<TParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			return this.ToPathInfo(settings, this.Request.RequestParameters);
		}

	}

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