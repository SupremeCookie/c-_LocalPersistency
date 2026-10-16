using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Persistency.Encryption
{
	public class Constants : MonoBehaviour
	{
		public static int key = 129;
		public static string encryptionPrefix = "FAF**$%mnb";
		public static string unreadableEncryptionPrefix => SimpleXOREncryption.EncryptDecrypt(encryptionPrefix);
	}
}