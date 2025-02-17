// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public partial class SourceConfig
{
	public bool HasBoolValue => Tag == UnionTag.T1;

	public bool HasSourceFilterValue => Tag == UnionTag.T2;

	public bool TryGetBool([NotNullWhen(returnValue: true)] out bool? value)
	{
		if (Tag is UnionTag.T1)
		{
			value = Value1;
			return true;
		}

		value = null;
		return false;
	}

	public bool TryGetSourceFilter([NotNullWhen(returnValue: true)] out SourceFilter? value)
	{
		if (Tag is UnionTag.T2)
		{
			value = Value2;
			return true;
		}

		value = null;
		return false;
	}
}
