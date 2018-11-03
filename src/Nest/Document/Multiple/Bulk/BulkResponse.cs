using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		bool Errors { get; }
		IReadOnlyCollection<IBulkResponseItem> Items { get; }
		IEnumerable<IBulkResponseItem> ItemsWithErrors { get; }
		long Took { get; }
	}

	[JsonObject]
	public class BulkResponse : ResponseBase, IBulkResponse
	{
		[JsonProperty("errors")]
		public bool Errors { get; internal set; }

		public override bool IsValid => base.IsValid && !Errors && !ItemsWithErrors.HasAny();

		[JsonProperty("items")]
		public IReadOnlyCollection<IBulkResponseItem> Items { get; internal set; } = EmptyReadOnly<IBulkResponseItem>.Collection;

		[JsonIgnore]
		public IEnumerable<IBulkResponseItem> ItemsWithErrors => !Items.HasAny()
			? Enumerable.Empty<IBulkResponseItem>()
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
