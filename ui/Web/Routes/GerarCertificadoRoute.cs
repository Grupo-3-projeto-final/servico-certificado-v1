using IdentityGama.Filters;
using Microsoft.AspNetCore.Mvc;
using servico_certificado.Application.Entities;
using servico_certificado.Domain.Entities;
using servico_certificado.Infrastructure;
using servico_certificado.Infrastructure.Utilities;

namespace servico_certificado.Web.Routes
{
    public class GerarCertificadoRoute
    {
        private readonly WebApplication _app;
        private readonly GeradorCertificadoPDF _geradorCertificadoPDF;
        private readonly CertificadoService _certificado;

        public GerarCertificadoRoute(WebApplication app)
        {
            _app = app;
            _geradorCertificadoPDF = new GeradorCertificadoPDF();
            _certificado = new CertificadoService(_geradorCertificadoPDF);
        }

        public void Register()
        {
           
            _app.MapPost("/gerar-certificado", [Authentication] (HttpContext context, [FromBody] DadosCertificado dados) =>
            {
                var pdfBytes = _certificado.EmitirCertificado(dados.Nome, dados.Curso, dados.CPF);

                var dadosCertificado = new CertificadoAluno
                {
                    Nome = dados.Nome,
                    Curso = dados.Curso,
                    Cpf = dados.CPF
                };

                _certificado.SalvarDadosCertificado(dadosCertificado);

                context.Response.ContentType = "application/pdf";
                context.Response.Headers.Add("Content-Disposition", "attachment; filename=Certificado.pdf");
                return context.Response.Body.WriteAsync(pdfBytes, 0, pdfBytes.Length);
            })
            .WithName("PostGerarCertificado")
            .WithOpenApi();
        }
    }
}
