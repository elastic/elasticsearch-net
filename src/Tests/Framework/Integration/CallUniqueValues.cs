using System;
using System.Collections.Generic;

namespace Tests.Framework.Integration
{
	public class CallUniqueValues : Dictionary<ClientMethod, string>
	{
		private string UniqueValue => "nest-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		private IDictionary<ClientMethod, IDictionary<string, object>> ExtendedValues { get; }
			= new Dictionary<ClientMethod, IDictionary<string, object>>();

		public ClientMethod CurrentView { get; set; } = ClientMethod.Fluent;
		public ClientMethod[] Views { get; } = new[] { ClientMethod.Fluent, ClientMethod.FluentAsync, ClientMethod.Initializer, ClientMethod.InitializerAsync };

		public string Value => this[CurrentView];
		public T ExtendedValue<T>(string key) where T : class => this.ExtendedValues[CurrentView][key] as T;
		public void ExtendedValue<T>(string key, T value) where T : class => this.ExtendedValues[CurrentView][key] = value;

		public CallUniqueValues()
		{
			this.Add(ClientMethod.Fluent, this.UniqueValue);
			this.Add(ClientMethod.FluentAsync, this.UniqueValue);
			this.Add(ClientMethod.Initializer, this.UniqueValue);
			this.Add(ClientMethod.InitializerAsync, this.UniqueValue);

			this.ExtendedValues.Add(ClientMethod.Fluent, new Dictionary<string, object>());
			this.ExtendedValues.Add(ClientMethod.FluentAsync, new Dictionary<string, object>());
			this.ExtendedValues.Add(ClientMethod.Initializer, new Dictionary<string, object>());
			this.ExtendedValues.Add(ClientMethod.InitializerAsync, new Dictionary<string, object>());
		}
	}
}