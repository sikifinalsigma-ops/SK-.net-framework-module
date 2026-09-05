using System;
using BCrypt.Net;

namespace SK_Encrypt
{
    public class BCryptEncryptionHelper
    {
        // 推荐工作因子 (Work Factor / Cost Factor)：11 或 12 (耗时约 100~250ms)
        private const int WorkFactor = 12;

        /// <summary>
        /// 【用户注册 / 修改密码】将前端传来的密码字串进行 BCrypt 加盐慢速杂凑
        /// </summary>
        /// <param name="clientPasswordHash">前端传入的密码字串 (如 SHA-256 散列)</param>
        /// <returns>长度为 60 字元的 FinalDbHash (存入资料库)</returns>
        public static string HashPassword(string clientPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(clientPasswordHash))
                throw new ArgumentException("密码字串不能为空", nameof(clientPasswordHash));

            // 内部会自动生成随机 Salt，并输出含版本、Cost、Salt、Hash 的 60 字元字串
            return BCrypt.Net.BCrypt.HashPassword(clientPasswordHash, workFactor: WorkFactor);
        }

        /// <summary>
        /// 【用户登入】验证前端传来的密码字串与资料库中存的 Hash 是否匹配
        /// </summary>
        /// <param name="clientPasswordHash">前端传入的密码字串 (如 SHA-256 散列)</param>
        /// <param name="storedDbHash">资料库中储存的 60 字元 BCrypt Hash</param>
        /// <returns>比对成功回传 true，否则回传 false</returns>
        public static bool VerifyPassword(string clientPasswordHash, string storedDbHash)
        {
            if (string.IsNullOrWhiteSpace(clientPasswordHash) || string.IsNullOrWhiteSpace(storedDbHash))
                return false;

            try
            {
                // BCrypt.Verify 会自动从 storedDbHash 拆解 Salt，并使用常数时间比对防范 Timing Attack
                return BCrypt.Net.BCrypt.Verify(clientPasswordHash, storedDbHash);
            }
            catch
            {
                // 防范传入非法的 BCrypt 格式字串导致抛出例外
                return false;
            }
        }
    }
}
