using System;
using System.Collections.Generic;

namespace Tests.Framework.Integration
{
	public class CallUniqueValues : Dictionary<ClientMethod, string>
	{
		public CallUniqueValues()
		{
			Add(ClientMethod.Fluent, UniqueValue);
			Add(ClientMethod.FluentAsync, UniqueValue);
			Add(ClientMethod.Initializer, UniqueValue);
			Add(ClientMethod.InitializerAsync, UniqueValue);

			ExtendedValues.Add(ClientMethod.Fluent, new Dictionary<string, object>());
			ExtendedValues.Add(ClientMethod.FluentAsync, new Dictionary<string, object>());
			ExtendedValues.Add(ClientMethod.Initializer, new Dictionary<string, object>());
			ExtendedValues.Add(ClientMethod.InitializerAsync, new Dictionary<string, object>());
		}

		public ClientMethod CurrentView { get; set; } = ClientMethod.Fluent;

		public string Value => this[CurrentView];

		public ClientMethod[] Views { get; } = new[]
			{ ClientMethod.Fluent, ClientMethod.FluentAsync, ClientMethod.Initializer, ClientMethod.InitializerAsync };

		private IDictionary<ClientMethod, IDictionary<string, object>> ExtendedValues { get; }
			= new Dictionary<ClientMethod, IDictionary<string, object>>();

		private string UniqueValue => "nest-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		public T ExtendedValue<T>(string key) where T : class => ExtendedValues[CurrentView][key] as T;

		public void ExtendedValue<T>(string key, T value) where T : class => ExtendedValues[CurrentView][key] = value;
	}
}
