using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{

	[DescriptorFor("Mget")]
	public partial class MultiGetDescriptor : FixedIndexTypePathDescriptor<MultiGetDescriptor, MultiGetQueryString>
		, IPathInfo<MultiGetQueryString>
	{
		internal readonly IList<ISimpleGetDescriptor> _GetOperations = new List<ISimpleGetDescriptor>();
		private readonly IConnectionSettings _settings;

		[JsonProperty("docs")]
		internal IEnumerable<MultiGetDoc> Docs
		{
			get
			{
				var inferrer = new ElasticInferrer(this._settings);
				return this._GetOperations.Select(g => new MultiGetDoc
				{
					Index = g._Index == null ? inferrer.IndexName(g._ClrType) : inferrer.IndexName(g._Index),
					Type = g._Type == null ? inferrer.TypeName(g._ClrType) : inferrer.TypeName(g._Type),
					Fields = g._Fields,
					Routing = g._Routing,
					Id = g._Id

				});
			}
		} 

		public MultiGetDescriptor(IConnectionSettings settings)
		{
			_settings = settings;
		}

		public MultiGetDescriptor Get<K>(Action<SimpleGetDescriptor<K>> getSelector) where K : class
		{
			getSelector.ThrowIfNull("getSelector");

			var descriptor = new SimpleGetDescriptor<K>();
			getSelector(descriptor);

			this._GetOperations.Add(descriptor);
			return this;

		}

		ElasticSearchPathInfo<MultiGetQueryString> IPathInfo<MultiGetQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = this.ToPathInfo<MultiGetQueryString>(settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST; // no data in GETS in the .net world
			return pathInfo;
		}
	}
}
