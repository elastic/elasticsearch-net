using System;

namespace Nest
{
	/// <summary>
	/// An exception when handling <see cref="IGeoShape"/> in Well-Known Text format
	/// </summary>
	public class GeoWKTException : Exception
	{
		public GeoWKTException(string message)
			: base(message)
		{
		}

		public GeoWKTException(string message, int lineNumber, int position)
			: base($"{message} at line {lineNumber}, position {position}")
		{
		}
	}
}
