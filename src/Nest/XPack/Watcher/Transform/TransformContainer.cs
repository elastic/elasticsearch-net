using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<TransformContainer, ITransformContainer>))]
	public interface ITransformContainer
	{
		[JsonProperty("search")]
		ISearchTransform Search { get; set; }

		[JsonProperty("script")]
		IScriptTransform Script { get; set; }

		[JsonProperty("chain")]
		IChainTransform Chain { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class TransformContainer : ITransformContainer, IDescriptor
	{
		internal TransformContainer() {}

		public TransformContainer(TransformBase transform)
		{
			transform.ThrowIfNull(nameof(transform));
			transform.WrapInContainer(this);
		}

		ISearchTransform ITransformContainer.Search { get; set; }
		IScriptTransform ITransformContainer.Script { get; set; }
		IChainTransform ITransformContainer.Chain { get; set; }
	}

	public class TransformDescriptor : TransformContainer
	{
		private TransformDescriptor Assign(Action<ITransformContainer> assigner) => Fluent.Assign(this, assigner);

		public TransformDescriptor Search(Func<SearchTransformDescriptor, ISearchTransform> selector) =>
			Assign(a => a.Search = selector?.InvokeOrDefault(new SearchTransformDescriptor()));

		public TransformDescriptor Script(Func<ScriptTransformDescriptor, IScriptTransform> selector) =>
			Assign(a => a.Script = selector?.Invoke(new ScriptTransformDescriptor()));

		public TransformDescriptor Chain(Func<ChainTransformDescriptor, IChainTransform> selector) =>
			Assign(a => a.Chain = selector.Invoke(new ChainTransformDescriptor()));
	}
}
