using servico_certificado.Domain.Entities;
using servico_certificado.Domain.interfaces;
using System.Threading.Tasks;

namespace servico_certificado.Domain.Services
{
    public class DadosCertificadoService
    {
        private readonly IDadosCertificadoRepository _dadosCertificadoRepository;

        public DadosCertificadoService(IDadosCertificadoRepository dadosCertificadoRepository)
        {
            _dadosCertificadoRepository = dadosCertificadoRepository;
        }

        public async Task SaveDadosCertificado(CertificadoAluno dadosCertificado)
        {
            await _dadosCertificadoRepository.AddAsync(dadosCertificado);
        }
    }
}
