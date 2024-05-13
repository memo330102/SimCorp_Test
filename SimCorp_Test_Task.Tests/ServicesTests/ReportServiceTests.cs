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
    public class ReportServiceTests 
    {
        private readonly ReportService _reportService;
        private readonly Mock<IFile> _fileServiceMock;
        private string fileFolder = AppDomain.CurrentDomain.BaseDirectory;

        public ReportServiceTests()
        {
            _fileServiceMock = new Mock<IFile>();
            _reportService = new ReportService(_fileServiceMock.Object);
        }

        [Fact]
        public void ReportService_CountWords_Should_Return_Word_Count_When_File_Exists()
        {
            var filePath = Path.Combine(fileFolder, "test.txt");
            var fileData = "Go do that thing that you do so well"; 
            var expectedReadData = new[]
            {
                "Go",
                "do",
                "that",
                "thing",
                "that",
                "you",
                "do",
                "so",
                "well"
            };

            _fileServiceMock.Setup(fs => fs.WriteAllText(filePath, fileData));
            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns(expectedReadData);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().HaveCount(7);
            result.Should().ContainKey("do").WhoseValue.Should().Be(2);
            result.Should().ContainKey("that").WhoseValue.Should().Be(2);
            result.Should().ContainKey("go").WhoseValue.Should().Be(1);
            result.Should().ContainKey("so").WhoseValue.Should().Be(1);
            result.Should().ContainKey("thing").WhoseValue.Should().Be(1);
            result.Should().ContainKey("well").WhoseValue.Should().Be(1);
            result.Should().ContainKey("you").WhoseValue.Should().Be(1);
        }

        [Fact]
        public void ReportService_CountWords_Should_Return_Empty_Dictionary_When_File_Does_Not_Exist()
        {
            var filePath = "nonexistent.txt";

            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns(new string[0]);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
            result.Should().HaveCount(0);
        }

        [Fact]
        public void ReportService_CountWords_Should_Return_Empty_Dictionary_When_FileService_Returns_Null()
        {
            var filePath = "test.txt";

            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns((IEnumerable<string>)null);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
            result.Should().HaveCount(0);
        }

        [Fact]
        public void ReportService_CountWords_Should_Ignore_Empty_Lines()
        {
            var filePath = Path.Combine(fileFolder, "fileWithEmptyLines.txt");
            var fileData = "The\n\nquick\n\n\nbrown fox\n\njumps\nover the lazy\n\ndog.";
            var expectedReadData = new[]
            {                
                "The",
                "",
                "quick",
                "",
                "",
                "brown fox",
                "",
                "jumps",
                "over the lazy",
                "",
                "dog"
            };

            _fileServiceMock.Setup(fs => fs.WriteAllText(filePath,fileData));
            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns(expectedReadData);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().HaveCount(8);
            result.Should().ContainKey("the").WhoseValue.Should().Be(2);
            result.Should().ContainKey("quick").WhoseValue.Should().Be(1);
            result.Should().ContainKey("brown").WhoseValue.Should().Be(1);
            result.Should().ContainKey("fox").WhoseValue.Should().Be(1);
            result.Should().ContainKey("jumps").WhoseValue.Should().Be(1);
            result.Should().ContainKey("dog").WhoseValue.Should().Be(1);
        }

        [Fact]
        public void ReportService_CountWords_Should_Count_Words_Ignoring_Punctuation()
        {
            var filePath = Path.Combine(fileFolder, "fileWithPunctuation.txt");
            var fileData = "The quick, brown fox jumps over the lazy dog.";
            var expectedReadData = new[]
            {
                "The",
                "quick",
                "brown",
                "fox",
                "jumps",
                "over",
                "the",
                "lazy",
                "dog"
            };

            _fileServiceMock.Setup(fs => fs.WriteAllText(filePath, fileData));
            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns(expectedReadData);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().HaveCount(8);
            result.Should().ContainKey("the").WhoseValue.Should().Be(2);
            result.Should().ContainKey("quick").WhoseValue.Should().Be(1);
            result.Should().ContainKey("brown").WhoseValue.Should().Be(1);
            result.Should().ContainKey("fox").WhoseValue.Should().Be(1);
            result.Should().ContainKey("jumps").WhoseValue.Should().Be(1);
            result.Should().ContainKey("over").WhoseValue.Should().Be(1);
            result.Should().ContainKey("lazy").WhoseValue.Should().Be(1);
            result.Should().ContainKey("dog").WhoseValue.Should().Be(1);
        }

        [Fact]
        public void ReportService_CountWords_Should_Count_Words_Ignoring_Number()
        {
            var filePath = Path.Combine(fileFolder, "fileWithNumber.txt");
            var fileData = "The quick23, 4533 brown fox1 jumps 35346 over the434 lazy dog.";
            var expectedReadData = new[]
            {
                "The",
                "quick",
                "brown",
                "fox",
                "jumps",
                "over",
                "the",
                "lazy",
                "dog"
            };

            _fileServiceMock.Setup(fs => fs.WriteAllText(filePath, fileData));
            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns(expectedReadData);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().HaveCount(8);
            result.Should().ContainKey("the").WhoseValue.Should().Be(2);
            result.Should().ContainKey("quick").WhoseValue.Should().Be(1);
            result.Should().ContainKey("brown").WhoseValue.Should().Be(1);
            result.Should().ContainKey("fox").WhoseValue.Should().Be(1);
            result.Should().ContainKey("jumps").WhoseValue.Should().Be(1);
            result.Should().ContainKey("over").WhoseValue.Should().Be(1);
            result.Should().ContainKey("lazy").WhoseValue.Should().Be(1);
            result.Should().ContainKey("dog").WhoseValue.Should().Be(1);
        }
    }
}
