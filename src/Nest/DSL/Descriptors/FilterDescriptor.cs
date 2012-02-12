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

		public FilterDescriptor()
		{
			
		}

		public FilterDescriptor<T> Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.ExistsFilter = new ExistsFilter { Field = field };
			return this;
		}

	}
}
