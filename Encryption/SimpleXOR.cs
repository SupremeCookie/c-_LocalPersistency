using System;
using System.Collections.Generic;
using System.Text;
using Persistency.Encryption;


namespace Persistency
{
	// https://www.nullskull.com/a/780/simple-xor-encryption.html
	public static class SimpleXOREncryption
	{
		public static string EncryptDecrypt(string textToEncrypt)
		{
			StringBuilder inSb = new StringBuilder(textToEncrypt);
			StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
			char c;
			for (int i = 0; i < textToEncrypt.Length; i++)
			{
				c = inSb[i];
				c = (char)(c ^ Constants.key);
				outSb.Append(c);
			}
			return outSb.ToString();
		}
	}
}