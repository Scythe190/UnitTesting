using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BLL.DbContext.Interface;
using Microsoft.Extensions.DependencyInjection;
using BLL.DbContext;
using BLL.Interface;

namespace BLL.Tests
{
    public class DataAccessTests
    {
        #region Dependency Injection
        private readonly IDataAccessRepository _dataAccessRepository;
        private readonly ISqlDataAccess _db;
        public DataAccessTests()
        {
            var services = new ServiceCollection();

            services.AddScoped<IDataAccessRepository, DataAccessRepository>();
            services.AddScoped<ISqlDataAccess, SqlDataAccess>();

            var serviceProvider = services.BuildServiceProvider();

            _dataAccessRepository = serviceProvider.GetService<IDataAccessRepository>();
            _db = serviceProvider.GetService<ISqlDataAccess>();
        }
        #endregion

        #region File
        //This goes against a single unit of work. Should not be tested. (Rather integration tested)
        [Fact]
        public void AddNewPerson_TestingAddingNewPersonAsAWhole()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void AddNewUserToList_AddingNewUserShouldWork()
        {
            //Arrange
            UserModel user = new UserModel { FirstName = "Mahmoud", LastName = "Hamad" };

            //Act 
            List<UserModel> users = new List<UserModel>();
            _dataAccessRepository.AddNewUserToList(users, user);

            //Assert
            Assert.Contains<UserModel>(user, users);
        }

        [Theory]
        [InlineData("", "Ahmed", "FirstName")]
        [InlineData("Mohammed", "", "LastName")]
        public void AddNewUserToList_AddingNewUserShouldFailAndThrowException(string firstName, string lastName, string parameter)
        {
            //Arrange
            List<UserModel> users = new List<UserModel>();
            UserModel user = new UserModel { FirstName = firstName, LastName = lastName };

            //Assert
            Assert.Throws<ArgumentException>(parameter, () => _dataAccessRepository.AddNewUserToList(users, user));
        }
        #endregion

        #region Database
        [Fact]
        public void LoadData_LoadingMockDbResults()
        {
            //Arrange
            Mock<ISqlDataAccess> dbMock = new();

            dbMock.Setup(
                x => x.LoadData<UserModel>("select * from tbl_users")).Returns(GetSampleData());

            var expected = GetSampleData();

            //Act
            DataAccessRepository obj = new(dbMock.Object);
            var actual = obj.LoadData(); 

            //Assert
            Assert.Equal(expected.Count, actual.Count); 
        }

        [Fact]
        public void SaveData_SavingMockDbResults()
        {
            //Arrange
            Mock<ISqlDataAccess> dbMock = new();
            var user = GetSampleData()[0];
            string sql = "insert into tbl_users (FirstName,LastName) values (@FirstName,@LastName)";

            dbMock.Setup(
               x => x.SaveData(user, sql));

            //Act
            DataAccessRepository obj = new(dbMock.Object);
            obj.SaveData(user);

            //Assert
            dbMock.Verify(x => x.SaveData(user, sql), Times.Once);

        }

        #region private methods
        private List<UserModel> GetSampleData()
        {
            List<UserModel> output = new List<UserModel>
            {
               new UserModel
               {
                   FirstName = "Abdulaziz",
                   LastName = "Hachem"
               },
               new UserModel
               {
                   FirstName = "Ran",
                   LastName = "Out"
               },
               new UserModel
               {
                   FirstName = "Of",
                   LastName = "Names"
               }
            };
            return output;
        }
        #endregion
        #endregion
    }
}
