using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using servico_certificado.Application.Entities;
using servico_certificado.Infrastructure;
using servico_certificado.Infrastructure.Utilities;
using servico_certificado.Web.Routes;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    [TestClass]
    public class GerarCertificadoRouteTests
    {
        [TestMethod]
        public async Task TestGerarCertificadoRoute()
        {
            var dadosCertificado = new DadosCertificado
            {
                Nome = "John Doe",
                Curso = "Sample Course",
                CPF = "12345678900"
            };

        
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(c => c.RequestServices).Returns(new ServiceCollection().BuildServiceProvider());

       
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dadosCertificado)));
            var request = new Mock<HttpRequest>();
            request.SetupGet(r => r.Method).Returns("POST");
            request.SetupGet(r => r.Path).Returns("/gerar-certificado");
            request.SetupGet(r => r.ContentType).Returns("application/json");
            request.SetupGet(r => r.Body).Returns(memoryStream);
            mockHttpContext.SetupGet(c => c.Request).Returns(request.Object);

    
            var responseStream = new MemoryStream();
            var response = new Mock<HttpResponse>();
            response.SetupGet(r => r.ContentType).Returns("application/pdf");
            response.Setup(r => r.Headers).Returns(new HeaderDictionary());
            response.SetupGet(r => r.Body).Returns(responseStream);
            mockHttpContext.SetupGet(c => c.Response).Returns(response.Object);

            // Act
            //var gerarCertificadoRoute = new GerarCertificadoRoute();
            //gerarCertificadoRoute.Register(new WebApplicationBuilder().Build());

      
            await request.Object.HttpContext.RequestServices.GetRequiredService<IRouter>().RouteAsync(new RouteContext(mockHttpContext.Object));

            // Assert
            Assert.AreEqual("application/pdf", response.Object.ContentType);
            Assert.IsTrue(response.Object.Headers.ContainsKey("Content-Disposition"));

  
            responseStream.Position = 0;
            var responseContent = await new StreamReader(responseStream).ReadToEndAsync();
      
        }
    }
}



