using BLL.DbContext.Interface;
using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DbContext
{
    public class DataAccessRepository : IDataAccessRepository
    {
        #region Private proporities
        private readonly ISqlDataAccess _db;
        #endregion

        #region Constructor
        public DataAccessRepository(ISqlDataAccess sqlDataAccess)
        {
            _db = sqlDataAccess;
        }
        #endregion

        #region Writing to File
        private string usernameTextFile = "UsernamesTextFile.txt";

        //This cannot be tested! As it contains more than one method and is basically a caller. Instead what can be done is
        //Integration testing where an actual test file can be used to test this method.
        //Instead.. what should be tested is, each method inside this method to make sure it will be working properly when performing integration testing it.
        public void AddNewPerson(UserModel newUser)
        {

            List<UserModel> users = GetAllUsers();

            AddNewUserToList(users, newUser);

            List<string> listOfLines = GetStringListOfUsers(users);

            File.WriteAllLines(usernameTextFile, listOfLines);
        }

        #region internal methods
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string[] content = File.ReadAllLines(usernameTextFile);

            foreach (string line in content)
            {
                string[] parts = line.Split(',');
                users.Add(new UserModel { FirstName = parts[0], LastName = parts[1] });
            }
            return users;
        }

        public void AddNewUserToList(List<UserModel> users, UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("You cannot pass an empty or null value", "FirstName");
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("You cannot pass an empty or null value", "LastName");
            }
            users.Add(user);
        }
        public List<string> GetStringListOfUsers(List<UserModel> users)
        {
            List<string> fileLines = new List<string>();
            foreach (var user in users)
            {
                fileLines.Add($"{user.FirstName},{user.LastName}");
            }
            return fileLines;
        }


        #endregion
        #endregion

        #region Writing to DB
        public List<UserModel> LoadData()
        {
            string sql = "select * from tbl_users";

            return _db.LoadData<UserModel>(sql);
        }

        public void SaveData(UserModel user)
        {
            string sql = "insert into tbl_users (FirstName,LastName) values (@FirstName,@LastName)";

            _db.SaveData(user, sql);
        }
        #endregion
    }
}
