using System.Collections.Generic;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Holds meta data about a client request.
	/// </summary>
	public sealed class RequestMetaData
	{
		/// <summary>
		/// Reserved key for a meta data entry which identifies the helper which produced the request.
		/// </summary>
		public const string HelperKey = "helper";

		private Dictionary<string, string> _metaDataItems;

		public bool TryAddMetaData (string key, string value)
		{
			_metaDataItems ??= new Dictionary<string, string>();

			if (_metaDataItems.ContainsKey(key))
				return false;

			_metaDataItems.Add(key, value);
			return true;
		}

		public IReadOnlyDictionary<string, string> Items => _metaDataItems ?? EmptyReadOnly<string, string>.Dictionary;
	}
}
