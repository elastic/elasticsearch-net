using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;
using System.Reflection;
using System.IO;
using Nest.Resolvers.Writers;
using Nest.Resolvers;

namespace Nest.Tests.Unit
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
			var writer = new TypeMappingWriter(t, type, new PropertyNameResolver());

			return writer.MapFromAttributes();
		}
	}
}
