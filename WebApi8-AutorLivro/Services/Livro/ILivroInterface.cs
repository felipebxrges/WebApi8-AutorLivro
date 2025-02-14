using Microsoft.AspNetCore.Mvc;
using WebApi8_AutorLivro.Models;
using WebApi8_AutorLivro.Dto.Livro;

namespace WebApi8_AutorLivro.Services.Livro
{
    public interface ILivroInterface
    {
        public Task<ResponseModel<List<LivroModel>>> ListarLivros();
        public Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
        public Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);
        public Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);
        public Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        public Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
 
    }
}
