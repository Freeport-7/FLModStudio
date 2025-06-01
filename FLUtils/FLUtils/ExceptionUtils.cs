using System;
using System.Text;

namespace FLUtils;

public class ExceptionUtils
{
	public static string Get(Exception exception, bool includeStackTrace = false)
	{
		StringBuilder stringBuilder = new StringBuilder(exception.Message);
		if (exception.InnerException != null)
		{
			stringBuilder.Append(Environment.NewLine + Environment.NewLine + Get(exception.InnerException));
		}
		if (includeStackTrace)
		{
			stringBuilder.Append(Environment.NewLine + Environment.NewLine + "Stack Trace: " + exception.StackTrace);
		}
		return stringBuilder.ToString();
	}
}
