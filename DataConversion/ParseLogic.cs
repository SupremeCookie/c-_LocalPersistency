using System;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Persistency.DataConversion
{
	public static class ParseLogic
	{
		public static readonly Dictionary<Type, Converter> parsers = new Dictionary<Type, Converter>
		{
			{ typeof(int), new IntConverter() },
			{ typeof(float), new FloatConverter() },
			{ typeof(bool), new BoolConverter() },

			{ typeof(Coordinates), new CoordinatesConverter() },

#if UNITY_5_3_OR_NEWER
			{ typeof(Vector2), new Vector2Converter() },
#endif
		};



		public static string ParseValue<T>(T value, out ParseResult parseResult)
		{
			System.Type tType = typeof(T);
			if (tType.Equals(typeof(string)))
			{
				parseResult = ParseResult.Parsed;
				return value as string;
			}

			if (parsers.ContainsKey(tType))
			{
				var typeParser = parsers[tType];
				string result = typeParser.ParseToString<T>(value);
				parseResult = ParseResult.Parsed;
				return result;
			}

			parseResult = ParseResult.NoParse_NoConverter;
			Debug.LogWarning($"No parser defined for type: {tType.ToString()}, returning an empty string");
			return "";
		}

		public static T ParseValue<T>(string value, out ParseResult parseResult)
		{
			System.Type tType = typeof(T);
			if (tType.Equals(typeof(string)))
			{
				parseResult = ParseResult.Parsed;
				return (T)(object)value;
			}

			if (parsers.ContainsKey(tType))
			{
				var typeParser = parsers[tType];
				T result = typeParser.ParseFromString<T>(value);
				parseResult = ParseResult.Parsed;
				return result;
			}


			parseResult = ParseResult.NoParse_NoConverter;
			Debug.LogWarning($"No parser defined for type: {tType.ToString()}, returning a default T ({tType.ToString()})");
			return default(T);
		}
	}
}
