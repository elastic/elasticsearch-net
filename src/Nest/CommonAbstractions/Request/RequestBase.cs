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
		HttpMethod HttpMethod { get; }

		RequestPath RouteValues { get; }

        void ResolveRouteValues(IConnectionSettingsValues settings);
	}

	public interface IRequest<TParameters> : IRequest
		where TParameters : IRequestParameters, new()
	{
        /// <summary>
        /// Used to describe request parameters not part of the body. e.q query string or 
        /// connection configuration overrides
        /// </summary>
        TParameters RequestParameters { get; set; }
	}

    public abstract class RequestBase<TParameters> : IRequest<TParameters>
        where TParameters : IRequestParameters, new()
    {
        protected RequestBase(Func<RequestPath, RequestPath> pathSelector)
        {
            pathSelector(this.Request.RouteValues);
        }

        [JsonIgnore]
        HttpMethod IRequest.HttpMethod => this.Request.RequestParameters.DefaultHttpMethod;

		[JsonIgnore]
		RequestPath IRequest.RouteValues { get; } = new RequestPath();

        void IRequest.ResolveRouteValues(IConnectionSettingsValues settings) => this.Request.RouteValues.Resolve(settings);

        [JsonIgnore]
        TParameters IRequest<TParameters>.RequestParameters { get; set; } = new TParameters();

        [JsonIgnore]
        protected IRequest<TParameters> Request => this;

		protected TOut Q<TOut>(string name) => this.Request.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => this.Request.RequestParameters.AddQueryStringValue(name, value);
		
	}

    public abstract class RequestDescriptorBase<TDescriptor, TParameters> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
        protected RequestDescriptorBase(Func<RequestPath, RequestPath> pathSelector) : base(pathSelector) { }

		protected TDescriptor AssignParam(Action<TParameters> assigner)
		{
			assigner?.Invoke(this.Request.RequestParameters);
			return (TDescriptor)this;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			this.Request.RequestParameters.RequestConfiguration(configurationSelector);
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