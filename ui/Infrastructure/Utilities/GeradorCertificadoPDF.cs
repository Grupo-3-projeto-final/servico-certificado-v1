using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace servico_certificado.Infrastructure.Utilities
{
    public class GeradorCertificadoPDF
    {
        public byte[] GerarCertificado(string nome, string curso, string cpf)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (var writer = new PdfWriter(ms))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf, PageSize.A4.Rotate()))
                {
                    // Carregue a imagem do logo
                    string logoFilePath = "/home/ubuntu/imagem-test/logo/PlanoFundoCertificado.png";
                    Image logo = new Image(ImageDataFactory.Create(logoFilePath));

                    // Obtenha o tamanho da página
                    Rectangle pageSize = document.GetPdfDocument().GetDefaultPageSize();

                    // Defina o tamanho da imagem como o tamanho da página
                    logo.ScaleToFit(pageSize.GetWidth(), pageSize.GetHeight());

                    // Defina a posição da imagem como plano de fundo
                    logo.SetFixedPosition(0, 0);

                    // Adicione a imagem ao documento como plano de fundo
                    document.Add(logo);
                     document.Add(new Paragraph("\n\n\n"));
                      document.Add(new Paragraph("\n\n\n"));
                       document.Add(new Paragraph("\n\n\n"));

                    // Adicione o conteúdo do certificado
                    string titulo = "CERTIFICADO";
                    document.Add(new Paragraph(titulo).SetFontSize(32).SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("\n\n\n"));

                    string textoCertificado = "Certificamos que {0} concluiu o curso de {1}.";
                    string textoCPF = "CPF: {0}";

                    Paragraph certificado = new Paragraph(string.Format(textoCertificado, nome.ToUpper(), curso.ToUpper()))
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER);
                    document.Add(certificado);

                    Paragraph cpfParagrafo = new Paragraph(string.Format(textoCPF, cpf))
                        .SetFontSize(16)
                        .SetTextAlignment(TextAlignment.CENTER);
                    document.Add(cpfParagrafo);
                }

                return ms.ToArray();
            }
        }
    }
}

