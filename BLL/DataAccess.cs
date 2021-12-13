using BLL.DbContext.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DataAccess
    {
        private readonly IDataAccessRepository _dataAccessRepository;
        private string usernameTextFile = "UsernamesTextFile.txt";

        public DataAccess(IDataAccessRepository dataAccessRepository)
        {
            _dataAccessRepository = dataAccessRepository;
        }

        //Violates First SOLID_P which is the Single Responsibility Principle
        //public static void AddNewPerson(UserModel newUser)
        //{
        //    List<string> fileLines = new List<string>();

        //    List<UserModel> users = new List<UserModel>();
        //    string[] content = File.ReadAllLines(usernameTextFile);

        //    foreach (string line in content)
        //    {
        //        string[] parts = line.Split(',');
        //        users.Add(new UserModel { FirstName = parts[0], LastName = parts[1] });
        //    }

        //    foreach (UserModel user in users)
        //    {
        //        fileLines.Add($"{user.FirstName},{user.LastName}");
        //    }


        //    File.WriteAllLines(usernameTextFile, fileLines);
        //}

        //Follows Single Responsibility Principle

        public string AddNewPerson(UserModel newUser)
        {
            List<UserModel> users = _dataAccessRepository.GetAllUsers();

            _dataAccessRepository.AddNewUserToList(users, newUser);

            List<string> listOfLines = _dataAccessRepository.GetStringListOfUsers(users);

            //_dataAccessRepository.WriteUsersToFile(listOfLines);


            return "added new user";
        }


        #region internal methods
        public List<UserModel> GetAllUsers()
        {
           return _dataAccessRepository.GetAllUsers();
        }

        public void AddNewUserToList(List<UserModel> users, UserModel user)
        {
            _dataAccessRepository.AddNewUserToList(users,user);
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

        public void WriteUsersToFile(List<string> fileLines)
        {
            File.WriteAllLines(usernameTextFile, fileLines);
        }

        #endregion
    }
}

