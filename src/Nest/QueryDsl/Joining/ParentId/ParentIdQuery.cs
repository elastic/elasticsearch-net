using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The parent_id query can be used to find child documents which belong to a particular parent.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ParentIdQuery>))]
	public interface IParentIdQuery : IQuery
	{
		/// <summary>
		/// The child type. This must be a type with _parent field.
	    /// </summary>
		[JsonProperty("type")]
		RelationName Type { get; set; }

		/// <summary>
		/// The id of the parent document to get children for.
		/// </summary>
		[JsonProperty("id")]
		Id Id { get; set; }

		/// <summary>
		/// When set to true this will ignore an unmapped type and will not match any documents for
		/// this query. This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }
	}

	public class ParentIdQuery : QueryBase, IParentIdQuery
	{
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.ParentId = this;

		internal static bool IsConditionless(IParentIdQuery q) => q.Type.IsConditionless() || q.Id.IsConditionless();

		public RelationName Type { get; set; }

		public Id Id { get; set; }

		public bool? IgnoreUnmapped { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ParentIdQueryDescriptor<T>
	: QueryDescriptorBase<ParentIdQueryDescriptor<T>, IParentIdQuery>
	, IParentIdQuery where T : class
	{
		protected override bool Conditionless => ParentIdQuery.IsConditionless(this);

		RelationName IParentIdQuery.Type { get; set; }
		Id IParentIdQuery.Id { get; set; }
		bool? IParentIdQuery.IgnoreUnmapped { get; set; }

		public ParentIdQueryDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public ParentIdQueryDescriptor<T> Type(RelationName type) => Assign(a => a.Type = type);

		public ParentIdQueryDescriptor<T> Type<TChild>() => Assign(a => a.Type = typeof(TChild));

		public ParentIdQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(a => a.IgnoreUnmapped = ignoreUnmapped);
	}
}
