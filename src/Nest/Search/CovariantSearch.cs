using System;

namespace Nest
{
	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
	}
}
