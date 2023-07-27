using System.Collections.Generic;
using System.Threading.Tasks;
using servico_certificado.Domain.Entities;

namespace servico_certificado.Domain.interfaces;
public interface IDadosCertificadoRepository
{
    Task<CertificadoAluno> GetByIdAsync(int id);
    Task<IEnumerable<CertificadoAluno>> GetAllAsync();
    Task AddAsync(CertificadoAluno dadosCertificado);
}
