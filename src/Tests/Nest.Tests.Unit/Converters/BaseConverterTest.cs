using System.IO;

namespace Nest.Tests.Unit.Converters
{
	public abstract class BaseConverterTest : BaseJsonTests
	{
		protected T SerializeAndDeserialize<T>(T expected)
		{
			var json = _client.Serializer.Serialize(expected);

			using (var ms = new MemoryStream(json))
			{
				return _client.Serializer.Deserialize<T>(ms);
			}
		}

		protected class ConverterTestObject
		{
			public string Name { get; set; }
			public IFuzziness Fuzziness { get; set; }
		}
	}
}