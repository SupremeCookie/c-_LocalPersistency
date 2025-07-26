namespace Persistency
{
	public interface IDataIO
	{
		T GetValue<T>(string key, T defaultValue, out GetValueResult result);
		void SetValue<T>(string key, T value, out SetValueResult result);
	}
}
