﻿using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MathNet.Spatial.Euclidean;

namespace MathNet.Spatial
{
  public static class Parser
  {
    private const string SeparatorPattern = @" *[,;] *";
    //        public static readonly string Vector2DPattern = string.Format(@"^ *\(?(?<x>{0}){1}(?<y>{0})?\)? *$", DoublePattern, SeparatorPattern);
    public static readonly string DoublePattern = @"[+-]?\d*(?:[.,]\d+)?(?:[eE][+-]?\d+)?";

    public static readonly string Vector3DPattern = string.Format(@"^ *\(?(?<x>{0}){1}(?<y>{0}){1}(?<z>{0})\)? *$",
                                                                  DoublePattern, SeparatorPattern);

    private static readonly string _item3DPattern = Vector3DPattern.Trim('^', '$');

    private static readonly string PlanePointVectorPattern = string.Format(
                                                                           @"^ *p: *{{(?<p>{0})}} *v: *{{(?<v>{0})}} *$",
                                                                           _item3DPattern);

    private static readonly string PlaneAbcdPattern =
        string.Format(@"^ *\(?(?<a>{0}){1}(?<b>{0}){1}(?<c>{0}){1}(?<d>{0})\)? *$", DoublePattern, SeparatorPattern);


    public static double ParseDouble(Group group)
    {
      if (group.Captures.Count != 1)
      {
        throw new ArgumentException("Expected single capture");
      }

      return Parser.ParseDouble(group.Value);
    }


    public static double ParseDouble(string s)
    {
      return double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
    }


    //        public static double[] ParseItem2D(string vectorString)
    //        {
    //            var match = Regex.Match(vectorString, Vector2DPattern);
    //            Group[] ss = { match.Groups["x"], match.Groups["y"] };
    //            double[] ds = ss.Select(ParseDouble).ToArray();
    //            return ds;
    //        }

    public static double[] ParseItem3D(string vectorString)
    {
      var match = Regex.Match(vectorString, Vector3DPattern);
      Group[] ss =
      {
        match.Groups["x"],
        match.Groups["y"],
        match.Groups["z"]
      };
      double[] ds = ss.Select(x => double.Parse(x.Value.Replace(',', '.'), CultureInfo.InvariantCulture)).ToArray();
      return ds;
    }


    public static Plane ParsePlane(string s)
    {
      var match = Regex.Match(s, PlanePointVectorPattern);
      if (match.Success)
      {
        var p = Point3D.Parse(match.Groups["p"].Value);
        var uv = UnitVector3D.Parse(match.Groups["v"].Value);
        return new Plane(p, uv);
      }

      match = Regex.Match(s, PlaneAbcdPattern);
      {
        var a = Parser.ParseDouble(match.Groups["a"]);
        var b = Parser.ParseDouble(match.Groups["b"]);
        var c = Parser.ParseDouble(match.Groups["c"]);
        var d = Parser.ParseDouble(match.Groups["d"]);
        return new Plane(a, b, c, d);
      }
    }


    public static Ray3D ParseRay3D(string s)
    {
      var match = Regex.Match(s, PlanePointVectorPattern);
      var p = Point3D.Parse(match.Groups["p"].Value);
      var uv = UnitVector3D.Parse(match.Groups["v"].Value);
      return new Ray3D(p, uv);
    }
  }
}
