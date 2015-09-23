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

		RouteValues RouteValues { get; }
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
		[JsonIgnore]
		protected IRequest<TParameters> Self => this;

		protected RequestBase() { }
		protected RequestBase(Func<RouteValues, RouteValues> pathSelector)
		{
			pathSelector(Self.RouteValues);
		}

		protected virtual HttpMethod HttpMethod => Self.RequestParameters.DefaultHttpMethod;

		[JsonIgnore]
		HttpMethod IRequest.HttpMethod => this.HttpMethod;

		[JsonIgnore]
		RouteValues IRequest.RouteValues { get; } = new RouteValues();

		[JsonIgnore]
		TParameters IRequest<TParameters>.RequestParameters { get; set; } = new TParameters();


		protected TOut Q<TOut>(string name) => Self.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => Self.RequestParameters.AddQueryStringValue(name, value);

	}

	public abstract class RequestDescriptorBase<TDescriptor, TParameters> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		protected RequestDescriptorBase() { }
		protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }

		protected TDescriptor AssignParam(Action<TParameters> assigner)
		{
			assigner?.Invoke(this.Self.RequestParameters);
			return (TDescriptor)this;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			Self.RequestParameters.RequestConfiguration(configurationSelector);
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
	public abstract class RequestDescriptorBase<TDescriptor, TParameters, TInterface>
		: RequestDescriptorBase<TDescriptor, TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters, TInterface>, TInterface
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		protected RequestDescriptorBase() { }
		protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }
		protected TDescriptor Assign(Action<TInterface> assign) => Fluent.Assign((TDescriptor)this, assign);
	}
}