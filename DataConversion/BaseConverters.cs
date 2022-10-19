namespace Persistency.DataConversion
{
	public abstract class Converter
	{
		public abstract string ParseToString<T>(T obj);
		public abstract T ParseFromString<T>(string data);
	}

	public class IntConverter : Converter
	{
		public override T ParseFromString<T>(string data)
		{
			bool converted = int.TryParse(data, out int result);
			Debug.Assert(converted, $"Tried to convert a non-int-parsable value ({data})");

			return (T)(object)result;
		}

		public override string ParseToString<T>(T obj)
		{
			return obj.ToString();
		}
	}

	public class BoolConverter : Converter
	{
		public override T ParseFromString<T>(string data)
		{
			bool converted = bool.TryParse(data, out bool result);
			Debug.Assert(converted, $"Tried to convert a non-bool-parsable value ({data})");

			return (T)(object)result;
		}

		public override string ParseToString<T>(T obj)
		{
			return obj.ToString();
		}
	}

	public class FloatConverter : Converter
	{
		public override T ParseFromString<T>(string data)
		{
			bool converted = float.TryParse(data, out float result);
			Debug.Assert(converted, $"Tried to convert a non-float-parsable value ({data})");

			return (T)(object)result;
		}

		public override string ParseToString<T>(T obj)
		{
			return obj.ToString();
		}
	}
}
