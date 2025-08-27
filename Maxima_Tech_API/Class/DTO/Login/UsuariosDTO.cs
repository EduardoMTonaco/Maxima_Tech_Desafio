using Maxima_Tech_API.Class.Attributes;
using Maxima_Tech_API.Class.DTO.Base;
using System.Security.Cryptography;
using Utility;
namespace Maxima_Tech_API.Class.DTO.Login
{
    public class UsuariosDTO : BaseDTO
    {
        private int _id = int.MinValue;
        private string _username;
        private string _encyptPassword;
        private string _email;
        private string _nomeCompleto;
        [DisplayAttributes(true, "id", 0)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayAttributes(false, "username", 1)]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [DisplayAttributes(false, "encyptPassword", 2)]
        public string EncyptPassword
        {
            get { return _encyptPassword; }
            set { _encyptPassword = value; }
        }

        [DisplayAttributes(false, "email", 2)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DisplayAttributes(false, "NomeCompleto", 2)]
        public string NomeCompleto
        {
            get { return _nomeCompleto; }
            set { _nomeCompleto = value; }
        }

        public override string SelectCommand()
        {
            return CreateSelectCommand(this);
        }

        public override string Table()
        {
            return "usuarios";
        }

        public override void FillSubClass()
        {

        }
        public override string UpdateCommand()
        {
            return Update(this);
        }

        public override string InsertCommand()
        {
            return Insert(this);
        }
        public void ResetPassword()
        {
            _encyptPassword = "********";
        }
    }
}
