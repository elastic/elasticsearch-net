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
		internal const string HelperKey = "helper";

		private Dictionary<string, string> _metaDataItems;

		internal bool TryAddMetaData (string key, string value)
		{
			if (_metaDataItems is null)
				_metaDataItems = new Dictionary<string, string>();

#if NETSTANDARD2_1
			return _metaDataItems.TryAdd(key, value);
#else
			if (_metaDataItems.ContainsKey(key))
				return false;

			_metaDataItems.Add(key, value);
			return true;
#endif
		}		

		public IReadOnlyDictionary<string, string> Items => _metaDataItems is null ? EmptyReadOnly<string, string>.Dictionary : _metaDataItems;
	}
}
