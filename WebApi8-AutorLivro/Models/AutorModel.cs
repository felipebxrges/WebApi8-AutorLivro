using System.Text.Json.Serialization;

namespace WebApi8_AutorLivro.Models
{
    public class AutorModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [JsonIgnore]  //para nao criar os livros na hora de criar os autores
        public ICollection<LivroModel>  Livros{ get; set; }
    }
}
