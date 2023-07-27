using Microsoft.VisualStudio.TestTools.UnitTesting;
using servico_certificado.Application.Services;
using servico_certificado.Domain.Entities;
using servico_certificado.Infrastructure.Contexto;
using servico_certificado.Infrastructure.Repositories;
using servico_certificado.Infrastructure.Utilities;
using System.Threading.Tasks;


namespace test
{
[TestClass]
public class CertificadoServiceTests
{
    [TestMethod]
    public void EmitirCertificado_Returns_PdfByteArray()
    {
        var _geradorCertificadoPDF = new GeradorCertificadoPDF();
        var certificadoService = new CertificadoService(_geradorCertificadoPDF);
        string nome = "João da Silva";
        string curso = "Matemática Avançada";
        string cpf = "123.456.789-00";

        // Act
        byte[] pdfByteArray = certificadoService.EmitirCertificado(nome, curso, cpf);

        // Assert
        Assert.IsNotNull(pdfByteArray);
        Assert.IsTrue(pdfByteArray.Length > 0);
       
    }

    [TestMethod]
    public async Task SalvarDadosCertificado_SavesDataToRepository()
    {
        // Arrange
        var _geradorCertificadoPDF = new GeradorCertificadoPDF();
        var certificadoService = new CertificadoService(_geradorCertificadoPDF);
        var dadosCertificado = new CertificadoAluno
        {
            Nome = "Nome do Aluno",
            Curso = "Curso de Teste",
            Cpf = "123.456.789-00"
        };

        // Act
        await certificadoService.SalvarDadosCertificado(dadosCertificado);

    }
}

}
