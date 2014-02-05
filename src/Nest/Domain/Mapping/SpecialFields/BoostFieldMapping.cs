using System;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class BoostFieldMapping
	{
		[JsonProperty("name")]
		public PropertyNameMarker Name { get; internal set; }

		[JsonProperty("null_value")]
		public double NullValue { get; internal set; }
	}


	public class BoostFieldMapping<T> : BoostFieldMapping
	{
		public BoostFieldMapping<T> SetNullValue(double boost)
		{
			this.NullValue = boost;
			return this;
		}
		public BoostFieldMapping<T> SetName(string path)
		{
			this.Name = path;
			return this;
		}
		public BoostFieldMapping<T> SetName(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			this.Name = objectPath;
			return this;
		}
	}
}