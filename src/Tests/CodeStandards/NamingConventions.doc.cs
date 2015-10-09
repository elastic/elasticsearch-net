using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using FluentAssertions;

namespace Tests.CodeStandards
{
	public class NamingConventions
	{
		/**
		* Abstract classes should end with a `Base` suffix
		*/
		//[U]
		public void AbstractClassNamesEndWithBaseSuffix()
		{
			var abstractClasses = Assembly.Load("Nest").GetTypes()
				.Where(t => t.IsClass && t.IsAbstract && !t.IsSealed)
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			foreach (var abstractClass in abstractClasses)
				abstractClass.Should().EndWith("Base");
		}
	}
}
