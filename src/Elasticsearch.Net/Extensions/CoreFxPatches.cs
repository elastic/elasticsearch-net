using System;

#if DNXCORE50
namespace System
{
	public enum TypeCode
	{
		Empty = 0,			// Null reference
		Object = 1,			// Instance that isn't a value
		DBNull = 2,			// Database null value
		Boolean = 3,		// Boolean
		Char = 4,			// Unicode character
		SByte = 5,			// Signed 8-bit integer
		Byte = 6,			// Unsigned 8-bit integer
		Int16 = 7,			// Signed 16-bit integer
		UInt16 = 8,			// Unsigned 16-bit integer
		Int32 = 9,			// Signed 32-bit integer
		UInt32 = 10,		// Unsigned 32-bit integer
		Int64 = 11,			// Signed 64-bit integer
		UInt64 = 12,		// Unsigned 64-bit integer
		Single = 13,		// IEEE 32-bit float
		Double = 14,		// IEEE 64-bit double
		Decimal = 15,		// Decimal
		DateTime = 16,		// DateTime
		String = 18,		// Unicode character string
	}
}
#endif
