using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IdField>))]
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

	public class IdFieldDescriptor 
		: DescriptorBase<IdFieldDescriptor, IIdField>, IIdField
	{
		string IIdField.Path { get; set; }
		string IIdField.Index { get; set; }
		bool? IIdField.Store { get; set; }

		public IdFieldDescriptor Path(string path) => Assign(a => a.Path = path);

		public IdFieldDescriptor Index(string index) => Assign(a => a.Index = index);

		public IdFieldDescriptor Store(bool store = true) => Assign(a => a.Store = store);
	}
}