using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IBulkResponse : IResponse
	{
		long Took { get; }
		bool Errors { get; }
		IReadOnlyCollection<BulkResponseItemBase> Items { get; }
		IEnumerable<BulkResponseItemBase> ItemsWithErrors { get; }
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
		public IReadOnlyCollection<BulkResponseItemBase> Items { get; internal set; } = EmptyReadOnly<BulkResponseItemBase>.Collection;

		[JsonIgnore]
		public IEnumerable<BulkResponseItemBase> ItemsWithErrors => !this.Items.HasAny()
			? Enumerable.Empty<BulkResponseItemBase>()
			: this.Items.Where(i => !i.IsValid);
	}
}
