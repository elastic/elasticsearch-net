using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	public interface IIpType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class IpType : ElasticType, IIpType
	{
		public IpType() : base("ip") { }

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
	public class IpTypeDescriptor<T>
	{
		internal IpType _Mapping = new IpType();

		public IpTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public IpTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public IpTypeDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}
		public IpTypeDescriptor<T> NoIndex()
		{
			this._Mapping.Index = false;
			return this;
		}
		public IpTypeDescriptor<T> Store(bool store = true)
		{
			this._Mapping.Store = store;
			return this;
		}

		
		public IpTypeDescriptor<T> Boost(double boost)
		{
			this._Mapping.Boost = boost;
			return this;
		}
		public IpTypeDescriptor<T> NullValue(string nullValue)
		{
			this._Mapping.NullValue = nullValue;
			return this;
		}

		public IpTypeDescriptor<T> PrecisionStep(int precisionStep)
		{
			this._Mapping.PrecisionStep = precisionStep;
			return this;
		}

		public IpTypeDescriptor<T> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		

	}
}