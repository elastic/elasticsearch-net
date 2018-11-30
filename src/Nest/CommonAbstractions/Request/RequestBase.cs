using System;
using System.ComponentModel;
using System.IO;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRequest
	{
		HttpMethod HttpMethod { get; }

		RouteValues RouteValues { get; }
	}

	public interface IRequest<TParameters> : IRequest
		where TParameters : IRequestParameters, new()
	{
		/// <summary>
		/// Used to describe request parameters that are not part of the body. e.g. query string, connection configuration overrides, etc.
		/// </summary>
		TParameters RequestParameters { get; set; }
	}

	/// <summary>
	/// A request that that does not necessarily (de)serializes itself
	/// </summary>
	public interface IProxyRequest : IRequest
	{
		void WriteJson(IElasticsearchSerializer sourceSerializer, Stream s, SerializationFormatting serializationFormatting);
	}

	public abstract class RequestBase<TParameters> : IRequest<TParameters> where TParameters : IRequestParameters, new()
	{
		protected RequestBase() => Initialize();

		protected RequestBase(Func<RouteValues, RouteValues> pathSelector)
		{
			pathSelector(RequestState.RouteValues);
			Initialize();
		}

		protected virtual HttpMethod HttpMethod => RequestState.RequestParameters.DefaultHttpMethod;

		[IgnoreDataMember]
		protected IRequest<TParameters> RequestState => this;

		[IgnoreDataMember]
		HttpMethod IRequest.HttpMethod => HttpMethod;

		[IgnoreDataMember]
		TParameters IRequest<TParameters>.RequestParameters { get; set; } = new TParameters();

		[IgnoreDataMember]
		RouteValues IRequest.RouteValues { get; } = new RouteValues();

		protected virtual void Initialize() { }

		protected TOut Q<TOut>(string name) => RequestState.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => RequestState.RequestParameters.SetQueryString(name, value);
	}

	public abstract partial class PlainRequestBase<TParameters> : RequestBase<TParameters>
		where TParameters : IRequestParameters, new()
	{
		protected PlainRequestBase() { }

		protected PlainRequestBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public IRequestConfiguration RequestConfiguration
		{
			get => RequestState.RequestParameters.RequestConfiguration;
			set => RequestState.RequestParameters.RequestConfiguration = value;
		}
	}

	/// <summary>
	///  Base class for all Request descriptor types
	/// </summary>
	public abstract partial class RequestDescriptorBase<TDescriptor, TParameters, TInterface> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters, TInterface>, TInterface
		where TParameters : RequestParameters<TParameters>, new()
	{
		private readonly TDescriptor _descriptor;

		protected RequestDescriptorBase() => _descriptor = (TDescriptor)this;

		protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) => _descriptor = (TDescriptor)this;

		protected IRequestConfiguration RequestConfig => ((IRequestParameters)RequestState.RequestParameters).RequestConfiguration;

		protected TInterface Self => _descriptor;

		protected TDescriptor Assign(Action<TInterface> assign) => Fluent.Assign(_descriptor, assign);

		protected TDescriptor AssignParam(Action<TParameters> assigner)
		{
			assigner?.Invoke(RequestState.RequestParameters);
			return _descriptor;
		}

		protected TDescriptor Qs(Action<TParameters> assigner)
		{
			assigner?.Invoke(RequestState.RequestParameters);
			return _descriptor;
		}

		protected TDescriptor Qs(string name, object value)
		{
			Q(name, value);
			return _descriptor;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			var rc = RequestState.RequestParameters.RequestConfiguration;
			RequestState.RequestParameters.RequestConfiguration = configurationSelector?.Invoke(new RequestConfigurationDescriptor(rc)) ?? rc;
			return _descriptor;
		}

		/// <summary>
		/// Hides the <see cref="Equals" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		/// Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
	}
}
