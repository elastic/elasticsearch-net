using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		long Took { get; }
		bool Errors { get; }
		IReadOnlyCollection<IBulkResponseItem> Items { get; }
		IEnumerable<IBulkResponseItem> ItemsWithErrors { get; }
	}

	[JsonObject]
	public class BulkResponse : ResponseBase, IBulkResponse
	{
		public override bool IsValid => base.IsValid && !this.Errors && !this.ItemsWithErrors.HasAny();
		protected override void DebugIsValid(StringBuilder sb)
		{
			if (this.Items == null) return;
			sb.AppendLine($"# Invalid Bulk items:");
			foreach(var i in Items.Select((item, i) => new { item, i}).Where(i=>!i.item.IsValid))
				sb.AppendLine($"  operation[{i.i}]: {i.item}");
		}

		[JsonProperty("took")]
		public long Took { get; internal set; }

		[JsonProperty("errors")]
		public bool Errors { get; internal set; }

		[JsonProperty("items")]
		public IReadOnlyCollection<IBulkResponseItem> Items { get; internal set; } = EmptyReadOnly<IBulkResponseItem>.Collection;

		[JsonIgnore]
		public IEnumerable<IBulkResponseItem> ItemsWithErrors => !this.Items.HasAny()
			? Enumerable.Empty<IBulkResponseItem>()
			: this.Items.Where(i => !i.IsValid);
	}
}
