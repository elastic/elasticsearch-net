using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(InputContainer))]
	public interface IInputContainer
	{
		[DataMember(Name ="chain")]
		IChainInput Chain { get; set; }

		[DataMember(Name ="http")]
		IHttpInput Http { get; set; }

		[DataMember(Name ="search")]
		ISearchInput Search { get; set; }

		[DataMember(Name ="simple")]
		ISimpleInput Simple { get; set; }
	}

	[DataContract]
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
