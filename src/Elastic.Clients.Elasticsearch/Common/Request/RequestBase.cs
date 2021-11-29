using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public interface IRequest
	{
		[JsonIgnore] string? ContentType { get; }

		[JsonIgnore] HttpMethod HttpMethod { get; }

		[JsonIgnore] bool SupportsBody { get; }

		[JsonIgnore] RouteValues RouteValues { get; }

		[JsonIgnore] IRequestParameters RequestParameters { get; }

		//[JsonIgnore] bool CanBeEmpty { get; }

		//[JsonIgnore] bool IsEmpty { get; }

		string GetUrl(IElasticsearchClientSettings settings);
	}

	public interface IRequest<out TParameters> : IRequest
		where TParameters : class, IRequestParameters, new()
	{
		/// <summary>
		///     Used to describe request parameters that are not part of the body. e.g. query string, connection configuration
		///     overrides, etc.
		/// </summary>
		[JsonIgnore]
		new TParameters RequestParameters { get; }
	}

	public abstract class RequestBase<TParameters> : IRequest<TParameters>
		where TParameters : class, IRequestParameters, new()
	{
		private readonly TParameters _parameters;

		// ReSharper disable once VirtualMemberCallInConstructor
		protected RequestBase()
		{
			_parameters = new TParameters();
			// ReSharper disable once VirtualMemberCallInConstructor
			RequestDefaults(_parameters);
		}

		protected RequestBase(Func<RouteValues, RouteValues> pathSelector)
		{
			pathSelector(RequestState.RouteValues);
			_parameters = new TParameters();
			// ReSharper disable once VirtualMemberCallInConstructor
			RequestDefaults(_parameters);
		}

		protected virtual HttpMethod? DynamicHttpMethod { get; }

		protected abstract HttpMethod HttpMethod { get; }

		protected abstract bool SupportsBody { get; }

		internal virtual void BeforeRequest() { }

		//protected virtual bool CanBeEmpty => false;

		//protected virtual bool IsEmpty => false;

		[JsonIgnore] protected IRequest<TParameters> RequestState => this;

		protected virtual string? ContentType { get; } = null;

		internal abstract ApiUrls ApiUrls { get; }

		[JsonIgnore] HttpMethod IRequest.HttpMethod => DynamicHttpMethod ?? HttpMethod;

		[JsonIgnore] bool IRequest.SupportsBody => SupportsBody;

		//[JsonIgnore] bool IRequest.CanBeEmpty => CanBeEmpty;

		//[JsonIgnore] bool IRequest.IsEmpty => IsEmpty;

		[JsonIgnore] string? IRequest.ContentType => ContentType;

		[JsonIgnore] TParameters IRequest<TParameters>.RequestParameters => _parameters;

		IRequestParameters IRequest.RequestParameters => _parameters;

		[JsonIgnore] RouteValues IRequest.RouteValues { get; } = new();

		string IRequest.GetUrl(IElasticsearchClientSettings settings) => ResolveUrl(RequestState.RouteValues, settings);

		protected virtual string ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings) =>
			ApiUrls.Resolve(routeValues, settings);

		/// <summary>
		///     Allows a request implementation to set certain request parameter defaults, use sparingly!
		/// </summary>
		protected virtual void RequestDefaults(TParameters parameters) { }

		protected TOut Q<TOut>(string name) => RequestState.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => RequestState.RequestParameters.SetQueryString(name, value);

		protected void Q(string name, IStringable value) => RequestState.RequestParameters.SetQueryString(name, value.GetString());

		protected void SetAcceptHeader(string format)
		{
			RequestState.RequestParameters.RequestConfiguration ??= new RequestConfiguration();
			RequestState.RequestParameters.RequestConfiguration.Accept =
				RequestState.RequestParameters.AcceptHeaderFromFormat(format);
		}
	}

	public abstract partial class PlainRequestBase<TParameters> : RequestBase<TParameters>
		where TParameters : class, IRequestParameters, new()
	{
		protected PlainRequestBase() { }

		protected PlainRequestBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) { }

		/// <summary>
		///     Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		[JsonIgnore]
		public IRequestConfiguration RequestConfiguration
		{
			get => RequestState.RequestParameters.RequestConfiguration;
			set => RequestState.RequestParameters.RequestConfiguration = value;
		}
	}

	/// <summary>
	///     Base class for all Request descriptor types
	/// </summary>
	public abstract partial class
		RequestDescriptorBase<TDescriptor, TParameters> : RequestBase<TParameters>, IDescriptor, ISelfSerializable
			where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters>, IRequest<TParameters>
			where TParameters : RequestParameters<TParameters>, new()
	{
		private readonly TDescriptor _descriptor;

		void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => Serialize(writer, options, settings);

		protected RequestDescriptorBase() => _descriptor = (TDescriptor)this;

		protected abstract void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

		protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) =>
			_descriptor = (TDescriptor)this;

		protected TDescriptor Self => _descriptor;

		protected RouteValues RouteValues => ((IRequest<TParameters>)this).RouteValues;

		protected TDescriptor Assign<TValue>(TValue value, Action<TDescriptor, TValue> assign) => Fluent.Assign(_descriptor, value, assign);

		protected TDescriptor InvokeAndAssign<T>(Action<T> configure, Action<TDescriptor, T> assign) where T : new()
		{
			var d = new T();
			configure(d);
			return Fluent.Assign(_descriptor, d, assign);
		}

		protected TDescriptor Qs(string name, object value)
		{
			Q(name, value);
			return _descriptor;
		}

		protected TDescriptor Qs(string name, IStringable value)
		{
			Q(name, value.GetString());
			return _descriptor;
		}

		/// <summary>
		///     Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(
			Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			var rc = RequestState.RequestParameters.RequestConfiguration;
			RequestState.RequestParameters.RequestConfiguration =
				configurationSelector?.Invoke(new RequestConfigurationDescriptor(rc)) ?? rc;
			return _descriptor;
		}

		/// <summary>
		///     Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();

		/// <summary>
		///     Hides the <see cref="Equals" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable BaseObjectEqualsIsObjectEquals
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		///     Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
		public override int GetHashCode() => base.GetHashCode();
		// ReSharper restore BaseObjectEqualsIsObjectEquals
	}

	/// *** THIS IS THE ORIGINAL FORMAT
	///// <summary>
	/////     Base class for all Request descriptor types
	///// </summary>
	//public abstract partial class
	//	RequestDescriptorBase<TDescriptor, TParameters, TInterface> : RequestBase<TParameters>, IDescriptor
	//	where TDescriptor : RequestDescriptorBase<TDescriptor, TParameters, TInterface>, TInterface
	//	where TParameters : RequestParameters<TParameters>, new()
	//{
	//	private readonly TDescriptor _descriptor;

	//	protected RequestDescriptorBase() => _descriptor = (TDescriptor)this;

	//	protected RequestDescriptorBase(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) =>
	//		_descriptor = (TDescriptor)this;

	//	protected TInterface Self => _descriptor;

	//	protected TDescriptor Assign<TValue>(TValue value, Action<TInterface, TValue> assign) =>
	//		Fluent.Assign(_descriptor, value, assign);

	//	protected TDescriptor Qs(string name, object value)
	//	{
	//		Q(name, value);
	//		return _descriptor;
	//	}

	//	/// <summary>
	//	///     Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
	//	/// </summary>
	//	public TDescriptor RequestConfiguration(
	//		Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
	//	{
	//		var rc = RequestState.RequestParameters.RequestConfiguration;
	//		RequestState.RequestParameters.RequestConfiguration =
	//			configurationSelector?.Invoke(new RequestConfigurationDescriptor(rc)) ?? rc;
	//		return _descriptor;
	//	}

	//	/// <summary>
	//	///     Hides the <see cref="ToString" /> method.
	//	/// </summary>
	//	[Browsable(false)]
	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	public override string ToString() => base.ToString();

	//	/// <summary>
	//	///     Hides the <see cref="Equals" /> method.
	//	/// </summary>
	//	[Browsable(false)]
	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	// ReSharper disable BaseObjectEqualsIsObjectEquals
	//	public override bool Equals(object obj) => base.Equals(obj);

	//	/// <summary>
	//	///     Hides the <see cref="GetHashCode" /> method.
	//	/// </summary>
	//	[Browsable(false)]
	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
	//	public override int GetHashCode() => base.GetHashCode();
	//	// ReSharper restore BaseObjectEqualsIsObjectEquals
	//}
}
