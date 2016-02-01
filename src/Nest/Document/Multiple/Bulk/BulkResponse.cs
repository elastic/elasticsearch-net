using System.Collections.Generic;
using System.Linq;
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
