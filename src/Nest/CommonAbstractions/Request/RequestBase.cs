using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRequest
	{
		//IRequestConfiguration Configuration { get; set; }
        //IRequestParameters Parameters { get; set; }
	}

	public interface IRequest<TParameters> : IRequest
		where TParameters : IRequestParameters, new()
	{
        /// <summary>
        /// Used to describe request parameters not part of the body. e.q query string or 
        /// connection configuration overrides
        /// </summary>
        TParameters Parameters { get; set; }

		HttpMethod HttpMethod { get; }

		RequestPath RouteValues { get; }

        void ResolveRouteValues(IConnectionSettingsValues settings);
	}

    public abstract class RequestBase<TParameters> : IRequest<TParameters>
        where TParameters : IRequestParameters, new()
    {
        private RequestPath _path;

        [JsonIgnore]
        HttpMethod IRequest<TParameters>.HttpMethod => this.Request.Parameters.DefaultHttpMethod;

		[JsonIgnore]
		RequestPath IRequest<TParameters>.RouteValues { get; } = new RequestPath();

        [JsonIgnore]
        TParameters IRequest<TParameters>.Parameters { get; set; } = new TParameters();
		
        public RequestBase() { }

        protected RequestBase(Func<RequestPath, RequestPath> pathSelector)
        {
            _path = pathSelector(new RequestPath());
        }

        [JsonIgnore]
        protected IRequest<TParameters> Request => this;

		protected TOut Q<TOut>(string name) => this.Request.Parameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => this.Request.Parameters.AddQueryStringValue(name, value);
		
        /// <summary>
		/// Creates a PathInfo object from this request that we can use to dispatch into the low level client
		/// </summary>
		internal virtual RequestPath Path(IConnectionSettingsValues settings, TParameters queryString)
        {
			//ask subclasses to set the relevant pathInfo parameters
			SetRouteParameters(settings, _path);

			//update the pathInfo, is abstract and forces each subclass to at a minimum set the HttpMethod
			UpdateRequestPath(settings, _path);

			ValidateRequestPath(_path);

            return _path;
		}

        void IRequest<TParameters>.ResolveRouteValues(IConnectionSettingsValues settings) => this.Request.RouteValues.Resolve(settings);

        protected virtual void SetRouteParameters(IConnectionSettingsValues settings, RequestPath path) { }

		protected virtual void ValidateRequestPath(IRequestPath path) { }

		protected virtual void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
		}
	}

    public abstract class RequestDescriptorBase<TDescriptor, TParameters> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
        public RequestDescriptorBase() { }

        protected RequestDescriptorBase(Func<RequestPath, RequestPath> pathSelector)
            : base(pathSelector)
        { }

		protected TDescriptor _requestParams(Action<TParameters> assigner)
		{
			assigner?.Invoke(this.Request.Parameters);
			return (TDescriptor)this;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			this.Request.Parameters.RequestConfiguration(configurationSelector);
			return (TDescriptor)this;
		}
		
		/// <summary>
		/// Hides the <see cref="Equals"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		/// Hides the <see cref="ToString"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
		
	}
}