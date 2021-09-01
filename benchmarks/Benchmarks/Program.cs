using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Nest;

namespace Benchmarks;

internal class Program
{
	private static void Main()
	{
		var thing = new IndicesGetString() { NameCount = 3 };
		thing.Setup();
		thing.NaiveStringCreate();

		BenchmarkRunner.Run<IndicesGetString>();
	}
}

[MemoryDiagnoser]
public class IndicesGetString
{
	private readonly List<IndexName> _indices = new();

	private string _output = string.Empty;

	private readonly ElasticsearchClientSettings _settings = new();

	[Params(1,3)]
	public int NameCount { get; set; }

	[GlobalSetup]
	public void Setup()
	{
		for (var i = 0; i < NameCount; i++)
		{
			_indices.Add($"item{i}");
		}
	}

	[Benchmark(Baseline = true)]
	public void StringJoin()
	{
		var indices = _indices.Select(i => i.GetString(_settings)).Distinct();
		_output = string.Join(',', indices);
	}

	[Benchmark]
	public void NaiveStringCreate()
	{
		// This implementation does ensure distinct values
		// Is that really an issue as it's unlikely and would still be a valid request

		var length = 0;

		var indices = _indices;
		for (var i = 0; i < indices.Count; i++)
		{
			length += _indices[i].Value.Length + 1;
		}

		length = length == 0 ? 0 : length - 1;

		_output = string.Create(length, indices, (span, state) =>
		{
			var written = 0;
			for (var i = 0; i < indices.Count; i++)
			{
				var value = state[i].Value.AsSpan();
				value.CopyTo(span[written..]);
				written += value.Length;

				if (i != indices.Count - 1)
					span[written++] = ',';
			}
		});
	}
}
