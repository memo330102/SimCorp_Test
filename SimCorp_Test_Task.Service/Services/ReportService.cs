using Serilog;
using SimCorp_Test_Task.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorp_Test_Task.Service.Services
{
    public class ReportService : IReport
    {
        private IFile _fileService;
        public ReportService()
        {
            _fileService = new FileService();
        }
        public Dictionary<string, int> CountWords(string filePath)
        {

            var dataFromFile = _fileService.ReadFromFile(filePath);

            try
            {
                return dataFromFile
                    .SelectMany(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(word => new string(word.Where(char.IsLetter).ToArray()).ToLower())
                    .Where(cleanedWord => !string.IsNullOrWhiteSpace(cleanedWord))
                    .GroupBy(cleanedWord => cleanedWord)
                    .ToDictionary(group => group.Key, group => group.Count());
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
                //Console.WriteLine($"Exception: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }

    }
}
