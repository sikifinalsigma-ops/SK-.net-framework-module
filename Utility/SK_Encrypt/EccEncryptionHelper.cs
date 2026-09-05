using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;

namespace SK_Encrypt
{
    public class EccEncryptionHelper
    {
        /// <summary>
        /// 生成 Ed25519 公私金钥对 (各 32 bytes)
        /// </summary>
        public static void GenerateKeyPair(out byte[] publicKey, out byte[] privateKey)
        {
            var keyPairGen = new Ed25519KeyPairGenerator();
            keyPairGen.Init(new Ed25519KeyGenerationParameters(new SecureRandom()));

            var keyPair = keyPairGen.GenerateKeyPair();
            var pubParam = (Ed25519PublicKeyParameters)keyPair.Public;
            var privParam = (Ed25519PrivateKeyParameters)keyPair.Private;

            publicKey = pubParam.GetEncoded();    // 32 bytes
            privateKey = privParam.GetEncoded();  // 32 bytes
        }

        /// <summary>
        /// 使用私钥对资料进行 Ed25519 签名
        /// </summary>
        /// <param name="data">待签名原始资料</param>
        /// <param name="privateKey">32 字节私钥</param>
        /// <returns>64 字节签名结果</returns>
        public static byte[] Sign(byte[] data, byte[] privateKey)
        {
            var privKeyParam = new Ed25519PrivateKeyParameters(privateKey, 0);
            var signer = new Ed25519Signer();
            signer.Init(true, privKeyParam); // true 表示签名模式
            signer.BlockUpdate(data, 0, data.Length);

            return signer.GenerateSignature(); // 固定 64 bytes
        }

        /// <summary>
        /// 使用公钥验证 Ed25519 签名
        /// </summary>
        /// <param name="data">原始资料</param>
        /// <param name="signature">64 字节签名</param>
        /// <param name="publicKey">32 字节公钥</param>
        /// <returns>验证是否通过</returns>
        public static bool Verify(byte[] data, byte[] signature, byte[] publicKey)
        {
            var pubKeyParam = new Ed25519PublicKeyParameters(publicKey, 0);
            var verifier = new Ed25519Signer();
            verifier.Init(false, pubKeyParam); // false 表示验证模式
            verifier.BlockUpdate(data, 0, data.Length);

            return verifier.VerifySignature(signature);
        }
    }
}
