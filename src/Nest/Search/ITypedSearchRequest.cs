using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary> Signals the type to deserialize hits into </summary>
	[InterfaceDataContract]
	public interface ISearchTypeInformation
	{
		Type ClrType { get; }
	}
}
