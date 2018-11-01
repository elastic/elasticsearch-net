using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<TransformContainer, ITransformContainer>))]
	public interface ITransformContainer
	{
		[JsonProperty("chain")]
		IChainTransform Chain { get; set; }

		[JsonProperty("script")]
		IScriptTransform Script { get; set; }

		[JsonProperty("search")]
		ISearchTransform Search { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class TransformContainer : ITransformContainer, IDescriptor
	{
		internal TransformContainer() { }

		public TransformContainer(TransformBase transform)
		{
			transform.ThrowIfNull(nameof(transform));
			transform.WrapInContainer(this);
		}

		IChainTransform ITransformContainer.Chain { get; set; }
		IScriptTransform ITransformContainer.Script { get; set; }

		ISearchTransform ITransformContainer.Search { get; set; }
	}

	public class TransformDescriptor : TransformContainer
	{
		public TransformDescriptor Chain(Func<ChainTransformDescriptor, IChainTransform> selector) =>
			Assign(a => a.Chain = selector.Invoke(new ChainTransformDescriptor()));

		public TransformDescriptor Script(Func<ScriptTransformDescriptor, IScriptTransform> selector) =>
			Assign(a => a.Script = selector?.Invoke(new ScriptTransformDescriptor()));

		public TransformDescriptor Search(Func<SearchTransformDescriptor, ISearchTransform> selector) =>
			Assign(a => a.Search = selector?.InvokeOrDefault(new SearchTransformDescriptor()));

		private TransformDescriptor Assign(Action<ITransformContainer> assigner) => Fluent.Assign(this, assigner);
	}
}
