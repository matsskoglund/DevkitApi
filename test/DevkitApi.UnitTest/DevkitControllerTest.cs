using System;
using Xunit;
using DevkitApi.Controllers;
using DevkitApi.Services;
using Moq;
using Microsoft.Extensions.Logging;
using DevkitApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevkitApi.UnitTest
{
    public class DevkitsControllerTest
    {
        protected DevkitsController ControllerUnderTest { get; }
        protected Mock<IDevkitService> DevkitServiceMock { get; }
        protected Mock<ILogger<DevkitsController>> LoggerMock { get;  }

        public  DevkitsControllerTest() { 
            DevkitServiceMock = new Mock<IDevkitService>(); // DevkitServiceMock mock
            LoggerMock = new Mock<ILogger<DevkitsController>>(); // Logger
            ControllerUnderTest = new DevkitsController(DevkitServiceMock.Object, LoggerMock.Object);
        }

        public class ReadAllAsync : DevkitsControllerTest
        {
            [Fact]
            public void Should_return_OkObjectResult_with_a_list_devkits()
            {
                // Arrange
                var expectedDevkits = new Devkit[]
                {
                    new Devkit { Name = "Test clan 1" },
                    new Devkit { Name = "Test clan 2" },
                    new Devkit { Name = "Test clan 3" }
                };
                DevkitServiceMock
                    .Setup(x => x.GetDevkits())
                    .Returns(expectedDevkits); // Mocked the ReadAllAsync() method

                // Act
                var result = ControllerUnderTest.GetDevkits();

                // Assert
                // var okResult = Assert.IsType<IEnumerable<Devkit>>(result);
                Assert.Same(expectedDevkits, result);
                //Assert.NotSame(expectedDevkits, result);
            }
        }

       
}
}
