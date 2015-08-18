using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IdFieldMapping>))]
	public interface IIdFieldMapping : ISpecialField
	{
		[JsonProperty("path")]
		string Path { get; set; }

		[JsonProperty("index")]
		string Index { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }
	}

	public class IdFieldMapping : IIdFieldMapping
	{
		public string Path { get; set; }

		public string Index { get; set;}

		public bool? Store { get; set;}
	}


	public class IdFieldMappingDescriptor : IIdFieldMapping
	{
		private IIdFieldMapping Self => this;

		string IIdFieldMapping.Path { get; set; }

		string IIdFieldMapping.Index { get; set; }

		bool? IIdFieldMapping.Store { get; set; }

		public IdFieldMappingDescriptor Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public IdFieldMappingDescriptor Index(string index)
		{
			Self.Index = index;
			return this;
		}
		public IdFieldMappingDescriptor Store(bool stored = true)
		{
			Self.Store = stored;
			return this;
		}
	}
}