using System.Collections.Generic;

namespace Elasticsearch.Net
{
	public class ErrorCause
	{
		public string Reason { get; set; }
		public string Type { get; set; }
		public ErrorCause CausedBy { get; set; }

		internal static ErrorCause CreateErrorCause(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var causedBy = new ErrorCause();
			causedBy.FillValues(dict);
			if (dict.TryGetValue("caused_by", out var innerCausedBy))
				causedBy.CausedBy = (ErrorCause)strategy.DeserializeObject(innerCausedBy, typeof(ErrorCause));

			return causedBy;
		}

		public override string ToString() => this.CausedBy == null
			? $"Type: {this.Type} Reason: \"{this.Reason}\""
			: $"Type: {this.Type} Reason: \"{this.Reason}\" CausedBy: \"{this.CausedBy}\"";
	}
}