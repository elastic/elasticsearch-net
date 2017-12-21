using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A query to run for a phrase suggester collate
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PhraseSuggestCollateQuery>))]
	public interface IPhraseSuggestCollateQuery
	{
		/// <summary>
		/// The source script to be executed
		/// </summary>
		[JsonProperty("source")]
		string Source { get; set; }

		/// <summary>
		/// The id for a stored script to execute
		/// </summary>
		[JsonProperty("id")]
		Id Id { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestCollateQuery : IPhraseSuggestCollateQuery
	{
		/// <inheritdoc />
		public string Source { get; set; }

		/// <inheritdoc />
		public Id Id { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestCollateQueryDescriptor :
		DescriptorBase<PhraseSuggestCollateQueryDescriptor, IPhraseSuggestCollateQuery>, IPhraseSuggestCollateQuery
	{
		string IPhraseSuggestCollateQuery.Source { get; set; }
		Id IPhraseSuggestCollateQuery.Id { get; set; }

		/// <inheritdoc />
		public PhraseSuggestCollateQueryDescriptor Source(string source) => Assign(a => a.Source = source);

		/// <inheritdoc />
		public PhraseSuggestCollateQueryDescriptor Id(Id id) => Assign(a => a.Id = id);
	}
}
