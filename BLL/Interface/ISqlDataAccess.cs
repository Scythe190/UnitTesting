using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ISqlDataAccess
    {
        public List<UserModel> LoadData<UserModel>(string sql);
        public void SaveData(UserModel model,string sql);  
    }
}
