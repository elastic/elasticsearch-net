using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		bool Errors { get; }
		IReadOnlyCollection<BulkResponseItemBase> Items { get; }
		IEnumerable<BulkResponseItemBase> ItemsWithErrors { get; }
		long Took { get; }
	}

	[JsonObject]
	public class BulkResponse : ResponseBase, IBulkResponse
	{
		[JsonProperty("errors")]
		public bool Errors { get; internal set; }

		public override bool IsValid => base.IsValid && !Errors && !ItemsWithErrors.HasAny();

		[JsonProperty("items")]
		public IReadOnlyCollection<BulkResponseItemBase> Items { get; internal set; } = EmptyReadOnly<BulkResponseItemBase>.Collection;

		[JsonIgnore]
		public IEnumerable<BulkResponseItemBase> ItemsWithErrors => !Items.HasAny()
			? Enumerable.Empty<BulkResponseItemBase>()
			: Items.Where(i => !i.IsValid);

		[JsonProperty("took")]
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
