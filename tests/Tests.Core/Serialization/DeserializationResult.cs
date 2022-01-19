using System;

namespace Tests.Core.Serialization
{
	public class DeserializationResult<T> : SerializationResult
	{
		public T Result { get; set; }

		public override string ToString()
		{
			var s = $"Deserialization has result: {Result != null}";
			s += Environment.NewLine;
			s += base.ToString();
			return s;
		}
	}
}
