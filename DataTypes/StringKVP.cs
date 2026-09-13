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
			if (string.IsNullOrEmpty(content.key))
				Debug.LogWarning($"Content's key is null or empty, please fix");

			if (string.IsNullOrEmpty(content.value))
				Debug.LogWarning($"Content's value (for key:{content.key}) is null or empty, please fix");

			return new string[]
			{
				content.key,
				content.value,
			};
		}
	}
}
