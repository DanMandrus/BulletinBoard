using System.Linq.Expressions;
using BulletinBoard.BL.User;
using BulletinBoard.BL.UnitTests.Mapper;
using BulletinBoard.DataAccess;
using BulletinBoard.DataAccess.Entities;
using Moq;
using NUnit.Framework;

namespace BulletinBoard.BL.UnitTests.Users;

[TestFixture]
public class UsersProviderTests
{
    [Test]
    public void TestGetAllUsers()
    {
        Expression expression = null;
        Mock<IRepository<UserEntity>> UsersRepository = new Mock<IRepository<UserEntity>>();
        UsersRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .Callback((Expression<Func<UserEntity, bool>> x) => { expression = x; });
        var UsersProvider = new UsersProvider(UsersRepository.Object, MapperHelper.Mapper, 18);
        var Users = UsersProvider.GetUsers();

        UsersRepository.Verify(x => x.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()), Times.Exactly(1));
    }
}