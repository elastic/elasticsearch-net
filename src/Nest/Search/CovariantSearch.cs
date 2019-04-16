using System;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
	}
}
