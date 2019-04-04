using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IdsQueryDescriptor>))]
	public interface IIdsQuery : IQuery
	{
		[JsonProperty("type")]
		Types Types { get; set; }

		[JsonProperty("values")]
		IEnumerable<Id> Values { get; set; }
	}

	public class IdsQuery : QueryBase, IIdsQuery
	{
		public Types Types { get; set; }
		public IEnumerable<Id> Values { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Ids = this;

		internal static bool IsConditionless(IIdsQuery q) => !q.Values.HasAny();
	}

	public class IdsQueryDescriptor
		: QueryDescriptorBase<IdsQueryDescriptor, IIdsQuery>
			, IIdsQuery
	{
		protected override bool Conditionless => IdsQuery.IsConditionless(this);
		Types IIdsQuery.Types { get; set; }
		IEnumerable<Id> IIdsQuery.Values { get; set; }

		public IdsQueryDescriptor Types(params TypeName[] types) => Assign(types, (a, v) => a.Types = v);

		public IdsQueryDescriptor Types(IEnumerable<TypeName> values) => Types(values?.ToArray());

		public IdsQueryDescriptor Types(Types types) => Assign(types, (a, v) => a.Types = v);

		public IdsQueryDescriptor Values(params Id[] values) => Assign(values, (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<Id> values) => Values(values?.ToArray());

		public IdsQueryDescriptor Values(params string[] values) => Assign(values?.Select(v => (Id)v), (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<string> values) => Values(values.ToArray());

		public IdsQueryDescriptor Values(params long[] values) => Assign(values?.Select(v => (Id)v), (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<long> values) => Values(values.ToArray());

		public IdsQueryDescriptor Values(params Guid[] values) => Assign(values?.Select(v => (Id)v), (a, v) => a.Values = v);

		public IdsQueryDescriptor Values(IEnumerable<Guid> values) => Values(values.ToArray());
	}
}
