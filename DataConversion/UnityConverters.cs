#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Persistency.DataConversion
{
	public class Vector2Converter : Converter
	{
		private const char splitChar = '|';

		public override T ParseFromString<T>(string data)
		{
			var result = Vector2.zero;

			string[] splitString = data.Split(splitChar);

			Debug.Assert(!splitString.IsNullOrEmpty(), $"No Vector2 data could be split using splitChar: ( {splitChar} ), and input: ( {data} )");
			Debug.Assert(splitString.Length == 2, $"Splitting the data: ( {data} ) using splitChar: ( {splitChar} ) did not create 2 entries");

			bool convertedX = float.TryParse(splitString[0], out result.x);
			bool convertedY = float.TryParse(splitString[1], out result.y);

			Debug.Assert(convertedX, $"Could not convert the string value for x, string value: {splitString[0]}");
			Debug.Assert(convertedY, $"Could not convert the string value for y, string value: {splitString[1]}");

			return (T)(object)result;
		}

		public override string ParseToString<T>(T obj)
		{
			Vector2 castedObj = (Vector2)(object)obj;
			return $"{castedObj.x}{splitChar}{castedObj.y}";
		}
	}
}
