//#define DEEP_LOGGING

using Persistency.DataConversion;
using System.Collections.Generic;

namespace Persistency
{
	public class CSVDataContainer
	{
		private string targetFile;

		private string rawData;
		private string[] lines;
		private Dictionary<string, string> data;
		// Note DK: May be good to investigate manners to store the data not in string format but in object format
		// Note DK: that way we don't do unnecessary conversions during runtime
		// Note DK: that also means we gotta store the data type somewhere, as we need to be able to cast the object properly and 
		// Note DK: we need to grab the right parsers whenever



		public bool HasData { get { return data != null; } }


		public CSVDataContainer() { }
		public CSVDataContainer(string filePath) { SetTarget(filePath); }


		public Dictionary<string, string> GetAllData()
		{
			Debug.Assert(HasData, $"Can't return ALL data, as the data collection is null");

			Dictionary<string, string> copyData = new Dictionary<string, string>(data.Count);

			foreach (var kvp in data)
			{
				copyData.Add(kvp.Key, kvp.Value);
			}

			return copyData;
		}


		public T GetValue<T>(string key, T defaultValue, out GetValueResult result)
		{
			T defaultObj = defaultValue;

			HasInitializedDataAssert();

			bool keyExists = data.ContainsKey(key);
			if (!keyExists)
			{
				result = GetValueResult.KeyDidNotExist;
				SetValue<T>(key, defaultValue, out var setResult);
				Debug.LogWarning($"No value for key ({key}) exists, will return defaultValue and store key with the defaultValue ({defaultValue}), setting the key returned with message: {setResult}");
				return defaultObj;
			}

			string value = data[key];
			var resultObject = ParseLogic.ParseValue<T>(value, out var parseResult);

#if DEEP_LOGGING
			Debug.Log($"Parsed value ({value}) for key ({key}), the result is: {parseResult}");
#endif

			if (parseResult == ParseResult.NoParse_NoConverter)
			{
				result = GetValueResult.KeyExisted_NoCast;
				Debug.LogError($"Trying to get value for type ({typeof(T)}), no parser exists for the type yet, returning defaultValue ({defaultValue}) instead");
				return defaultObj;
			}

			result = GetValueResult.KeyExisted;
			return resultObject;
		}

		public void SetValue<T>(string key, T value, out SetValueResult result)
		{
			string parsedValue = ParseLogic.ParseValue<T>(value, out var parseResult);
			if (parseResult == ParseResult.NoParse_NoConverter)
			{
				result = SetValueResult.ParserNotFound;
				return;
			}

			HasInitializedDataAssert();

			bool keyExists = data.ContainsKey(key);
			if (keyExists)
			{
				data[key] = parsedValue;
				result = SetValueResult.ValueStoredAndOverwritten;
				return;
			}

			data.Add(key, parsedValue);

			result = SetValueResult.ValueStored;
		}


		public void SetTarget(string targetFile)
		{
			this.targetFile = targetFile;
		}

		// TODO DK: Separate method into loose methods.
		public void OpenFile(out OpenFileResult result)
		{
			Debug.Assert(!string.IsNullOrEmpty(targetFile), $"TargetFile is null or empty, this means we can't target a file");
			Debug.Assert(string.IsNullOrEmpty(rawData), $"We already have some data stored in the class, this will overwrite the existing csv data, please clear existing data before opening a new file");

			rawData = FileHandling.FileHandler.GetFileContents(this.targetFile);
			bool fileContainsData = !string.IsNullOrEmpty(rawData);

			if (!fileContainsData)
			{
				lines = new string[] { };

				int minimumCapacity = 50;
				data = new Dictionary<string, string>(minimumCapacity);
				result = OpenFileResult.EmptyFile;
				return;
			}

			lines = TextTransformer.ToLines(rawData);
			Debug.Assert(!lines.IsNullOrEmpty(), $"We converted non-null raw data into lines, somehow ending up with no lines, please debug");

			int lineCount = lines.Length;
			data = new Dictionary<string, string>(lineCount);

			for (int i = 0; i < lineCount; ++i)
			{
				bool lineHasNoContent = string.IsNullOrEmpty(lines[i]);
				if (lineHasNoContent)
				{
					Debug.LogWarning($"No text for line on index: {i}, will skip and not parse");
					break;
				}

				StringKVP dataEntry = TextTransformer.ToKVP(lines[i], SplitCharacter.Comma);
				data.Add(dataEntry.key, dataEntry.value);
			}

			result = OpenFileResult.FilledFile;
		}

		// TODO DK: Separate method into loose methods.
		public void StoreFile(out StoreFileResult result)
		{
			Debug.Assert(!string.IsNullOrEmpty(this.targetFile), $"No target file set, can't store data if there's no target");
			HasInitializedDataAssert();

			bool dataIsEmpty = data.Count == 0;
			if (dataIsEmpty)
			{
				result = StoreFileResult.DidNotStore_NoData;
				return;
			}

			int index = 0;
			string[] csvLines = new string[data.Count];
			foreach (var kvp in data)
			{
				StringKVP stringified = new StringKVP(kvp.Key, kvp.Value);
				string line = TextTransformer.FromKVP(stringified, SplitCharacter.Comma);
				Debug.Assert(!string.IsNullOrEmpty(line), $"No data after converting the StringKVP to a string, stringKVP.key(\"{stringified.key}\"), stringKVP.value(\"{stringified.value}\"), string({line})");

				csvLines[index] = line;
				index++;
			}

			System.Array.Sort(csvLines);

			string csvFileContent = TextTransformer.FromLines(csvLines);

			var storeResult = FileHandling.FileHandler.StoreData(targetFile, csvFileContent);

			Debug.Log($"Stored file at path ({targetFile}), result was: {storeResult}");

			if (storeResult == StoreDataResult.NoStorage_NoFileAtPath)
			{
				result = StoreFileResult.DidNotStore_NoFile;
				return;
			}

			result = StoreFileResult.Stored;
		}


		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		private void HasInitializedDataAssert()
		{
			Debug.Assert(HasData, $"The data collection is null, can't set value, please first instantiate the data collection");
		}
	}
}