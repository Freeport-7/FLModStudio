using System;
using System.Diagnostics;
using System.Reflection;

namespace FLUtils;

public class AssemblyUtils
{
	public static string Name => Assembly.GetCallingAssembly().GetName().Name;

	public static Version Version => Assembly.GetCallingAssembly().GetName().Version;

	public static string Company => FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).CompanyName;

	public static string Description => FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).FileDescription;

	public static string Copyright => FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).LegalCopyright;
}
