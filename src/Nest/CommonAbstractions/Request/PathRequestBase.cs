using Elasticsearch.Net;
using Elasticsearch.Net.Connection.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Nest
{
	public abstract class PathRequestBase<TParameters> : RequestBase<TParameters>
		where TParameters : IRequestParameters, new()
	{
        // TODO: Placeholder for now so stuff compiles.  Need to remove this ctor eventually.
        public PathRequestBase()
        {
        }

        public PathRequestBase(Func<ElasticsearchPathInfo<TParameters>, ElasticsearchPathInfo<TParameters>> pathSelector)
        {
            this.PathInfo = pathSelector(new ElasticsearchPathInfo<TParameters>());
        }

		protected TOut Q<TOut>(string name) =>
			this.Request.RequestParameters.GetQueryStringValue<TOut>(name);

		protected void Q(string name, object value) =>
			this.Request.RequestParameters.AddQueryStringValue(name, value);
	}

    public abstract class PathDescriptorBase<TDescriptor, TParameters> : RequestBase<TParameters>, IDescriptor
		where TDescriptor : PathDescriptorBase<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
        // TODO: Placeholder for now so stuff compiles.  Need to remove this ctor eventually.
        public PathDescriptorBase()
        {
        }

        public PathDescriptorBase(Func<ElasticsearchPathInfo<TParameters>, ElasticsearchPathInfo<TParameters>> pathSelector)
        {
            this.PathInfo = pathSelector(new ElasticsearchPathInfo<TParameters>());
        }

		protected TDescriptor _requestParams(Action<TParameters> assigner)
		{
			assigner?.Invoke(this.Request.RequestParameters);
			return (TDescriptor)this;
		}

		/// <summary>
		/// Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
		/// </summary>
		public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
		{
			configurationSelector.ThrowIfNull("configurationSelector");
			this.Request.RequestConfiguration = configurationSelector(new RequestConfigurationDescriptor());
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