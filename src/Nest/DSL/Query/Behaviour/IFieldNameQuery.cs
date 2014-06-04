using Nest.Resolvers;

namespace Nest.DSL.Query.Behaviour
{
	public interface IFieldNameQuery : IQuery
	{
		PropertyPathMarker GetFieldName();
		void SetFieldName(string fieldName);
	}
}