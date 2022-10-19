using System.IO;

namespace Persistency.FileHandling
{

	public static class ExtensionExtensionMethods
	{
		public static string ToExactString(this Extension extension)
		{
			switch (extension)
			{
				case Extension.Ini: return ".ini";
				case Extension.Json: return ".json";
				case Extension.Txt: return ".txt";
				case Extension.None: return "";

				default:
				{
					Debug.Log($"No case defined for Extension: {extension}, please fix");
					return "";
				}
			}
		}
	}
	public static class FileHandler
	{
		public static CreationResult CreateIfNotExists(string filePath, Extension extension)
		{
			string fileName = Path.GetFileName(filePath);
			string fileExtension = Path.GetExtension(fileName);
			bool hasExtension = !string.IsNullOrEmpty(fileExtension);

			string newFileExtension = extension.ToExactString();
			bool extensionsMatch = fileExtension.Equals(newFileExtension);

			if (hasExtension && !extensionsMatch)
			{
#if UNITY_EDITOR
				Debug.LogError($"The file has an extension ({fileExtension}), but it doesn't match with ours ({newFileExtension})");
#endif
			}
			else if (!hasExtension)
			{
				filePath += newFileExtension;
			}

			bool fileExists = DoesFileExist(filePath);

			if (fileExists)
			{
				return CreationResult.NoCreation_AlreadyExists;
			}
			else
			{
				CreateFile(filePath);
				return CreationResult.Created_DidNotExist;
			}
		}

		public static DeletionResult DestroyIfExists(string filePath)
		{
			bool fileExists = DoesFileExist(filePath);
			if (!fileExists)
			{
				return DeletionResult.NoDeletion_DidNotExist;
			}

			File.Delete(filePath);
			return DeletionResult.Deleted;
		}


		public static bool DoesFileExist(string filePath)
		{
			return File.Exists(filePath);
		}


		private static void CreateFile(string filePath)
		{
			string folderPathOfFilePath = Path.GetDirectoryName(filePath);
			bool folderExists = Directory.Exists(folderPathOfFilePath);

			if (!folderExists)
			{
				Directory.CreateDirectory(folderPathOfFilePath);
			}

			var fs = File.Create(filePath);
			fs.Dispose();
		}


		public static string GetFileContents(string filePath)
		{
			Debug.Assert(DoesFileExist(filePath), $"There is no file at path: ({filePath}), please add a file before asking for content");

			string content = File.ReadAllText(filePath);
			if (string.IsNullOrEmpty(content))
			{
				Debug.Log($"File data at path: ({filePath}), is empty or null");
			}

			return content;
		}


		public static StoreDataResult StoreData(string filePath, string content)
		{
			Debug.Assert(content != null, $"Trying to store <null> data, that is not allowed, an empty string is the minimum requirement");

			bool targetFileExists = DoesFileExist(filePath);
			if (!targetFileExists)
			{
				return StoreDataResult.NoStorage_NoFileAtPath;
			}

			bool hasContent = content.Length > 0;
			if (!hasContent)
			{
				Debug.LogWarning($"There is no content to store, the file will be empty");
			}

			string existingContent = GetFileContents(filePath);
			bool thereIsExistingContent = !string.IsNullOrEmpty(existingContent);
			if (thereIsExistingContent)
			{
				Debug.LogWarning($"There is existing content, of length ({existingContent.Length}), we will overwrite this with new content, of length ({content.Length})");
			}

			File.WriteAllText(filePath, content);
			return StoreDataResult.StoredData;
		}
	}
}