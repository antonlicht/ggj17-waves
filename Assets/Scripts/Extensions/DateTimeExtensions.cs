using System;

namespace Shared.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime FromUnixTimeStamp (int unixTimeStamp)
		{
			var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dt = dt.AddSeconds(unixTimeStamp);
			return dt;
		}

		public static int ToUnixTimeStamp (this DateTime time)
		{
			return (int)((time - new DateTime(1970, 1, 1)).TotalSeconds);
		}
	}
}
