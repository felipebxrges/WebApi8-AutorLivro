using Microsoft.EntityFrameworkCore;
using WebApi8_AutorLivro.Data;
using WebApi8_AutorLivro.Dto.Livro;
using WebApi8_AutorLivro.Models;

namespace WebApi8_AutorLivro.Services.Livro
{
    public class LivroService : ILivroInterface
    {

        private readonly AppDbContext _context;
        
        public LivroService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                
                var livro = await _context.Livros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == idLivro);

                if( livro == null )
                {
                    resposta.Mensagem = "Este livro não foi encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado com sucesso!";
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .Include(x => x.Autor)
                    .Where(x => x.Autor.Id == idAutor)
                    .ToListAsync();

                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado com sucesso!";
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var autorExiste = await _context.Autores.FirstOrDefaultAsync(x => x.Id == livroCriacaoDto.Autor.Id);

                if(autorExiste == null)
                {
                    resposta.Mensagem = "Este autor não existe";
                    resposta.Status = false;
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autorExiste
                };

                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(x => x.Autor).ToListAsync();
                resposta.Mensagem = "Livro criado com sucesso!";
                resposta.Status = true;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .FirstOrDefaultAsync(x => x.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores
                    .FirstOrDefaultAsync(x => x.Id == livroEdicaoDto.Autor.Id);


                if(autor == null)
                {
                    resposta.Mensagem = "Este autor não existe!";
                    resposta.Status = true;
                    return resposta;
                }

                
                if(livro == null)
                {
                    resposta.Mensagem = "Este livro não foi encontrado!";
                    resposta.Status = false;
                    return resposta;
                }

                livro.Autor = autor;
                livro.Titulo = livroEdicaoDto.Titulo;

                _context.Livros.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(x => x.Autor).ToListAsync();
                resposta.Mensagem = "Livro editado com sucesso!";
                resposta.Status = true;

                return resposta;

            }
            catch ( Exception ex ) 
            { 
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
           ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(x => x.Id == idLivro);

                if(livro == null)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Livro não encontrado!";
                    return resposta;
                }

                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Status = true;
                resposta.Mensagem = "Livro excluído com sucesso!";
                resposta.Dados = await _context.Livros.Include(x => x.Autor).ToListAsync();

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }          
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livros = await _context.Livros.Include(x => x.Autor).ToListAsync();

                if(livros == null)
                {
                    resposta.Mensagem = "Não há livros registrados!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Livros listados com sucesso!";
                resposta.Status = true;
               
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
