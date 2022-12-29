namespace Persistency
{
	public interface IFileExistence
	{
		void DoesFileExist(out FileExistsResult existsResult);
		void CreateDefaultFile(Extension fileExtension, out CreationResult creationResult);
	}
}
