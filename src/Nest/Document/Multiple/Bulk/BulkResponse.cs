using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IBulkResponse : IResponse
	{
		int Took { get; }
		bool Errors { get; }
		IEnumerable<BulkResponseItem> Items { get; }
		IEnumerable<BulkResponseItem> ItemsWithErrors { get; }
	}

	[JsonObject]
	public class BulkResponse : BaseResponse, IBulkResponse
	{
		private bool _isValid;
		public override bool IsValid
		{
			get
			{
				return this._isValid && !this.Errors && !this.ItemsWithErrors.HasAny();
			}
			internal set
			{
				this._isValid = value;
			}
		}

		[JsonProperty("took")]
		public int Took { get; internal set; }

		[JsonProperty("errors")]
		public bool Errors { get; internal set; }

		[JsonProperty("items")]
		public IEnumerable<BulkResponseItem> Items { get; internal set; }

		[JsonIgnore]
		public IEnumerable<BulkResponseItem> ItemsWithErrors
		{
			get
			{
				return !this.Items.HasAny() ? Enumerable.Empty<BulkResponseItem>() : this.Items.Where(i => !i.IsValid);
			}
		}
	}
}
