using System;
using System.Globalization;
using System.Reflection;

namespace Utf8Json
{
	public interface IJsonProperty
	{
		string Name { get; set; }

		int Order { get; }

		bool Ignore { get; set; }

		bool? AllowPrivate { get; set; }
	}

	public class JsonProperty : IJsonProperty
	{
		public JsonProperty(string name)
		{
			Name = name;
		}

		public string Name { get; set; }

		public int Order
		{
			get { return 0; }
		}

		public bool Ignore { get; set; }

		public bool? AllowPrivate { get; set; }
	}
}
