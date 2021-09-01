using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using tododavid.Common.Models;
using tododavid.Functions.Functions;
using tododavid.Test.Helpers;
using Xunit;

namespace tododavid.Test.Test
{
    public class TodoApiTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void CreateTodo_Should_Return_200()
        {
            //Arrenge: Prepar prueba unitaria.
            MockCloudTableTodos mockTodos = new MockCloudTableTodos(new Uri("http://127.0.0.1:10002/devstoreaccount1/reports"));
            Todo todoRequest = TestFactory.GetTodoRequest();
            DefaultHttpRequest request = TestFactory.CreateHttpRequest(todoRequest);

            //Act: ejecutamos prueba unitaria.
            IActionResult response = await TodoApi.CreateTodo(request, mockTodos, logger);

            //Assert: verficamos si la prueba unitaria nos dió el resultado correcto.
            OkObjectResult result = (OkObjectResult)response;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async void UpdateTodo_Should_Return_200()
        {
            //Arrenge: Prepar prueba unitaria.
            MockCloudTableTodos mockTodos = new MockCloudTableTodos(new Uri("http://127.0.0.1:10002/devstoreaccount1/reports"));
            Todo todoRequest = TestFactory.GetTodoRequest();
            Guid todoId = Guid.NewGuid();
            DefaultHttpRequest request = TestFactory.CreateHttpRequest(todoId, todoRequest);

            //Act: ejecutamos prueba unitaria.
            IActionResult response = await TodoApi.UpdateTodo(request, mockTodos, todoId.ToString(), logger);

            //Assert: verficamos si la prueba unitaria nos dió el resultado correcto.
            OkObjectResult result = (OkObjectResult)response;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
