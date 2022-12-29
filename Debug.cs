namespace Persistency
{
	// Note DK: The persistency namespace can be used in many environments, this means we do not always have access to UnityEngine debug functionality.
	public static class Debug
	{
#if UNITY_5_3_OR_NEWER
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			UnityEngine.Debug.Assert(condition, FormatMessage(message));
		}
#else
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			throw new System.NotSupportedException("No implementation for Debug.Assert written yet");
		}
#endif


#if UNITY_5_3_OR_NEWER
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void Log(string message)
		{
			UnityEngine.Debug.Log(FormatMessage(message));
		}
#else
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void Log(string message)
		{
			throw new System.NotSupportedException("No implementation for Debug.Log written yet");
		}
#endif


#if UNITY_5_3_OR_NEWER
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void LogWarning(string message)
		{
			UnityEngine.Debug.LogWarning(FormatMessage(message));
		}
#else
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void LogWarning(string message)
		{
			throw new System.NotSupportedException("No implementation for Debug.LogWarning written yet");
		}
#endif


#if UNITY_5_3_OR_NEWER
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void LogError(string message)
		{
			UnityEngine.Debug.LogError(FormatMessage(message));
		}
#else
		[System.Diagnostics.ConditionalAttribute("DEBUG")]
		public static void LogError(string message)
		{
			throw new System.NotSupportedException("No implementation for Debug.LogError written yet");
		}
#endif



#if UNITY_5_3_OR_NEWER
		private static string FormatMessage(string message)
		{
			return string.Format("<color=#00FF00>Persistency System</color> {0}", message);
		}
#endif
	}
}
