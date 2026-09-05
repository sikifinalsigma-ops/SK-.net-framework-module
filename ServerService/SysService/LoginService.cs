using SK_DataAccess;
using SK_DataEntity;
using SK_DataEntity.Entity;
using SK_Encrypt;
using SK_Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerService.SysService
{
    public class LoginService
    {        
        public bool CheckLogin(string userName,string userPwd,out SK_SYS_USER userEntity) 
        {
            userEntity = null;
            try
            {                
                using (OracleRepository db = new DatabaseConnect().DbConnect() as OracleRepository)
                {
                    SK_SYS_USER user = db.QueryNoTracking<SK_SYS_USER>().Where(x=>x.USER_ACCOUNT == userName).FirstOrDefault();
                    if (user == null)
                    {
                        return false;
                    }
                    if (!BCryptEncryptionHelper.VerifyPassword(userPwd, user.USER_PASSWORD))
                    {
                        return false;
                    }

                    userEntity = user;
                    return true;
                }
            }
            catch
            {
                throw;
            }
            finally 
            {
                
            }
        }


        public SK_SYS_USER GetLoginUser(string userName)
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect() as OracleRepository)
                {
                    SK_SYS_USER user = db.QueryNoTracking<SK_SYS_USER>().Where(x => x.USER_ACCOUNT == userName).FirstOrDefault();
                    return user;
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public bool regesterUser(string userName,string userPwd) 
        {
            string BCryptCode = BCryptEncryptionHelper.HashPassword(userPwd);
            return false;
        }
    }
}
