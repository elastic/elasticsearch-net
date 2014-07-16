using System;
using Nest.Resolvers.Writers;

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
			var type = TypeNameMarker.Create(t);
			var writer = new TypeMappingWriter(t, type, TestElasticClient.Settings, 10);

			return writer.MapFromAttributes();
		}
	}
}
