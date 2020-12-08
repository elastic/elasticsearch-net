// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRequest
	{
		[IgnoreDataMember]
		string ContentType { get; }

		[IgnoreDataMember]
		HttpMethod HttpMethod { get; }

		[IgnoreDataMember]
		RouteValues RouteValues { get; }

		[IgnoreDataMember]
		IRequestParameters RequestParameters { get; }

		string GetUrl(IConnectionSettingsValues settings);
	}

	public interface IRequest<out TParameters> : IRequest
		where TParameters : class, IRequestParameters, new()
	{
		/// <summary>
		/// Used to describe request parameters that are not part of the body. e.g. query string, connection configuration
		/// overrides, etc.
		/// </summary>
		[IgnoreDataMember]
		new TParameters RequestParameters { get; }
	}

	public abstract class RequestBase<TParameters> : IRequest<TParameters> where TParameters : class, IRequestParameters, new()
	{
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

		protected virtual HttpMethod HttpMethod => RequestState.RequestParameters.DefaultHttpMethod;

		[IgnoreDataMember]
		protected IRequest<TParameters> RequestState => this;

		[IgnoreDataMember]
		HttpMethod IRequest.HttpMethod => HttpMethod;

		[IgnoreDataMember]
		string IRequest.ContentType => ContentType;
		protected virtual string ContentType { get; } = null;

		private readonly TParameters _parameters;

		[IgnoreDataMember]
		TParameters IRequest<TParameters>.RequestParameters => _parameters;
		IRequestParameters IRequest.RequestParameters => _parameters;

		[IgnoreDataMember]
		RouteValues IRequest.RouteValues { get; } = new RouteValues();

		internal abstract ApiUrls ApiUrls { get;  }

		string IRequest.GetUrl(IConnectionSettingsValues settings) => ResolveUrl(RequestState.RouteValues, settings);

		protected virtual string ResolveUrl(RouteValues routeValues, IConnectionSettingsValues settings) => ApiUrls.Resolve(routeValues, settings);

		/// <summary>
		/// Allows a request implementation to set certain request parameter defaults, use sparingly!
		/// </summary>
		protected virtual void RequestDefaults(TParameters parameters) { }

		protected TOut Q<TOut>(string name) => RequestState.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) => RequestState.RequestParameters.SetQueryString(name, value);

		protected void SetAcceptHeader(string format)
		{
			if (RequestState.RequestParameters.RequestConfiguration == null)
				RequestState.RequestParameters.RequestConfiguration = new RequestConfiguration();

			RequestState.RequestParameters.RequestConfiguration.Accept = RequestState.RequestParameters.AcceptHeaderFromFormat(format);
		}
	}

	public abstract partial class PlainRequestBase<TParameters> : RequestBase<TParameters>
		where TParameters : class, IRequestParameters, new()
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

		protected TInterface Self => _descriptor;

		protected TDescriptor Assign<TValue>(TValue value, Action<TInterface, TValue> assign) => Fluent.Assign(_descriptor, value, assign);

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
		// ReSharper disable BaseObjectEqualsIsObjectEquals
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
		public override int GetHashCode() => base.GetHashCode();
		// ReSharper restore BaseObjectEqualsIsObjectEquals

		/// <summary>
		/// Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
	}
}
