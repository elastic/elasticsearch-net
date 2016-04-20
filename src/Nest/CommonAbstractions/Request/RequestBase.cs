using System;
using System.ComponentModel;
using Elasticsearch.Net;
using Newtonsoft.Json;

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
		protected IRequest<TParameters> RequestState => this;

		protected RequestBase() { }
		protected RequestBase(Func<RouteValues, RouteValues> pathSelector)
		{
			pathSelector(RequestState.RouteValues);
		}

		protected virtual HttpMethod HttpMethod => RequestState.RequestParameters.DefaultHttpMethod;

		[JsonIgnore]
		HttpMethod IRequest.HttpMethod => this.HttpMethod;

		[JsonIgnore]
		RouteValues IRequest.RouteValues { get; } = new RouteValues();

		[JsonIgnore]
		TParameters IRequest<TParameters>.RequestParameters { get; set; } = new TParameters();

		protected TOut Q<TOut>(string name) => RequestState.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => RequestState.RequestParameters.AddQueryStringValue(name, value);

	}

	public abstract class PlainRequestBase<TParameters> : RequestBase<TParameters>
		where TParameters : IRequestParameters, new()
	{
		protected PlainRequestBase() { }
		protected PlainRequestBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public IRequestConfiguration RequestConfiguration
		{
			get { return RequestState.RequestParameters.RequestConfiguration;  }
			set { RequestState.RequestParameters.RequestConfiguration = value; }
		}
	}


	public abstract class RequestDescriptorBase<TDescriptor, TParameters, TInterface> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters, TInterface>, TInterface
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private readonly TDescriptor _descriptor;

		protected RequestDescriptorBase() { _descriptor = (TDescriptor)this; }
		protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { _descriptor = (TDescriptor)this;  }

		protected TInterface Self => _descriptor;
		protected IRequestConfiguration RequestConfig => ((IRequestParameters)RequestState.RequestParameters).RequestConfiguration;

		protected TDescriptor Assign(Action<TInterface> assign) => Fluent.Assign(_descriptor, assign);

		protected TDescriptor AssignParam(Action<TParameters> assigner)
		{
			assigner?.Invoke(this.RequestState.RequestParameters);
			return _descriptor;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			RequestState.RequestParameters.RequestConfiguration(configurationSelector);
			return _descriptor;
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
