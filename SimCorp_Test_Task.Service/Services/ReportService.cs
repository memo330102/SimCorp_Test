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
        public ReportService(IFile fileService)
        {
            _fileService = fileService;
        }
        public Dictionary<string, int> CountWords(string filePath)
        {
            try
            {
                var dataFromFile = _fileService.ReadFromFile(filePath);
                var words = SplitWords(dataFromFile);
                var cleanedWords = NormalizeAsOnlyWords(words);
                var wordCount = GroupWords(cleanedWords);

                return wordCount;
            }
            catch (IOException ex)
            {
                Log.Error($"IOException: {ex.Message}");
                return new Dictionary<string, int>();
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
                return new Dictionary<string, int>();
            }
        }
        private IEnumerable<string> SplitWords(IEnumerable<string> lines)
        {
            return lines.SelectMany(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        private IEnumerable<string> NormalizeAsOnlyWords(IEnumerable<string> words)
        {
            return words.Select(word => new string(word.Where(char.IsLetter).ToArray()).ToLower())
                        .Where(cleanedWord => !string.IsNullOrWhiteSpace(cleanedWord));
        }

        private Dictionary<string, int> GroupWords(IEnumerable<string> words)
        {
            return words.GroupBy(cleanedWord => cleanedWord)
                        .ToDictionary(group => group.Key, group => group.Count());
        }


    }
}
