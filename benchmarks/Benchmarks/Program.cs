using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks;

internal class Program
{
	private static void Main()
	{
		//var thing = new IndicesGetString() { NameCount = 3 };
		//thing.Setup();
		//thing.NaiveStringCreate();

		//BenchmarkRunner.Run<OneOrMany>();
	}
}

//[MemoryDiagnoser]
//public class IndicesGetString
//{
//	private readonly List<IndexName> _indices = new();

//	private string _output = string.Empty;

//	private readonly ElasticsearchClientSettings _settings = new();

//	[Params(1,3)]
//	public int NameCount { get; set; }

//	[GlobalSetup]
//	public void Setup()
//	{
//		for (var i = 0; i < NameCount; i++)
//		{
//			_indices.Add($"item{i}");
//		}
//	}

//	[Benchmark(Baseline = true)]
//	public void StringJoin()
//	{
//		var indices = _indices.Select(i => i.GetString(_settings)).Distinct();
//		_output = string.Join(',', indices);
//	}

//	[Benchmark]
//	public void NaiveStringCreate()
//	{
//		// This implementation doesn't ensure distinct values
//		// Is that really an issue as it's unlikely and would still be a valid request?

//		// Issue: This doesn't call the Inferrer so is not a fair test since we'd 

//		var length = 0;

//		var indices = _indices;
//		for (var i = 0; i < indices.Count; i++)
//		{
//			length += _indices[i].Value.Length + 1;
//		}

//		length = length == 0 ? 0 : length - 1;

//		_output = string.Create(length, indices, (span, state) =>
//		{
//			var written = 0;
//			for (var i = 0; i < indices.Count; i++)
//			{
//				var value = state[i].Value.AsSpan();
//				value.CopyTo(span[written..]);
//				written += value.Length;

//				if (i != indices.Count - 1)
//					span[written++] = ',';
//			}
//		});
//	}
//}

///// <summary>
///// Investigate whether it's "more efficient" to use a one or many design for Indices.
///// This avoids extra allocations in the case of a single item but is not so good for > 1 item.
///// </summary>
//[MemoryDiagnoser]
//public class OneOrMany
//{
//	private readonly List<IndexName> _indices = new();

//	private Indices? _finalIndices = null;
//	private IndicesV2? _finalIndicesV2 = null;

//	private readonly ElasticsearchClientSettings _settings = new();

//	[Params(1, 3)]
//	public int NameCount { get; set; }

//	[GlobalSetup]
//	public void Setup()
//	{
//		for (var i = 0; i < NameCount; i++)
//		{
//			_indices.Add($"item{i}");
//		}
//	}

//	[Benchmark(Baseline = true)]
//	public void PureHashSet() => _finalIndices = new Indices(_indices);

//	[Benchmark]
//	public void OneOrManyConcept()
//	{
//		if (_indices.Count == 1)
//			_finalIndicesV2 = new IndicesV2(_indices[0]);
//		else
//			_finalIndicesV2 = new IndicesV2(_indices);
//	}

//	internal partial class IndicesV2
//	{
//		public static readonly IndicesV2 All = new("_all");

//		private readonly HashSet<IndexName>? _indices;
//		private readonly IndexName? _index;

//		internal IndicesV2(IndexName index) => _index = index;

//		public IndicesV2(IEnumerable<IndexName> indices)
//		{
//			if (_indices is null)
//				_indices = new HashSet<IndexName>();

//			_indices.UnionWith(indices);
//		}
//	}
//}
