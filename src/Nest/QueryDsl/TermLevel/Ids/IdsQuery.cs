using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IdsQueryDescriptor>))]
	public interface IIdsQuery : IQuery
	{
		[JsonProperty(PropertyName = "type")]
		IEnumerable<string> Type { get; set; }

		[JsonProperty(PropertyName = "values")]
		IEnumerable<string> Values { get; set; }
	}
	
	public class IdsQuery : QueryBase, IIdsQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<string> Type { get; set; }
		public IEnumerable<string> Values { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Ids = this;
		internal static bool IsConditionless(IIdsQuery q) => !q.Values.HasAny() || q.Values.All(v => v.IsNullOrEmpty());
	}

	public class IdsQueryDescriptor 
		: QueryDescriptorBase<IdsQueryDescriptor, IIdsQuery>
		, IIdsQuery
	{
		bool IQuery.Conditionless => IdsQuery.IsConditionless(this);
		IEnumerable<string> IIdsQuery.Values { get; set; }
		IEnumerable<string> IIdsQuery.Type { get; set; }

		public IdsQueryDescriptor Type(params string[] types) => Assign(a => a.Type = types);

		public IdsQueryDescriptor Type(IEnumerable<string> values) => Type(values.ToArray());

		public IdsQueryDescriptor Values(params long[] values) =>
			Assign(a => a.Values = values.Select(v => v.ToString(CultureInfo.InvariantCulture)).ToArray());

		public IdsQueryDescriptor Values(IEnumerable<long> values) =>
			Values(values.Select(v => v.ToString(CultureInfo.InvariantCulture)).ToArray());

		public IdsQueryDescriptor Values(params string[] values) => Assign(a => a.Values = values);

		public IdsQueryDescriptor Values(IEnumerable<string> values) => Values(values.ToArray());
	}
}
