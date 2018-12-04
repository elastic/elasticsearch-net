using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A query to run for a phrase suggester collate
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(PhraseSuggestCollateQuery))]
	public interface IPhraseSuggestCollateQuery
	{
		/// <summary>
		/// The id for a stored script to execute
		/// </summary>
		[DataMember(Name = "id")]
		Id Id { get; set; }

		/// <summary>
		/// The source script to be executed
		/// </summary>
		[DataMember(Name = "source")]
		string Source { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestCollateQuery : IPhraseSuggestCollateQuery
	{
		/// <inheritdoc />
		public Id Id { get; set; }

		/// <inheritdoc />
		public string Source { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestCollateQueryDescriptor
		: DescriptorBase<PhraseSuggestCollateQueryDescriptor, IPhraseSuggestCollateQuery>, IPhraseSuggestCollateQuery
	{
		Id IPhraseSuggestCollateQuery.Id { get; set; }
		string IPhraseSuggestCollateQuery.Source { get; set; }

		/// <inheritdoc />
		public PhraseSuggestCollateQueryDescriptor Source(string source) => Assign(a => a.Source = source);

		/// <inheritdoc />
		public PhraseSuggestCollateQueryDescriptor Id(Id id) => Assign(a => a.Id = id);
	}
}
