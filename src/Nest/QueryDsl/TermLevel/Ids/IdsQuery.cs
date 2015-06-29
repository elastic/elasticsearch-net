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
		public string Name { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<string> Type { get; set; }
		public IEnumerable<string> Values { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Ids = this;
		internal static bool IsConditionless(IIdsQuery q) => !q.Values.HasAny() || q.Values.All(v => v.IsNullOrEmpty());
	}

	public class IdsQueryDescriptor : IIdsQuery
	{
		private IIdsQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => IdsQuery.IsConditionless(this);
		IEnumerable<string> IIdsQuery.Values { get; set; }
		IEnumerable<string> IIdsQuery.Type { get; set; }

		public IdsQueryDescriptor Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public IdsQueryDescriptor Type(params string[] types)
		{
			Self.Type = types;
			return this;
		}

		public IdsQueryDescriptor Type(IEnumerable<string> values)
		{
			return this.Type(values.ToArray());
		}

		public IdsQueryDescriptor Values(params long[] values)
		{
			if (values == null) return this;
			return this.Values(values.Select(v=>v.ToString(CultureInfo.InvariantCulture)).ToArray());
		}

		public IdsQueryDescriptor Values(IEnumerable<long> values)
		{
			if (values == null) return this;
			return this.Values(values.Select(v=>v.ToString(CultureInfo.InvariantCulture)).ToArray());
		}
	
		public IdsQueryDescriptor Values(params string[] values)
		{
			Self.Values = values;
			return this;
		}

		public IdsQueryDescriptor Values(IEnumerable<string> values)
		{
			if (values == null) return this;
			return this.Values(values.ToArray());
		}
	}
}
