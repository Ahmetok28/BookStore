using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorFullNameIsGıven_InvalidOperationExeption_ShouldBeReturn()
        {
            var author = new Author()
            {
                Name = "Test1234",
                SurName = "Test1234",
                BirthDate = DateTime.Now.AddYears(-28)

            };
            context.Add(author);
            context.SaveChanges();

            CreateAuthorCommand cmd = new CreateAuthorCommand(context,mapper);
            cmd.Model= new CreateAuthorModel() { Name=author.Name,SurName=author.SurName};

            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Zaten Ekli");
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            CreateAuthorCommand cmd = new CreateAuthorCommand(context, mapper);
            CreateAuthorModel model = new CreateAuthorModel() {Name="Test1",SurName="Test1", BirthDate = DateTime.Now.AddYears(-28) };
            cmd.Model = model;

            FluentActions.Invoking(() => cmd.Handle()).Invoke();

            var author  = context.Authors.SingleOrDefault(x => x.Name.ToLower() == model.Name.ToLower() && x.SurName.ToLower() == model.SurName.ToLower());

            author.Should().NotBeNull();
            author.SurName.Should().Be(model.SurName);
            author.Name.Should().Be(model.Name);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}
