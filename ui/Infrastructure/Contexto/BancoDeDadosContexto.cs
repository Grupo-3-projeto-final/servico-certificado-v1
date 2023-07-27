using Microsoft.EntityFrameworkCore;
using servico_certificado.Domain.Entities;
namespace servico_certificado.Infrastructure.Contexto;

public class BancoDeDadosContexto : DbContext
{
        private const string Name = "conexao";

        public DbSet<CertificadoAluno> CertificadosAlunos { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<CertificadoAluno>(entity =>
            {
                entity.ToTable("CertificadosAlunos"); 
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd().HasColumnName("Cert_id");
                entity.Property(e => e.GeradoData).IsRequired(); 
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString(Name) ?? "";
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
}