// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Mapping;

public partial class Properties
{
	public bool TryGetProperty<T>(PropertyName propertyName, out T property) where T : PropertyBase
	{
		if (BackingDictionary.TryGetValue(propertyName, out var propertyBase) && propertyBase is T finalProperty)
		{
			property = finalProperty;
			return true;
		}

		property = default;
		return false;
	}
}
