using Elasticsearch.Net;
using Nest.Domain;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{

	[DescriptorFor("Mget")]
	public partial class MultiGetDescriptor : FixedIndexTypePathDescriptor<MultiGetDescriptor, MultiGetRequestParameters>
		, IPathInfo<MultiGetRequestParameters>
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

		public MultiGetDescriptor GetMany<K>(IEnumerable<long> ids, Func<SimpleGetDescriptor<K>, long, SimpleGetDescriptor<K>> getSelector=null) where K : class
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
		ElasticsearchPathInfo<MultiGetRequestParameters> IPathInfo<MultiGetRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = this.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST; // no data in GETS in the .net world
			return pathInfo;
		}
	}
}
