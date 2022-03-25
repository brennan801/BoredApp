using BoredShared.Models;

namespace BoredWebAppAdmin.Models
{
    public class NewUserSnapShot
    {
        private readonly Id ID;
        private readonly UserName UserName;
        private readonly byte[] Salt;
        private readonly string Hash;

        public NewUserSnapShot(Id id, UserName userName, byte[] salt, string hash)
        {
            ID = id;
            UserName = userName;
            Salt = salt;
            Hash = hash;
        }
        public Id GetId() { return ID; }
        public UserName GetUserName() { return UserName; }
        public byte[] GetSalt() { return Salt; }
        public string GetHash() { return Hash; }
    }
}
