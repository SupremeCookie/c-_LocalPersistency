namespace Persistency
{
	public struct StringKVP
	{
		public string key;
		public string value;

		public StringKVP(string key, string value) { this.key = key; this.value = value; }
	}

	public static class StringKVPExtensions
	{
		public static string[] ToArray(this StringKVP content)
		{
			Debug.Assert(!string.IsNullOrEmpty(content.key), $"Content's key is null or empty, please fix");
			Debug.Assert(!string.IsNullOrEmpty(content.value), $"Content's value is null or empty, please fix");

			return new string[]
			{
				content.key,
				content.value,
			};
		}
	}
}
