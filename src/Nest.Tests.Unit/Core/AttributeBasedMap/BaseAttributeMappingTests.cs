using System;
using Nest.Resolvers.Writers;
using Nest.Resolvers;

namespace Nest.Tests.Unit.Core.AttributeBasedMap
{
	public class BaseAttributeMappingTests : BaseJsonTests
	{
		protected string CreateMapFor<T>() where T : class
		{
			return this.CreateMapFor(typeof(T));
		}
		protected string CreateMapFor(Type t)
		{
			var type = new TypeNameResolver().GetTypeNameFor(t);
			var writer = new TypeMappingWriter(t, type);

			return writer.MapFromAttributes();
		}
	}
}
