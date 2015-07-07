using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class IPMapping : IElasticType
	{
		public PropertyName Name { get; set; }

		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("type")]
		public virtual TypeName Type { get { return new TypeName { Name = "ip" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

		[JsonProperty("index"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Index { get; set; }

		[JsonProperty("precision_step")]
		public int? PrecisionStep { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public string NullValue { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }
	}
	public class IPMappingDescriptor<T>
	{
		internal IPMapping _Mapping = new IPMapping();

		public IPMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public IPMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public IPMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public IPMappingDescriptor<T> NoIndex()
		{
			this._Mapping.Index = false;
			return this;
		}
		public IPMappingDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		
		public IPMappingDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public IPMappingDescriptor<T> NullValue(string nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}

		public IPMappingDescriptor<T> PrecisionStep(int precisionStep)
		{
			this._Mapping.PrecisionStep = precisionStep;
			return this;
		}

		public IPMappingDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		

	}
}