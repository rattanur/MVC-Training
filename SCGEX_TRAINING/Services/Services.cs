using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 
namespace SCGEX_TRAINING.Services
{
  public class FileLogWriter : ILogWriter
    {
        public void Write(string message)
    {
      var dt = DateTime.Now;
      var fileName = $"Log {dt:yyyy-MM-dd-HH}.txt";
      var s = $"{dt:s}: {message}{Environment.NewLine}";
      File.AppendAllText(fileName, s);
    }
  }
}