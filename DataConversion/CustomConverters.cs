using System.Collections.Generic;
using System.Linq;

namespace Persistency.DataConversion
{
	public class CoordinatesConverter : Converter
	{
		private const char splitChar = '|';

		public override T ParseFromString<T>(string data)
		{
			var result = new Coordinates();

			string[] splitString = data.Split(splitChar);

			Debug.Assert(!splitString.IsNullOrEmpty(), $"No Coordinates data could be split using splitChar: ( {splitChar} ), and input: ( {data} )");
			Debug.Assert(splitString.Length == 2, $"Splitting the data: ( {data} ) using splitChar: ( {splitChar} ) did not create 2 entries");

			bool convertedX = int.TryParse(splitString[0], out result.x);
			bool convertedY = int.TryParse(splitString[1], out result.y);

			Debug.Assert(convertedX, $"Could not convert the string value for x, string value: {splitString[0]}");
			Debug.Assert(convertedY, $"Could not convert the string value for y, string value: {splitString[1]}");

			return (T)(object)result;
		}

		public override string ParseToString<T>(T obj)
		{
			Coordinates castedObj = (Coordinates)(object)obj;
			return $"{castedObj.x}{splitChar}{castedObj.y}";
		}
	}


	public class StringKVPListConverter : Converter
	{
		private const string entrySplitChar = "), (";
		private const char splitChar = ',';


		public override T ParseFromString<T>(string data)
		{
			//Debug.Log($"[StringConverter] parsing data: {data}");
			if (string.IsNullOrEmpty(data))
				return (T)(object)new List<StringKVP>();

			var splitData = data.Split(entrySplitChar);
			if (splitData.IsNullOrEmpty())
				return (T)(object)new List<StringKVP>();

			var result = new List<StringKVP>(splitData.Length);
			for (int i = 0; i < splitData.Length; ++i)
			{
				var entry = splitData[i];

				// We first cleanup the entry, then split it.
				var firstRemovalChar = entry.IndexOf("(");
				if (firstRemovalChar >= 0)
					entry = entry.Remove(firstRemovalChar, 1);

				var secondRemovalChar = entry.IndexOf(")");
				if (secondRemovalChar >= 0)
					entry = entry.Remove(secondRemovalChar, 1);

				var kvpEntry = entry.Split(",");
				Debug.Assert(kvpEntry.Length == 2, $"For the current entry: ({entry}) there are not 2 strings after splitting it");

				result.Add(new StringKVP(kvpEntry[0].Trim(), kvpEntry[1].Trim()));
			}

			return (T)(object)result;
		}

		public override string ParseToString<T>(T obj)
		{
			var typeData = obj as List<StringKVP>;
			if (typeData.IsNullOrEmpty())
				return "";

			//Debug.Log($"[StringConverter]  data: {typeData.Select(s => ToString(s)).Aggregate((s, x) => { return $"({s}),  ({x})"; })}");

			var result = "";
			for (int i = 0; i < typeData.Count; ++i)
			{
				result += ToString(typeData[i]);
				if (i < typeData.Count - 1)
					result += $"{splitChar} ";
			}

			//Debug.Log($"[StringConverter]  result: {result}");

			return result;
		}

		private string ToString(StringKVP input)
		{
			return $"({input.key},{input.value})";
		}
	}
}
