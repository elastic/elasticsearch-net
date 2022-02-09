﻿// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Bogus;
using System;
using Tests.Configuration;
using System.Net;
using Elastic.Clients.Elasticsearch;

namespace Tests.Domain;

public class Ranges
{
	//for deserialization
	public Ranges() { }

	private Ranges(Faker faker)
	{
		bool R() => faker.Random.Bool();

		SetDates(faker, R);
		SetDoubles(faker, R);
		SetFloats(faker, R);
		SetIntegers(faker, R);
		SetLongs(faker, R);
		SetIps(faker, R);
	}

	public DateRange Dates { get; set; }
	public DoubleRange Doubles { get; set; }
	public FloatRange Floats { get; set; }

	public static Faker<Ranges> Generator { get; } =
		new Faker<Ranges>()
			.UseSeed(TestConfiguration.Instance.Seed)
			.CustomInstantiator((f) => new Ranges(f));

	public IntegerRange Integers { get; set; }
	public IpAddressRange Ips { get; set; }
	public LongRange Longs { get; set; }

	private void SetDates(Faker faker, Func<bool> r)
	{
		var past = faker.Date.Past(faker.Random.Int(1, 19));
		var future = faker.Date.Future(faker.Random.Int(1, 10), past);
		var d = new DateRange();
		SwapAssign(r(), past, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
		SwapAssign(r(), future, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
		Dates = d;
	}

	private void SetDoubles(Faker faker, Func<bool> r)
	{
		var low = faker.Random.Double(-121, 10000);
		var high = faker.Random.Double(low, Math.Abs(low * 10)) + 2;
		var d = new DoubleRange();
		SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
		SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
		Doubles = d;
	}

	private void SetFloats(Faker faker, Func<bool> r)
	{
		var low = faker.Random.Float(-2000, 10000);
		var high = faker.Random.Float(low, Math.Abs(low * 10)) + 2;
		var d = new FloatRange();
		SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
		SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
		Floats = d;
	}

	private void SetIntegers(Faker faker, Func<bool> r)
	{
		var low = faker.Random.Int(-100, 10000);
		var high = faker.Random.Int(low, Math.Abs(low * 10)) + 2;
		var d = new FloatRange();
		SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
		SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
		Floats = d;
	}

	private void SetLongs(Faker faker, Func<bool> r)
	{
		var low = faker.Random.Long(-100, 10000);
		var high = faker.Random.Long(low, Math.Abs(low * 10)) + 2;
		var d = new LongRange();
		SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
		SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
		Longs = d;
	}

	private void SetIps(Faker faker, Func<bool> r)
	{
		var low = faker.Internet.Ip();
		var high = faker.Internet.Ip();
		var lowBytes = IPAddress.Parse(low).GetAddressBytes();
		var highBytes = IPAddress.Parse(high).GetAddressBytes();
		for (var i = 0; i < lowBytes.Length; i++)
		{
			var comparison = lowBytes[i].CompareTo(highBytes[i]);
			if (comparison == 0)
				continue;

			if (comparison > 0)
			{
				var s = low;
				low = high;
				high = s;
			}

			break;
		}
		var d = new IpAddressRange();
		SwapAssign(r(), low, v => d.GreaterThan = v, v => d.GreaterThanOrEqualTo = v);
		SwapAssign(r(), high, v => d.LessThan = v, v => d.LessThanOrEqualTo = v);
		Ips = d;
	}

	private static void SwapAssign<T>(bool b, T value, Action<T> first, Action<T> second)
	{
		if (b)
			first(value);
		else
			second(value);
	}
}
