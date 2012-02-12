using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class FilterDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "exists")]
		internal ExistsFilter ExistsFilter { get; set; }

		[JsonProperty(PropertyName = "ids")]
		internal IdsFilter IdsFilter { get; set; }

		[JsonProperty(PropertyName = "limit")]
		internal LimitFilter LimitFilter { get; set; }

		[JsonProperty(PropertyName = "type")]
		internal TypeFilter TypeFilter { get; set; }

		[JsonProperty(PropertyName = "match_all")]
		internal MatchAllFilter MatchAllFilter { get; set; }

		public FilterDescriptor()
		{
			
		}

		public FilterDescriptor<T> Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.ExistsFilter = new ExistsFilter { Field = field };
			return this;
		}

		public FilterDescriptor<T> Ids(IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values };
			return this;
		}
		public FilterDescriptor<T> Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsFilter = new IdsFilter { Values = values, Type = new []{ type } };
			return this;
		}
		public FilterDescriptor<T> Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values, Type = types };
			return this;
		}

		public FilterDescriptor<T> Limit(int limit)
		{
			this.LimitFilter = new LimitFilter { Value = limit };
			return this;
		}
		public FilterDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this.TypeFilter = new TypeFilter { Value = type };
			return this;
		}
		public FilterDescriptor<T> MatchAll()
		{
			this.MatchAllFilter = new MatchAllFilter { };
			return this;
		}
	}
}
