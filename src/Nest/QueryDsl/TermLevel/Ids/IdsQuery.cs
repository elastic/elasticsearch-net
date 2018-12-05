using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IdsQueryDescriptor))]
	public interface IIdsQuery : IQuery
	{
		[DataMember(Name = "values")]
		IEnumerable<Id> Values { get; set; }
	}

	public class IdsQuery : QueryBase, IIdsQuery
	{
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
		IEnumerable<Id> IIdsQuery.Values { get; set; }

		public IdsQueryDescriptor Values(params Id[] values) => Assign(a => a.Values = values);

		public IdsQueryDescriptor Values(IEnumerable<Id> values) => Values(values?.ToArray());

		public IdsQueryDescriptor Values(params string[] values) => Assign(a => a.Values = values?.Select(v => (Id)v));

		public IdsQueryDescriptor Values(IEnumerable<string> values) => Values(values.ToArray());

		public IdsQueryDescriptor Values(params long[] values) => Assign(a => a.Values = values?.Select(v => (Id)v));

		public IdsQueryDescriptor Values(IEnumerable<long> values) => Values(values.ToArray());

		public IdsQueryDescriptor Values(params Guid[] values) => Assign(a => a.Values = values?.Select(v => (Id)v));

		public IdsQueryDescriptor Values(IEnumerable<Guid> values) => Values(values.ToArray());
	}
}
