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
		private readonly IConnectionSettingsValues _settings;

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

		public MultiGetDescriptor(IConnectionSettingsValues settings)
		{
			_settings = settings;
		}

		public MultiGetDescriptor Get<K>(Func<SimpleGetDescriptor<K>, SimpleGetDescriptor<K>> getSelector) where K : class
		{
			getSelector.ThrowIfNull("getSelector");

			var descriptor = getSelector(new SimpleGetDescriptor<K>());

			this._GetOperations.Add(descriptor);
			return this;

		}

		public MultiGetDescriptor GetMany<K>(IEnumerable<int> ids, Func<SimpleGetDescriptor<K>, int, SimpleGetDescriptor<K>> getSelector=null) where K : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new SimpleGetDescriptor<K>().Id(id), id)))
				this._GetOperations.Add(sg);
			return this;

		}
		public MultiGetDescriptor GetMany<K>(IEnumerable<string> ids, Func<SimpleGetDescriptor<K>, string, SimpleGetDescriptor<K>> getSelector=null) where K : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new SimpleGetDescriptor<K>().Id(id), id)))
				this._GetOperations.Add(sg);
			return this;

		}
		ElasticsearchPathInfo<MultiGetQueryString> IPathInfo<MultiGetQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = this.ToPathInfo<MultiGetQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST; // no data in GETS in the .net world
			return pathInfo;
		}
	}
}
