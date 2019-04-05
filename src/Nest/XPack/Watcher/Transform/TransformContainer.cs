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
		private TransformDescriptor Assign<TValue>(TValue value, Action<ITransformContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public TransformDescriptor Search(Func<SearchTransformDescriptor, ISearchTransform> selector) =>
			Assign(selector,(a, v) => a.Search = v?.InvokeOrDefault(new SearchTransformDescriptor()));

		public TransformDescriptor Script(Func<ScriptTransformDescriptor, IScriptTransform> selector) =>
			Assign(selector,(a, v) => a.Script = v?.Invoke(new ScriptTransformDescriptor()));

		public TransformDescriptor Chain(Func<ChainTransformDescriptor, IChainTransform> selector) =>
			Assign(selector,(a, v) => a.Chain = v?.Invoke(new ChainTransformDescriptor()));
	}
}
