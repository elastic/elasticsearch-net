using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		int Took { get; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
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
			sb.AppendLine("# Invalid Bulk items:");
			foreach(var i in Items.Select((item, i) => new { item, i}).Where(i=>!i.item.IsValid))
				sb.AppendLine($"  operation[{i.i}]: {i.item}");
		}

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[JsonProperty("took")]
		public long TookAsLong { get;  internal set;}

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		[JsonIgnore]
		public int Took => TookAsLong > int.MaxValue ? int.MaxValue : (int)TookAsLong;

		[JsonProperty("errors")]
		public bool Errors { get; internal set; }

		[JsonProperty("items")]
		public IEnumerable<BulkResponseItemBase> Items { get; internal set; }

		[JsonIgnore]
		public IEnumerable<BulkResponseItemBase> ItemsWithErrors =>
			!this.Items.HasAny() ? Enumerable.Empty<BulkResponseItemBase>() : this.Items.Where(i => !i.IsValid);
	}
}
