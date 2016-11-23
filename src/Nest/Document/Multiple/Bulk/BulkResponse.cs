using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		[Obsolete(@"Took field is an Int but the value in the response can exced the max value for Int.
					If you use this field instead of TookAsLong the value can wrap around if it is too big.")]
		int Took { get; }
		long TookAsLong { get; }
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
		public long TookAsLong { get;  internal set;}

		[Obsolete(@"Took field is an Int but the value in the response can exced the max value for Int.
					If you use this field instead of TookAsLong the value can wrap around if it is too big.")]
		public int Took
		{
			get
			{
				return unchecked((int)TookAsLong);
			}
			internal set
			{
				TookAsLong = value;
			}
		}

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
