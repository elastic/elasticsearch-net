using System;
using System.Collections.Generic;

namespace Tests.Profiling.Framework
{
	public class ProfileConfiguration
	{
		private ProfileConfiguration() {}

		public IEnumerable<string> ClassNames { get; private set; }

		public static ProfileConfiguration Parse(string[] args)
		{
			var configuration = new ProfileConfiguration();

			var classNames = new HashSet<string>();
			var classIndex = Array.IndexOf(args, "Class");
			if (classIndex > -1 && args.Length > classIndex + 1)
			{
				foreach (var className in args[classIndex + 1].Split(','))
				{
					classNames.Add(className);
				}
			}

			configuration.ClassNames = classNames;
			return configuration;
		}
	}
}
