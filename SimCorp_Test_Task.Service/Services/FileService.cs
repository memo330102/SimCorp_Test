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
                Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
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
                return File.ReadLines(path);
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
                return null as IEnumerable<string>;
            }
            catch (FileNotFoundException ex)
            {
                Log.Error($"FileNotFoundException: {ErrorMessages.File_Not_Found_At_Path}");
                return null as IEnumerable<string>;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
                return null as IEnumerable<string>;
            }
        }

        public bool IsFileExist(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
                return false;
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
                File.WriteAllText(path, content);
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
            }
            catch (FileNotFoundException ex)
            {
                Log.Error($"FileNotFoundException: {ErrorMessages.File_Not_Found_At_Path}");
            }
            catch (Exception ex)
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

        public void CreateAndWriteToFile(string path,string content)
        {
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    Byte[] text = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(text, 0, text.Length);
                }
            }
            catch (ArgumentNullException ex)
            {
                Log.Error($"ArgumentNullException: {ErrorMessages.File_Path_Can_Not_Be_Null}");
            }
            catch (PathTooLongException ex)
            {
                Log.Error($"PathTooLongException: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                Log.Error($"DirectoryNotFoundException: {ex.Message}");
            }
            catch (IOException ex)
            {
                Log.Error($"IOException: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Log.Error($"NotSupportedException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: {ex.Message}");
            }
        }

    }
}
