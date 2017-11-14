using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("errors")]
		bool Errors { get; }

		[JsonProperty("items")]
		IReadOnlyCollection<IBulkResponseItem> Items { get; }

		IEnumerable<IBulkResponseItem> ItemsWithErrors { get; }
	}

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

		public long Took { get; internal set; }

		public bool Errors { get; internal set; }

		public IReadOnlyCollection<IBulkResponseItem> Items { get; internal set; } = EmptyReadOnly<IBulkResponseItem>.Collection;

		public IEnumerable<IBulkResponseItem> ItemsWithErrors => !this.Items.HasAny()
			? Enumerable.Empty<IBulkResponseItem>()
			: this.Items.Where(i => !i.IsValid);
	}
}
