using System;
using System.Linq;

namespace Nest
{
	public interface ICovariantSearchRequest
	{
		Type ClrType { get; }
	}
}
