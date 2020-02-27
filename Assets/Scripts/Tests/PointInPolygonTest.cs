using System;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;

namespace LeTai.Selections.Test
{
public class PointInPolygonTest
{
    Vector2[] polygon;
    Vector2   point;

    System.Random rng;
    Stopwatch     stopwatch;

    [SetUp]
    public void Setup()
    {
        rng       = new System.Random();
        stopwatch = Stopwatch.StartNew();
    }

    const int tryCount = 5000;

    [Test]
    public void Compare()
    {
        double sumCn = 0;
        double sumWn = 0;

        for (int i = 0; i < tryCount; i++)
        {
            point   = RandomV2();
            polygon = new Vector2[10000];
            for (var j = 0; j < polygon.Length; j++)
            {
                polygon[j] = RandomV2();
            }

            stopwatch.Restart();
            PointInPolygon.CrossingNumber(point, polygon);
            stopwatch.Stop();

            sumCn += stopwatch.Elapsed.TotalMilliseconds;

            stopwatch.Restart();
            PointInPolygon.WindingNumber(point, polygon);
            stopwatch.Stop();

            sumWn += stopwatch.Elapsed.TotalMilliseconds;
        }

        Console.WriteLine($"Crossing Number:    {sumCn / tryCount}ms");
        Console.WriteLine($"Winding Number:    {sumWn / tryCount}ms");
    }

    Vector2 RandomV2()
    {
        return new Vector2((float) rng.NextDouble(), (float) rng.NextDouble());
    }
}
}
