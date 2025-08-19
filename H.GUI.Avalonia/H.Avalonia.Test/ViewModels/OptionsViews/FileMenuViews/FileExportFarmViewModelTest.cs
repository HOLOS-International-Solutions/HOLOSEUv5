﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using H.Avalonia.ViewModels.OptionsViews.FileMenuViews;
using H.Core.Models;
using H.Core.Services.StorageService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Regions;

namespace H.Avalonia.Test.ViewModels.OptionsViews.FileMenuViews.Tests
{
    [TestClass]
    public class FileExportFarmViewModelTest
    {
        private Mock<IStorageService> _mockStorageService;
        private Mock<IRegionManager> _mockRegionManager;
        private Mock<IStorageFile> _mockFile;
        private Mock<Stream> _mockStream;
        private FileExportFarmViewModel _viewModel;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize mocks
            _mockStorageService = new Mock<IStorageService>();
            _mockRegionManager = new Mock<IRegionManager>();
            _mockFile = new Mock<IStorageFile>();
            _mockStream = new Mock<Stream>();

            // Setup the file mock to return our stream mock
            _mockFile.Setup(f => f.OpenWriteAsync()).ReturnsAsync(_mockStream.Object);
            _mockStream.SetupGet(s => s.CanWrite).Returns(true);

            _viewModel = new FileExportFarmViewModel(_mockRegionManager.Object, _mockStorageService.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _viewModel = null;
            _mockStorageService = null;
            _mockRegionManager = null;
            _mockFile = null;
            _mockStream = null;
        }

        [TestMethod]
        public async Task TestExportAsync_WithValidFile_CallsOpenWriteAsync()
        {
            // Arrange
            var testFarms = new List<Farm>
            {
                new Farm { Name = "Test Farm 1" },
                new Farm { Name = "Test Farm 2" }
            };
            _viewModel.SelectedFarms = testFarms;

            // Act
            await _viewModel.ExportAsync(_mockFile.Object);

            // Assert
            _mockFile.Verify(file => file.OpenWriteAsync(), Times.Once);
        }

        [TestMethod]
        public async Task TestExportAsync_WithSelectedFarms_SerializesCorrectly()
        {
            // Arrange
            var testFarms = new List<Farm>
            {
                new Farm { Name = "Test Farm 1" },
                new Farm { Name = "Test Farm 2" }
            };
            _viewModel.SelectedFarms = testFarms;

            // Create a custom MemoryStream that captures data before disposal
            var capturedData = new byte[0];
            var customMemoryStream = new TestMemoryStream();
            customMemoryStream.OnDispose = (stream) => {
                capturedData = stream.ToArray();
            };

            _mockFile.Setup(f => f.OpenWriteAsync()).ReturnsAsync(customMemoryStream);

            // Act
            await _viewModel.ExportAsync(_mockFile.Object);

            // Assert
            Assert.IsTrue(capturedData.Length > 0, "Stream should contain serialized data");
            
            // Verify the stream was written to
            var content = Encoding.UTF8.GetString(capturedData);
            Assert.IsFalse(string.IsNullOrEmpty(content), "Serialized content should not be empty");
            
            // Verify the content contains expected farm data
            Assert.IsTrue(content.Contains("Test Farm 1"), "Serialized content should contain first farm name");
            Assert.IsTrue(content.Contains("Test Farm 2"), "Serialized content should contain second farm name");
        }

        // Helper class to capture data before disposal
        private class TestMemoryStream : MemoryStream
        {
            public Action<MemoryStream> OnDispose { get; set; }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    OnDispose?.Invoke(this);
                }
                base.Dispose(disposing);
            }
        }

        [TestMethod]
        public async Task TestExportAsync_WithEmptySelectedFarms_StillExports()
        {
            // Arrange
            _viewModel.SelectedFarms = new List<Farm>();

            // Act
            await _viewModel.ExportAsync(_mockFile.Object);

            // Assert
            _mockFile.Verify(file => file.OpenWriteAsync(), Times.Once);
        }

        [TestMethod]
        public async Task TestExportAsync_WithNullFile_HandlesGracefully()
        {
            // Arrange
            var testFarms = new List<Farm>
            {
                new Farm { Name = "Test Farm" }
            };
            _viewModel.SelectedFarms = testFarms;

            // Act & Assert
            // The method should handle null file gracefully and not throw
            // (based on the try-catch implementation in the ExportAsync method)
            await _viewModel.ExportAsync(null);
            
            // If we reach here, the method handled the null file gracefully
            Assert.IsTrue(true, "Method should handle null file gracefully");
        }

        [TestMethod]
        public async Task TestExportAsync_WhenStreamThrowsException_HandlesGracefully()
        {
            // Arrange
            var testFarms = new List<Farm>
            {
                new Farm { Name = "Test Farm" }
            };
            _viewModel.SelectedFarms = testFarms;

            _mockFile.Setup(f => f.OpenWriteAsync()).ThrowsAsync(new IOException("File access denied"));

            // Act
            await _viewModel.ExportAsync(_mockFile.Object);

            // Assert
            // The method should handle the exception gracefully and not throw
            // (based on the try-catch implementation in the ExportAsync method)
            _mockFile.Verify(file => file.OpenWriteAsync(), Times.Once);
        }
    }
}