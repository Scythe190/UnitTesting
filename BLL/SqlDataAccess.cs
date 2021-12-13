using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public List<UserModel> LoadData<UserModel>(string sql)
        {
            //Loads data from DB
            throw new NotImplementedException();
        }

        public void SaveData(UserModel model, string sql)
        {
            //Save data to DB
            throw new NotImplementedException();
        }
    }
}
