using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<IdsQueryDescriptor>))]
	public interface IIdsQuery : IQuery
	{
		[JsonProperty(PropertyName = "type")]
		IEnumerable<string> Type { get; set; }

		[JsonProperty(PropertyName = "values")]
		IEnumerable<string> Values { get; set; }
	}
	
	public class IdsQuery : PlainQuery, IIdsQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Ids = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<string> Type { get; set; }
		public IEnumerable<string> Values { get; set; }
	}

	[Obsolete("Scheduled to be renamed in 2.0")]
	public class IdsQueryProperDescriptor : IIdsQuery
	{
		private IIdsQuery Self { get { return this; }}

		IEnumerable<string> IIdsQuery.Values { get; set; }
		IEnumerable<string> IIdsQuery.Type { get; set; }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return !Self.Values.HasAny() || Self.Values.All(s=>s.IsNullOrEmpty());
			}
		}

		public IdsQueryProperDescriptor Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public IdsQueryProperDescriptor Type(params string[] types)
		{
			Self.Type = types;
			return this;
		}

		public IdsQueryProperDescriptor Type(IEnumerable<string> values)
		{
			return this.Type(values.ToArray());
		}

		public IdsQueryProperDescriptor Values(params long[] values)
		{
			if (values == null) return this;
			return this.Values(values.Select(v=>v.ToString(CultureInfo.InvariantCulture)).ToArray());
		}

		public IdsQueryProperDescriptor Values(IEnumerable<long> values)
		{
			if (values == null) return this;
			return this.Values(values.Select(v=>v.ToString(CultureInfo.InvariantCulture)).ToArray());
		}
	
		public IdsQueryProperDescriptor Values(params string[] values)
		{
			Self.Values = values;
			return this;
		}

		public IdsQueryProperDescriptor Values(IEnumerable<string> values)
		{
			if (values == null) return this;
			return this.Values(values.ToArray());
		}
	}

	
	[Obsolete("Scheduled to be removed in 2.0")]
	public class IdsQueryDescriptor : IIdsQuery
	{
		[JsonProperty(PropertyName = "_name")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "type")]
		public IEnumerable<string> Type { get; set; }
		[JsonProperty(PropertyName = "values")]
		public IEnumerable<string> Values { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !this.Values.HasAny() || this.Values.All(s=>s.IsNullOrEmpty());
			}
		}
	}
}
