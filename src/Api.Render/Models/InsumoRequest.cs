using System.ComponentModel.DataAnnotations;

public class InsumoRequest
{
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    public string Nome { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A 'Quantidade' deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O 'Custo' deve ser maior que zero.")]
    public decimal Custo { get; set; }
}
