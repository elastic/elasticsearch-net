// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Elastic.Transport
{
	public class UserAgent
	{
		private readonly string _toString;

		private UserAgent(string reposName, Type typeVersionLookup, string[] metadata = null)
		{
			var version = typeVersionLookup.Assembly
				.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
				.InformationalVersion;

			var meta = string.Join("; ", metadata ?? Array.Empty<string>());
			var assemblyName = typeVersionLookup.Assembly.GetName().Name;

			_toString = $"{reposName}/{version} ({RuntimeInformation.OSDescription}; {RuntimeInformation.FrameworkDescription}; {assemblyName}{meta.Trim()})";
		}

		private UserAgent(string fullUserAgentString) => _toString = fullUserAgentString;

		public static UserAgent Create(string reposName, Type typeVersionLookup, string[] metadata) => new UserAgent(reposName, typeVersionLookup, metadata);
		public static UserAgent Create(string reposName, Type typeVersionLookup) => new UserAgent(reposName, typeVersionLookup);

		public static UserAgent Create(string fullUserAgentString) => new UserAgent(fullUserAgentString);

		public override string ToString() => _toString;
	}
}
