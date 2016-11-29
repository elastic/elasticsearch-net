using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<InputContainer, IInputContainer>))]
	public interface IInputContainer
	{
		[JsonProperty("http")]
		IHttpInput Http { get; set; }

		[JsonProperty("search")]
		ISearchInput Search { get; set; }

		[JsonProperty("simple")]
		ISimpleInput Simple { get; set; }

		[JsonProperty("chain")]
		IChainInput Chain { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class InputContainer : IInputContainer, IDescriptor
	{
		IHttpInput IInputContainer.Http { get; set; }
		ISearchInput IInputContainer.Search { get; set; }
		ISimpleInput IInputContainer.Simple { get; set; }
		IChainInput IInputContainer.Chain { get; set; }

		internal InputContainer() {}

		public InputContainer(InputBase input)
		{
			input.ThrowIfNull(nameof(input));
			input.WrapInContainer(this);
		}

		public static implicit operator InputContainer(InputBase input) => input == null
			? null
			: new InputContainer(input);
	}

	public class InputDescriptor : InputContainer
	{
		private InputDescriptor Assign(Action<IInputContainer> assigner) => Fluent.Assign(this, assigner);

		public InputDescriptor Search(Func<SearchInputDescriptor, ISearchInput> selector) =>
			Assign(a => a.Search = selector.Invoke(new SearchInputDescriptor()));

		public InputDescriptor Http(Func<HttpInputDescriptor, IHttpInput> selector) =>
			Assign(a => a.Http = selector.Invoke(new HttpInputDescriptor()));

		public InputDescriptor Simple(Func<SimpleInputDescriptor, ISimpleInput> selector) =>
			Assign(a => a.Simple = selector.Invoke(new SimpleInputDescriptor()));

		public InputDescriptor Chain(Func<ChainInputDescriptor, IChainInput> selector) =>
			Assign(a => a.Chain = selector.Invoke(new ChainInputDescriptor()));
	}
}
