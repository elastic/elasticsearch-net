using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net
{
	public class Error : ErrorCause
	{
		public string Index { get; set; }
		public string ResourceId { get; set; }
		public string ResourceType { get; set; }
		public IReadOnlyCollection<ErrorCause> RootCause { get; set; }

		internal static Error CreateError(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var error = new Error();
			error.FillValues(dict);

			if (dict.TryGetValue("caused_by", out var causedBy))
				error.CausedBy = (ErrorCause) strategy.DeserializeObject(causedBy, typeof(ErrorCause));

			if (!dict.TryGetValue("root_cause", out var rootCause)) return error;

			if (!(rootCause is object[] os)) return error;
			error.RootCause = os.Select(o => (ErrorCause)strategy.DeserializeObject(o, typeof(ErrorCause))).ToList().AsReadOnly();
			return error;
		}
	}
}