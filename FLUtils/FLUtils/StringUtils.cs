using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FLUtils;

public static class StringUtils
{
	private static uint[] createIDTable;

	private static uint[] createFactionIDTable;

	public static string SplitPascalCase(this string convert)
	{
		return Regex.Replace(Regex.Replace(convert, "(\\P{Ll})(\\P{Ll}\\p{Ll})", "$1 $2"), "(\\p{Ll})(\\P{Ll})", "$1 $2");
	}

	public static int CountOccurrences(this string val, string match)
	{
		return Regex.Matches(val, match, (RegexOptions)1).Count;
	}

	public static string Truncate(this string s, int length)
	{
		if (!string.IsNullOrEmpty(s) && length > 0)
		{
			if (s.Length <= length)
			{
				return s;
			}
			return s.Substring(0, length) + "...";
		}
		return string.Empty;
	}

	public static string TruncateAtWord(this string s, int length)
	{
		if (s != null && s.Length >= length && s.IndexOf(" ", length, StringComparison.Ordinal) != -1)
		{
			return s.Substring(0, s.IndexOf(" ", length, StringComparison.Ordinal));
		}
		return s;
	}

	public static bool IsEmailAddress(this string email)
	{
		return ((Group)Regex.Match(email, "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$")).Success;
	}

	public static bool IsDateTime(this string data, string format)
	{
		DateTime result;
		return DateTime.TryParseExact(data, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
	}

	public static uint CreateId(string nickname)
	{
		if (createIDTable == null)
		{
			createIDTable = new uint[256];
			for (uint num = 0u; num < 256; num++)
			{
				uint num2 = num;
				for (uint num3 = 0u; num3 < 8; num3++)
				{
					num2 = (((num2 & 1) == 1) ? ((num2 >> 1) ^ 0x28004000) : (num2 >> 1));
				}
				createIDTable[num] = num2;
			}
		}
		byte[] bytes = Encoding.ASCII.GetBytes(nickname.ToLowerInvariant());
		uint num4 = 0u;
		for (int i = 0; i < bytes.Length; i++)
		{
			num4 = (num4 >> 8) ^ createIDTable[(byte)num4 ^ bytes[i]];
		}
		num4 = (num4 >> 24) | ((num4 >> 8) & 0xFF00) | ((num4 << 8) & 0xFF0000) | (num4 << 24);
		return (num4 >> 2) | 0x80000000u;
	}

	public static uint CreateFactionId(string nickname)
	{
		if (createFactionIDTable == null)
		{
			createFactionIDTable = new uint[256];
			for (uint num = 0u; num < 256; num++)
			{
				uint num2 = num << 8;
				for (uint num3 = 0u; num3 < 8; num3++)
				{
					num2 = (((num2 & 0x8000) == 32768) ? ((num2 << 1) ^ 0x1021) : (num2 << 1));
					num2 &= 0xFFFF;
				}
				createFactionIDTable[num] = num2;
			}
		}
		byte[] bytes = Encoding.ASCII.GetBytes(nickname.ToLowerInvariant());
		uint num4 = 65535u;
		for (uint num5 = 0u; num5 < bytes.Length; num5++)
		{
			num4 = ((num4 & 0xFF00) >> 8) ^ createFactionIDTable[(num4 & 0xFF) ^ bytes[num5]];
		}
		return num4;
	}
}
