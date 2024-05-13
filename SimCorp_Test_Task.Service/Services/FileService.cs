using Serilog;
using SimCorp_Test_Task.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorp_Test_Task.Service.Services
{
    public class FileService : IFile
    {
        private ISort _sorter;
        public FileService()
        {
            _sorter = new SortService();
        }
        public IEnumerable<string> ReadFromFile(string filePath)
        {
            try
            {
                var data = ReadLines(filePath);
                return data;
            }
            catch (FileNotFoundException ex)
            {
                Log.Error($"FileNotFoundException: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
                return null;
            }
        }

        public void PrintWordsToConsole(Dictionary<string, int> wordCount)
        {
            try
            {
                var sortedWordCount = _sorter.OrderByDescending(wordCount);

                foreach (var (word, count) in sortedWordCount)
                {
                    Console.WriteLine($"{count}: {word}");
                }
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"ArgumentNullException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
            }
        }

        public IEnumerable<string> ReadLines(string path)
        {
            try
            {
                if (path == null)
                {
                    Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
                    throw new ArgumentNullException(nameof(path), ErrorMessages.File_Path_Can_Not_Be_Null);
                }

                if (!IsFileExist(path))
                {
                    Log.Error($"FileNotFoundException: {ErrorMessages.File_Not_Found_At_Path}");
                    throw new FileNotFoundException(ErrorMessages.File_Not_Found_At_Path + ": {path}");
                }

                return File.ReadLines(path);
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
                return null as IEnumerable<string>;
            }
        }

        public bool IsFileExist(string path)
        {
            if (path == null)
            {
                Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
                throw new ArgumentNullException(nameof(path), ErrorMessages.File_Path_Can_Not_Be_Null);
            }

            try
            {
                return File.Exists(path);
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
                return false;
            }
        }

        public void WriteAllText(string path, string content)
        {
            try
            {
                if (path == null)
                {
                    Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
                    throw new ArgumentNullException(nameof(path), ErrorMessages.File_Path_Can_Not_Be_Null);
                }

                if (!IsFileExist(path))
                {
                    Log.Error($"FileNotFoundException: {ErrorMessages.File_Not_Found_At_Path}");
                    throw new FileNotFoundException(ErrorMessages.File_Not_Found_At_Path + ": {path}");
                }

                if (string.IsNullOrEmpty(content))
                {
                    Log.Error($"ArgumentNullException: {ErrorMessages.Content_Can_Not_Be_Null_Or_Empty}");
                    throw new ArgumentNullException(nameof(content), ErrorMessages.Content_Can_Not_Be_Null_Or_Empty);
                }

                File.WriteAllText(path, content);
            }
            catch(Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                if (path == null)
                {
                    Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
                    throw new ArgumentNullException(nameof(path), ErrorMessages.File_Path_Can_Not_Be_Null);
                }

                if (!IsFileExist(path))
                {
                    Log.Error($"FileNotFoundException: {ErrorMessages.File_Not_Found_At_Path}");
                    throw new FileNotFoundException(ErrorMessages.File_Not_Found_At_Path + ": {path}");
                }

                File.Delete(path);
            }
            catch(Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
            }
        }
    }
}
