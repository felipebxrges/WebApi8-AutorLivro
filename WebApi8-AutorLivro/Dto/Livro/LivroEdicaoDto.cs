using WebApi8_AutorLivro.Dto.Vinculo;
using WebApi8_AutorLivro.Models;

namespace WebApi8_AutorLivro.Dto.Livro
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
