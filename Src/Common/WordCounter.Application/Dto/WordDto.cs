using Mapster;
using WordCounter.Domain.Entities;

namespace WordCounter.Application.Dto
{
    public class WordDto: IRegister
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public void Register(TypeAdapterConfig config) {
            config.NewConfig<Word, WordDto>();
        }
    }
}
