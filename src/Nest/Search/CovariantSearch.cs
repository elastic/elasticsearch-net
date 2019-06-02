using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	// TODO 7.x is this still needed?
	[InterfaceDataContract]
	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
	}
}
