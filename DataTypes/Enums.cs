namespace Persistency
{
	// CSVDataContainer
	public enum GetValueResult
	{
		KeyDidNotExist,
		KeyExisted,
		KeyExisted_NoCast,
	}

	public enum SetValueResult
	{
		ValueStored,
		ValueStoredAndOverwritten,
		ParserNotFound,
	}

	public enum OpenFileResult
	{
		EmptyFile,
		FilledFile,
	}

	public enum StoreFileResult
	{
		DidNotStore_NoData,
		DidNotStore_NoFile,
		Stored,
	}

	public enum ParseResult
	{
		NoParse_NoConverter,
		Parsed,
	}
	///////////////////////////////


	// FileHandler
	public enum CreationResult
	{
		NoCreation_AlreadyExists,
		Created_DidNotExist,
	}

	public enum DeletionResult
	{
		NoDeletion_DidNotExist,
		Deleted,
	}

	public enum StoreDataResult
	{
		StoredData,
		NoStorage_NoFileAtPath,
	}

	public enum Extension
	{
		None,
		Txt,
		Json,
		Ini,
	}

	public enum FileExistsResult
	{
		NoFilePathGiven,
		FileExists,
		FileDoesNotExist,
	}
	//////////////////////////////


	// TextTransformer
	public enum SplitCharacter
	{
		Comma,
	}
	//////////////////////////////
}
