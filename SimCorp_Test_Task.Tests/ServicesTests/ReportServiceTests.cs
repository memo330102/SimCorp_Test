using FluentAssertions;
using Moq;
using SimCorp_Test_Task.Service.Interfaces;
using SimCorp_Test_Task.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimCorp_Test_Task.Tests.ServicesTests
{
    public class ReportServiceTests : IClassFixture<ReportService>, IClassFixture<FileService>
    {
        private readonly ReportService _reportService;
        private readonly FileService _fileService;
        private readonly Mock<IFile> _fileServiceMock;
        public ReportServiceTests(ReportService reportService , FileService fileService)
        {
            _fileServiceMock = new Mock<IFile>();
            _reportService = reportService;
            _fileService = fileService;
        }

        [Fact]
        public void ReportService_CountWords_Should_Return_Word_Count_When_File_Exists()
        {
            var filePath = "test.txt";
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

            //_fileService.WriteAllText(filePath, fileData);
            _fileServiceMock.Setup(fs => fs.WriteAllText(filePath, fileData));

            _fileServiceMock.Setup(fs => fs.ReadLines(filePath)).Returns(expectedReadData);

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

            _fileService.DeleteFile(filePath);
        }

        [Fact]
        public void ReportService_CountWords_Should_Return_Empty_Dictionary_When_File_Does_Not_Exist()
        {
            var filePath = "nonexistent.txt";

            _fileServiceMock.Setup(fs => fs.ReadLines(filePath)).Returns(new string[0]);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void ReportService_CountWords_Should_Return_Empty_Dictionary_When_FileService_Returns_Null()
        {
            var filePath = "test.txt";

            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Returns((IEnumerable<string>)null);

            var result = _reportService.CountWords(filePath);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
