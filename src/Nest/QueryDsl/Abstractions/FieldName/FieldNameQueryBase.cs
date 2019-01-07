using System.Runtime.Serialization;

namespace Nest
{
	public interface IFieldNameQuery : IQuery
	{
		[IgnoreDataMember]
		Field Field { get; set; }
	}

	public abstract class FieldNameQueryBase : QueryBase, IFieldNameQuery
	{
		public Field Field { get; set; }
	}
}
