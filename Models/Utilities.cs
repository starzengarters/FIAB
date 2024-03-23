namespace FIAB.Models
{
	public class Utilities
	{
		public static string Env(string key, bool required=false, IConfiguration? config = null, string defaultValue = "")
		{
			// Prefer Environment values
			var value = System.Environment.GetEnvironmentVariable(key);
			
			// We can fallback to the appsettings/Secrets file(s).
			if(string.IsNullOrWhiteSpace(value) && config != null)
			{
				value = config.GetValue<string>(key);
			}
			if(string.IsNullOrWhiteSpace(value))
			{
				if (required)
				{
					throw new Exception($"Could not find ENV/Secret: {key}");
				}
				return defaultValue;
			}
			return value;
		}
	}
}