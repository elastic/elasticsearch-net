using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<IdField>))]
	public interface IIdField : ISpecialField
	{
		[JsonProperty("path")]
		string Path { get; set; }

		[JsonProperty("index")]
		string Index { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }
	}

	public class IdField : IIdField
	{
		public string Path { get; set; }

		public string Index { get; set;}

		public bool? Store { get; set;}
	}

	public class IdFieldDescriptor : IIdField
	{
		private IIdField Self => this;

		string IIdField.Path { get; set; }

		string IIdField.Index { get; set; }

		bool? IIdField.Store { get; set; }

		public IdFieldDescriptor Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public IdFieldDescriptor Index(string index)
		{
			Self.Index = index;
			return this;
		}
		public IdFieldDescriptor Store(bool stored = true)
		{
			Self.Store = stored;
			return this;
		}
	}
}