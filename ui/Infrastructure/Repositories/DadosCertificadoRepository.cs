using Microsoft.EntityFrameworkCore;
using servico_certificado.Domain.Entities;
using servico_certificado.Domain.interfaces;
using servico_certificado.Infrastructure.Contexto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace servico_certificado.Infrastructure.Repositories
{
    public class DadosCertificadoRepository : IDadosCertificadoRepository
    {
        private readonly BancoDeDadosContexto _context;
        public DadosCertificadoRepository(BancoDeDadosContexto context)
        {
             _context = context;
        }

        async Task<CertificadoAluno> IDadosCertificadoRepository.GetByIdAsync(int id)
        {
           return await _context.CertificadosAlunos.FindAsync(id);
        }

        async Task<IEnumerable<CertificadoAluno>> IDadosCertificadoRepository.GetAllAsync()
        {
             return await _context.CertificadosAlunos.ToListAsync();
        }

        async public Task AddAsync(CertificadoAluno dadosCertificado)
        {
            await _context.CertificadosAlunos.AddAsync(dadosCertificado);
            await _context.SaveChangesAsync();
        }
    }
}
