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
    public class FileServiceTests : IClassFixture<FileService>
    {
        private FileService _fileService;
        private readonly Mock<IFile> _fileServiceMock;
        public FileServiceTests(FileService fileService)
        {
            _fileServiceMock = new Mock<IFile>(MockBehavior.Strict);
            _fileService = fileService;
        }

        [Fact]
        public void FileService_ReadFromFile_Should_Return_Null_When_File_Does_Not_Exist()
        {
            var filePath = "nonexist.txt";

            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Throws<FileNotFoundException>();

            var result = _fileService.ReadFromFile(filePath);

            result.Should().BeNull();
        }

        [Fact]
        public void FileService_ReadFromFile_Should_Return_Null_When_Exception_Occurs()
        {
            var filePath = "test.txt";

            _fileServiceMock.Setup(fs => fs.ReadFromFile(filePath)).Throws<Exception>();

            var result = _fileService.ReadFromFile(filePath);

            result.Should().BeNull();
        }
    }
}
