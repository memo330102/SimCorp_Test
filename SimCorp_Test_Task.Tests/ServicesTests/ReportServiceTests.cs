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
    public class ReportServiceTests : IClassFixture<ReportService>, IClassFixture<FileService>
    {
        private readonly ReportService _reportService;
        private readonly FileService _fileService;
        private readonly Mock<IFile> _fileServiceMock;
        public ReportServiceTests(ReportService reportService, FileService fileService)
        {
            _fileServiceMock = new Mock<IFile>();
            _reportService = reportService;
            _fileService = fileService;
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
