using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre= _context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }
            if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() ))
            {
                throw new InvalidOperationException("Aynı İsimde Bir Kitap Türü Zaten var");
            }
            _mapper.Map<UpdateGenreModel,Genre>(Model, genre);
            //genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            //genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
