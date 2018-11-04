using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tests.Profiling.Framework
{
	public class ProfiledClass
	{
		private object _instance;

		public ProfiledClass(Type type, ProfiledMethod setupMethod, IEnumerable<ProfiledMethod> methods)
		{
			Type = type;
			Methods = methods;
			SetupMethod = setupMethod;
		}

		public IEnumerable<ProfiledMethod> Methods { get; set; }

		public string Name => Type.Name;

		public ProfiledMethod SetupMethod { get; set; }

		public Type Type { get; set; }

		public object CreateInstance(ProfilingCluster cluster)
		{
			if (_instance == null)
			{
				var constructors = Type.GetTypeInfo().GetConstructors();

				var clusterConstructor = constructors.FirstOrDefault(c =>
				{
					var parameters = c.GetParameters();
					return parameters.Length == 1 &&
						typeof(ProfilingCluster).IsAssignableFrom(parameters[0].ParameterType);
				});

				_instance = clusterConstructor != null
					? Activator.CreateInstance(Type, cluster)
					: Activator.CreateInstance(Type);
			}

			return _instance;
		}
	}
}
