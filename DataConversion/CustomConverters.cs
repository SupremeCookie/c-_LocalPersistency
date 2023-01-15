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
}
