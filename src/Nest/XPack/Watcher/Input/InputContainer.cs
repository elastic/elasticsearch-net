using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<InputContainer, IInputContainer>))]
	public interface IInputContainer
	{
		[JsonProperty("chain")]
		IChainInput Chain { get; set; }

		[JsonProperty("http")]
		IHttpInput Http { get; set; }

		[JsonProperty("search")]
		ISearchInput Search { get; set; }

		[JsonProperty("simple")]
		ISimpleInput Simple { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class InputContainer : IInputContainer, IDescriptor
	{
		internal InputContainer() { }

		public InputContainer(InputBase input)
		{
			input.ThrowIfNull(nameof(input));
			input.WrapInContainer(this);
		}

		IChainInput IInputContainer.Chain { get; set; }
		IHttpInput IInputContainer.Http { get; set; }
		ISearchInput IInputContainer.Search { get; set; }
		ISimpleInput IInputContainer.Simple { get; set; }

		public static implicit operator InputContainer(InputBase input) => input == null
			? null
			: new InputContainer(input);
	}

	public class InputDescriptor : InputContainer
	{
		private InputDescriptor Assign<TValue>(TValue value, Action<IInputContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public InputDescriptor Search(Func<SearchInputDescriptor, ISearchInput> selector) =>
			Assign(selector, (a, v) => a.Search = v.Invoke(new SearchInputDescriptor()));

		public InputDescriptor Http(Func<HttpInputDescriptor, IHttpInput> selector) =>
			Assign(selector, (a, v) => a.Http = v.Invoke(new HttpInputDescriptor()));

		public InputDescriptor Simple(Func<SimpleInputDescriptor, ISimpleInput> selector) =>
			Assign(selector,(a, v) => a.Simple = v.Invoke(new SimpleInputDescriptor()));

		public InputDescriptor Chain(Func<ChainInputDescriptor, IChainInput> selector) =>
			Assign(selector, (a, v) => a.Chain = v.Invoke(new ChainInputDescriptor()));
	}
}
