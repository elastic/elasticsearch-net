using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
	}
}
