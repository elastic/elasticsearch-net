using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;

namespace Nest
{
    public abstract class RequestBase<TParameters> : IRequest<TParameters>
        where TParameters : IRequestParameters, new()
    {
        [JsonIgnore]
        protected IRequest<TParameters> Request => this;

        /// <summary>
        /// Allows you to override connection settings on a per call basis
        /// </summary>
        [JsonIgnore]
        IRequestConfiguration IRequest.RequestConfiguration { get; set; }

        /// <summary>
        /// Describes parameters that are supplied on the querystring rather then the body of the request
        /// </summary>
        [JsonIgnore]
        TParameters IRequest<TParameters>.RequestParameters { get; set; } = new TParameters();

        [JsonIgnore]
        public IRequestPath<TParameters> Path { get; set; }

		/// <summary>
		/// Creates a PathInfo object from this request that we can use to dispatch into the low level client
		/// </summary>
		internal virtual IRequestPath<TParameters> GetRequestPath(
			IConnectionSettingsValues settings, 
			TParameters queryString
			)
		{
            this.Path = this.Path ?? new RequestPath<TParameters>();

            this.Path.RequestParameters = queryString;
			//if this request describes request specific connection overrides make sure they are carried 
			//over into the pathInfo object
			var config = ((IRequest) this).RequestConfiguration;
			if (config != null)
			{
				IRequestParameters p = this.Path.RequestParameters;
				p.RequestConfiguration = config;
			}
			
			//ask subclasses to set the relevant pathInfo parameters
			SetRouteParameters(settings, this.Path);

			//update the pathInfo, is abstract and forces each subclass to at a minimum set the HttpMethod
			UpdateRequestPath(settings, this.Path);

			ValidateRequestPath(this.Path);

            return this.Path; 
		}

		protected virtual void SetRouteParameters(IConnectionSettingsValues settings, IRequestPath<TParameters> pathInfo) { }

		protected virtual void ValidateRequestPath(IRequestPath<TParameters> pathInfo) { }

		protected virtual void UpdateRequestPath(IConnectionSettingsValues settings, IRequestPath<TParameters> pathInfo)
		{
			pathInfo.HttpMethod = pathInfo.RequestParameters.DefaultHttpMethod;
		}
	}
}