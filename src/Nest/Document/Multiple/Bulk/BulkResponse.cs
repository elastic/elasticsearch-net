using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		int Took { get; }
		bool Errors { get; }
		IEnumerable<BulkResponseItemBase> Items { get; }
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
		public int Took { get; internal set; }

		[JsonProperty("errors")]
		public bool Errors { get; internal set; }

		[JsonProperty("items")]
		public IEnumerable<BulkResponseItemBase> Items { get; internal set; }

		[JsonIgnore]
		public IEnumerable<BulkResponseItemBase> ItemsWithErrors
		{
			get
			{
				return !this.Items.HasAny() ? Enumerable.Empty<BulkResponseItemBase>() : this.Items.Where(i => !i.IsValid);
			}
		}
	}
}
