using FluentAssertions;
using Moq;
using SimCorp_Test_Task.Service.Interfaces;
using SimCorp_Test_Task.Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimCorp_Test_Task.Tests.ServicesTests
{
    public class FileServiceTests
    {
        private readonly FileService _fileService;
        private readonly Mock<ISort> _sortServiceMock;
        private string fileFolder = AppDomain.CurrentDomain.BaseDirectory;
        public FileServiceTests()
        {
            _sortServiceMock = new Mock<ISort>();
            _fileService = new FileService(_sortServiceMock.Object);
        }

        [Fact]
        public void FileService_ReadFromFile_Should_Return_Data_When_File_Exists()
        {
            var filePath = Path.Combine(fileFolder, "text.txt");
            var fileData = "Go do that thing that you do so well";

            _fileService.CreateAndWriteToFile(filePath, fileData);
            var result = _fileService.ReadFromFile(filePath);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(fileData);
        }

        [Fact]
        public void FileService_ReadFromFile_Should_Return_Null_When_File_Path_Is_Null()
        {
            string filePath = null;

            var result = _fileService.ReadFromFile(filePath);

            result.Should().BeNull();
        }


        [Fact]
        public void FileService_ReadFromFile_Should_Return_Null_When_File_Is_Not_Found()
        {
            var filePath = "nonexistentfile.txt";

            var result = _fileService.ReadFromFile(filePath);

            result.Should().BeNull();
        }

        [Fact]
        public void FileService_PrintWordsToConsole_Should_Not_Print_Anything_When_WordCount_Is_Null()
        {
            Dictionary<string, int> wordCount = null;
            IOrderedEnumerable<KeyValuePair<string, int>> keyValues = null;
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _sortServiceMock.Setup(fs => fs.OrderByDescending(wordCount)).Returns(keyValues);

            _fileService.PrintWordsToConsole(wordCount);

            consoleOutput.ToString().Should().BeEmpty();
        }

        [Fact]
        public void FileService_PrintWordsToConsole_Should_Print_Word_Count_When_WordCount_Is_Not_Null()
        {
            var wordCount = new Dictionary<string, int>
            {
                { "word1", 5 },
                { "word2", 3 },
                { "word3", 8 }
            };

            var sortedWordCount = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("word3", 8),
                new KeyValuePair<string, int>("word1", 5),
                new KeyValuePair<string, int>("word2", 3)
            };

            _sortServiceMock.Setup(s => s.OrderByDescending(wordCount)).Returns(sortedWordCount.OrderByDescending(kv => kv.Value));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _fileService.PrintWordsToConsole(wordCount);

            var expectedOutput = $"8: word3{Environment.NewLine}5: word1{Environment.NewLine}3: word2{Environment.NewLine}";
            consoleOutput.ToString().Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("text.txt", true)]
        [InlineData("nonexistentfile.txt", false)]
        [InlineData(null, false)]
        public void FileService_IsFileExist_Should_Return_Correct_Value(string filePath, bool expectedExistence)
        {

            var fullFilePath = filePath is not null ? Path.Combine(fileFolder, filePath) : filePath;

            var result = _fileService.IsFileExist(fullFilePath);

            result.Should().Be(expectedExistence);
        }

        [Theory]
        [InlineData("existingfile.txt", "Go do that thing that you do so well")]
        public void FileService_WriteAllText_Should_Write_Content_To_File(string filePath, string content)
        {
            var fullFilePath = Path.Combine(fileFolder, filePath);

            _fileService.WriteAllText(fullFilePath, content);

            File.ReadAllText(filePath).Should().Be(content);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("nonexistentfile.txt")]
        public void FileService_DeleteFile_Should_Not_Throw_Exception_When_File_Does_Not_Exist(string filePath)
        {
            var fullFilePath = filePath is not null ? Path.Combine(fileFolder, filePath) : filePath;

            _fileService.Invoking(fs => fs.DeleteFile(fullFilePath)).Should().NotThrow<FileNotFoundException>();
        }

        [Fact]
        public void FileService_DeleteFile_Should_Delete_File_When_Exists()
        {
            var fullFilePath = Path.Combine(fileFolder, "existingfile.txt");

            File.WriteAllText(fullFilePath, "Go do that thing that you do so well");

            _fileService.DeleteFile(fullFilePath);

            File.Exists(fullFilePath).Should().BeFalse();
        }

        [Fact]
        public void FileService_CreateAndWriteToFile_Should_Create_File_With_Content()
        {
            var fullFilePath = Path.Combine(fileFolder, "newfile.txt");
            var content = "Go do that thing that you do so well";

            _fileService.CreateAndWriteToFile(fullFilePath, content);

            File.Exists(fullFilePath).Should().BeTrue();
            File.ReadAllText(fullFilePath).Should().Be(content);
        }
    }
}
