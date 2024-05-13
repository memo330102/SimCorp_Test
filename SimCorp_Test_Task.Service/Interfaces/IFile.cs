using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorp_Test_Task.Service.Interfaces
{
    public interface IFile
    {
        public IEnumerable<string> ReadFromFile(string filePath);
        void PrintWordsToConsole(Dictionary<string, int> wordCount);
        public IEnumerable<string> ReadLines(string path);
        public void WriteAllText(string path, string content);
        public void DeleteFile(string filePath);
        public bool IsFileExist(string path);
        public void CreateAndWriteToFile(string path, string content);
    }
}
