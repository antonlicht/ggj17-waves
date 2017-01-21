using System.Collections.Generic;
using System.IO;

namespace Shared.Extensions
{
	public static class FileIoExtensions
	{
		public static string[] GetDirectoryNamesAbove (this DirectoryInfo dirInfo)
		{
			var dirNames = new List<string>();
			while (dirInfo.Parent != null)
			{
				dirNames.Add(dirInfo.Parent.Name);
				dirInfo = dirInfo.Parent;
			}
			return dirNames.ToArray();
		}
	}
}