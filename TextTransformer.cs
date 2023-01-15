namespace Persistency
{
	public static class SplitCharacterExtensions
	{
		public static string[] ToSplitCharArray(this SplitCharacter character)
		{
			return new string[] { character.ToSplitString() };
		}

		public static string ToSplitString(this SplitCharacter character)
		{
			switch (character)
			{
				case SplitCharacter.Comma: { return ","; }

				default:
				{
					Debug.LogError($"No case defined for: ({character}), please add");
					return null;
				}
			}
		}
	}

	public static class TextTransformer
	{
		public static string[] ToLines(string content)
		{
			Debug.Assert(!string.IsNullOrEmpty(content), $"No content to turn into an array, please provide some content, currentContent: {content}");

			string[] result = content.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);

			Debug.Assert(!result.IsNullOrEmpty(), $"content couldn't be converted to an array of lines, please debug");

			return result;
		}

		public static string FromLines(string[] lines)
		{
			Debug.Assert(!lines.IsNullOrEmpty(), $"Trying to convert empty or null lines to string content, this is not okay, please provide some content");

			int initialCapacity = 250;
			System.Text.StringBuilder sb = new System.Text.StringBuilder(initialCapacity);
			for (int i = 0; i < lines.Length; ++i)
			{
				sb.AppendLine(lines[i]);
			}

			string result = sb.ToString();

			Debug.Assert(result != null, $"Stringbuilding with the content, resulted in a string that is null, this shouldn't happen. Input lines count: {lines?.Length ?? -1}");

			return result;
		}

		public static StringKVP ToKVP(string content, SplitCharacter splitCharacter)
		{
			Debug.Assert(!string.IsNullOrEmpty(content), $"Trying to parse an empty or null content string to a {typeof(StringKVP)}, we need some content. Current content: ({content})");

			string[] splitByCharacter = content.Split(splitCharacter.ToSplitCharArray(), System.StringSplitOptions.None);
			Debug.Assert(!splitByCharacter.IsNullOrEmpty(), $"The content ({content}), could not be split by: ({splitCharacter.ToSplitCharArray()}), the result is null or empty");
			Debug.Assert(splitByCharacter.Length > 1, $"SplitByCharacter has less than 2 entries ({splitByCharacter.Length}), this means the data is invalid, or the line read wrongly, content: ({content})");

			string key = splitByCharacter[0];
			string value = splitByCharacter[1];

			bool hasMoreThanTwoEntries = splitByCharacter.Length > 2;
			if (hasMoreThanTwoEntries)
			{
				Debug.LogWarning($"We detected more than 2 data entries in the line: ({content}) after splitting it, we will consider the content " +
					$"till the first splitCharacter ({splitCharacter.ToSplitString()}) as the key, and everything after as the value");

				int indexOfSecondEntry = key.Length + splitCharacter.ToSplitCharArray().Length;
				string rawValueString = content.Substring(indexOfSecondEntry);

				Debug.LogWarning($"The taken literal string is: ({rawValueString}), this will then be parsed by the system");
				value = rawValueString;
			}

			StringKVP kvp = new StringKVP(key, value);
			return kvp;
		}

		public static string FromKVP(StringKVP content, SplitCharacter splitCharacter)
		{
			string splitString = splitCharacter.ToSplitString();
			string[] values = content.ToArray();
			string result = string.Join(splitString, values);

			Debug.Assert(!string.IsNullOrEmpty(result), $"Converting from StringKVP to string caused the result to be null or empty, please fix");

			return result;
		}
	}
}
