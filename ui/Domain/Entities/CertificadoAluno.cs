namespace servico_certificado.Domain.Entities;

public record CertificadoAluno
{
    public int Id {get;set;}
    public string Nome {get; set;}
    public string Curso {get; set;}
    public string Cpf {get; set;}
    public DateTime GeradoData  { get; set; }

}