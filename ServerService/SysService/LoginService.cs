using Newtonsoft.Json;
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
    public static class LoginService
    {        
        public static bool CheckLogin(string userName,string userPwd,out SK_SYS_USER userEntity) 
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


        public static SK_SYS_USER GetLoginUser(string userAccount)
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect() as OracleRepository)
                {
                    SK_SYS_USER user = db.QueryNoTracking<SK_SYS_USER>().Where(x => x.USER_ACCOUNT == userAccount).FirstOrDefault();
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

        public static bool registerUser(string userName,string userPwd) 
        {
            string BCryptCode = BCryptEncryptionHelper.HashPassword(userPwd);
            return false;
        }

        public static bool ChangePassword(SK_SYS_USER userEntity, string newPassword) 
        {
            using (OracleRepository db = new DatabaseConnect().DbConnect() as OracleRepository) 
            {
                userEntity.USER_PASSWORD = BCryptEncryptionHelper.HashPassword(newPassword);
                db.UpdateAndSaveEntity(userEntity);
            }
            
            return true;
        }

        
    }
}
