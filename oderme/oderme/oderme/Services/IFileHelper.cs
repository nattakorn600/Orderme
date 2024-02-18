using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace oderme.Services
{
	public interface IFileHelper
	{
		string GetLocalFilePath(string filename);
		Task SaveFileToDefaultLocation(string fileName, byte[] bytes);
	}
}
