using System;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
	}
}
