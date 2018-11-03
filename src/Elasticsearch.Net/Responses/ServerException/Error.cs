using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Elasticsearch.Net
{
	public class Error : ErrorCause
	{
		private static readonly IReadOnlyDictionary<string, string> DefaultHeaders =
			new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(0));

		public IReadOnlyDictionary<string, string> Headers { get; set; } = DefaultHeaders;

		public IReadOnlyCollection<ErrorCause> RootCause { get; set; }

		internal static Error CreateError(IDictionary<string, object> dict, IJsonSerializerStrategy strategy)
		{
			var error = new Error();
			error.FillValues(dict);

			if (dict.TryGetValue("caused_by", out var causedBy))
				error.CausedBy = (ErrorCause)strategy.DeserializeObject(causedBy, typeof(ErrorCause));

			if (dict.TryGetValue("headers", out var headers))
			{
				var d = (IDictionary<string, string>)strategy.DeserializeObject(headers, typeof(IDictionary<string, string>));
				if (d != null) error.Headers = new ReadOnlyDictionary<string, string>(d);
			}

			error.Metadata = ErrorCauseMetadata.CreateCauseMetadata(dict, strategy);

			return ReadRootCause(dict, strategy, error);
		}

		private static Error ReadRootCause(IDictionary<string, object> dict, IJsonSerializerStrategy strategy, Error error)
		{
			if (!dict.TryGetValue("root_cause", out var rootCause)) return error;

			if (!(rootCause is object[] os)) return error;

			error.RootCause = os.Select(o => (ErrorCause)strategy.DeserializeObject(o, typeof(ErrorCause))).ToList().AsReadOnly();
			return error;
		}
	}
}
