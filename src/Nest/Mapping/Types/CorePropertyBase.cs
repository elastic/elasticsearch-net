using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface ICoreProperty : IProperty
	{
		[JsonProperty("store")]
		bool? Store { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IProperties Fields { get; set; }

		[JsonProperty("similarity")]
		Union<SimilarityOption, string> Similarity { get; set; }

		[JsonProperty("copy_to")]
		Fields CopyTo { get; set; }
	}

	public abstract class CorePropertyBase : PropertyBase, ICoreProperty
	{
		protected CorePropertyBase(TypeName typeName) : base(typeName)
		{
		}

		public Fields CopyTo { get; set; }
		public IProperties Fields { get; set; }
		public Union<SimilarityOption, string> Similarity { get; set; }
		public bool? Store { get; set; }
	}
}
