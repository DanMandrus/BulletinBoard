using BulletinBoard.DataAccess;
using BulletinBoard.DataAccess.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace BulletinBoard.UnitTests.Repository;

[TestFixture]
[Category("Integration")]
public class UserRepositoryTests : RepositoryTestsBaseClass
{
    [Test]
    public void GetAllUsersTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Title = "My User",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        var users = new UserEntity[]
        {
            new UserEntity()
            {
                Birthday = new DateTime(2000, 12, 1),
                UserId = User.Id,
                FirstName = "Test1",
                SecondName = "Test12",
                Patronymic = "Test123",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Birthday = new DateTime(2001, 11, 2),
                UserId = User.Id,
                FirstName = "Test2",
                SecondName = "Test22",
                Patronymic = "Test223",
                ExternalId = Guid.NewGuid(),
            },
        };
        context.Users.AddRange(users);
        context.SaveChanges();
        
        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll();
      
        actualUsers.Should().BeEquivalentTo(users, options => options.Excluding(x => x.User));
    }

    [Test]
    public void GetAllUsersWithFilterTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Title = "My User",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        var users = new UserEntity[]
        {
            new UserEntity()
            {
                Birthday = new DateTime(2000, 12, 1),
                UserId = User.Id,
                FirstName = "Test1",
                SecondName = "Test12",
                Patronymic = "Test123",
                ExternalId = Guid.NewGuid()
            },
            new UserEntity()
            {
                Birthday = new DateTime(2001, 11, 2),
                UserId = User.Id,
                FirstName = "Test2",
                SecondName = "Test22",
                Patronymic = "Test223",
                ExternalId = Guid.NewGuid(),
            },
        };
        context.Users.AddRange(users);
        context.SaveChanges();
        //execute

        var repository = new Repository<UserEntity>(DbContextFactory);
        var actualUsers = repository.GetAll(x => x.FirstName == "Test1").ToArray();

        //assert
        actualUsers.Should().BeEquivalentTo(users.Where(x => x.FirstName == "Test1"),
            options => options.Excluding(x => x.User));
    }

    [Test]
    public void SaveNewUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Title = "My User",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        //execute

        var user = new UserEntity()
        {
            Birthday = new DateTime(2000, 12, 1),
            UserId = User.Id,
            FirstName = "Test1",
            SecondName = "Test12",
            Patronymic = "Test123"            
        };
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.User)
            .Excluding(x => x.Id)
            .Excluding(x => x.ModificationTime)
            .Excluding(x => x.CreationTime)
            .Excluding(x => x.ExternalId));
        actualUser.Id.Should().NotBe(default);
        actualUser.ModificationTime.Should().NotBe(default);
        actualUser.CreationTime.Should().NotBe(default);
        actualUser.ExternalId.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void UpdateUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Title = "My User",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        var user = new UserEntity()
        {
            Birthday = new DateTime(2000, 12, 1),
            UserId = User.Id,
            ExternalId = Guid.NewGuid(),
            FirstName = "Test1",
            SecondName = "Test12",
            Patronymic = "Test123"
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute

        user.FirstName = "new name1";
        user.SecondName = "new name2";
        user.Patronymic = "new name3";
        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Save(user);

        //assert
        var actualUser = context.Users.SingleOrDefault();
        actualUser.Should().BeEquivalentTo(user, options => options.Excluding(x => x.User));
    }


    [Test]
    public void DeleteUserTest()
    {
        //prepare
        using var context = DbContextFactory.CreateDbContext();
        var User = new UserEntity()
        {
            Title = "My User",
            ExternalId = Guid.NewGuid()
        };
        context.Users.Add(User);
        context.SaveChanges();

        var user = new UserEntity()
        {
            Birthday = new DateTime(2000, 12, 1),
            UserId = User.Id,
            ExternalId = Guid.NewGuid(),
            FirstName = "Test1",
            SecondName = "Test12",
            Patronymic = "Test123"
        };
        context.Users.Add(user);
        context.SaveChanges();

        //execute

        var repository = new Repository<UserEntity>(DbContextFactory);
        repository.Delete(user);

        //assert
        context.Users.Count().Should().Be(0);
    }

    [SetUp]
    public void SetUp()
    {
        CleanUp();
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();
    }

    public void CleanUp()
    {
        using (var context = DbContextFactory.CreateDbContext())
        {
            context.Users.RemoveRange(context.Users);
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }
    }
}