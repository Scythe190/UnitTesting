using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DbContext.Interface
{
    public interface IDataAccessRepository
    {
        #region File
        public void AddNewPerson(UserModel newUser);
        public List<UserModel> GetAllUsers();
        public List<string> GetStringListOfUsers(List<UserModel> listOfUsers);
        public void AddNewUserToList(List<UserModel> users, UserModel user);
        #endregion

        #region DB
        public List<UserModel> LoadData();

        public void SaveData(UserModel user);
        #endregion
    }
}
