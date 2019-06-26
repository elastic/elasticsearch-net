using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class BulkResponse : ResponseBase
	{
		[DataMember(Name ="errors")]
		public bool Errors { get; internal set; }

		public override bool IsValid => base.IsValid && !Errors && !ItemsWithErrors.HasAny();

		[DataMember(Name ="items")]
		public IReadOnlyList<BulkResponseItemBase> Items { get; internal set; } = EmptyReadOnly<BulkResponseItemBase>.List;

		[IgnoreDataMember]
		public IEnumerable<BulkResponseItemBase> ItemsWithErrors => !Items.HasAny()
			? Enumerable.Empty<BulkResponseItemBase>()
			: Items.Where(i => !i.IsValid);

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		protected override void DebugIsValid(StringBuilder sb)
		{
			if (Items == null) return;

			sb.AppendLine($"# Invalid Bulk items:");
			foreach (var i in Items.Select((item, i) => new { item, i }).Where(i => !i.item.IsValid))
				sb.AppendLine($"  operation[{i.i}]: {i.item}");
		}
	}
}
