using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tests.Profiling.Framework
{
	public class ProfiledClass
	{
		private object _instance;

		public string Name => this.Type.Name;

		public Type Type { get; set; }

		public ProfiledMethod SetupMethod { get; set; }

		public IEnumerable<ProfiledMethod> Methods { get; set; }

		public ProfiledClass(Type type, ProfiledMethod setupMethod, IEnumerable<ProfiledMethod> methods)
		{
			this.Type = type;
			this.Methods = methods;
			this.SetupMethod = setupMethod;
		}

		public object CreateInstance(ProfilingCluster cluster)
		{
			if (_instance == null)
			{
				var constructors = this.Type.GetTypeInfo().GetConstructors();

				var clusterConstructor = constructors.FirstOrDefault(c =>
				{
					var parameters = c.GetParameters();
					return parameters.Length == 1 &&
						   typeof(ProfilingCluster).IsAssignableFrom(parameters[0].ParameterType);
				});

				_instance = clusterConstructor != null
					? Activator.CreateInstance(this.Type, cluster)
					: Activator.CreateInstance(this.Type);
			}

			return _instance;
		}
	}
}
