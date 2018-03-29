using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(PropertyJsonConverter))]
	public interface ICoreProperty : IProperty
	{
		/// <summary>
		/// Whether the field value should be stored and retrievable separately from the _source field
		/// Default is <c>false</c>.
		/// </summary>
		/// <remarks>
		/// Not valid on <see cref="ObjectProperty"/>
		/// </remarks>
		[JsonProperty("store")]
		bool? Store { get; set; }

		/// <summary>
		/// Configures multi-fields for this field. Allows one field to be indexed in different
		/// ways to serve different purposes
		/// </summary>
		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IProperties Fields { get; set; }

		/// <summary>
		/// Which relevancy scoring algorithm or similarity should be used. Defaults to BM25
		/// </summary>
		[JsonProperty("similarity")]
		Union<SimilarityOption, string> Similarity { get; set; }

		/// <summary>
		/// Copies the value of this field into another field, which can be queried as a single field.
		/// Allows for the creation of custom _all fields
		/// </summary>
		[JsonProperty("copy_to")]
		[JsonConverter(typeof(FieldsJsonConverter))]
		Fields CopyTo { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class CorePropertyBase : PropertyBase, ICoreProperty
	{
		protected CorePropertyBase(FieldType type) : base(type) { }

		/// <inheritdoc />
		public Fields CopyTo { get; set; }
		/// <inheritdoc />
		public IProperties Fields { get; set; }
		/// <inheritdoc />
		public Union<SimilarityOption, string> Similarity { get; set; }
		/// <inheritdoc />
		public bool? Store { get; set; }
	}
}
