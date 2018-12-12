using System;
using System.Runtime.Serialization;

namespace Nest
{
	internal class ReadAsAttribute : Attribute
	{
		public ReadAsAttribute(Type readAs) => Type = readAs;

		public Type Type { get; }
	}

	internal class WriteAsAttribute : Attribute
	{
		public WriteAsAttribute(Type readAs) => Type = readAs;

		public Type Type { get; }
	}
}
