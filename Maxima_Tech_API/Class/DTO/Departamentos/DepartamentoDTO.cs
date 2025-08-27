using Maxima_Tech_API.Class.Attributes;
using Maxima_Tech_API.Class.DTO.Base;

namespace Maxima_Tech_API.Class.DTO.Departamentos
{
    public class DepartamentoDTO : BaseDTO
    {
        private int _id = int.MinValue;
        private string _nome;
        [DisplayAttributes(true, "id", 0)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [DisplayAttributes(false, "nome", 1)]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public override string SelectCommand()
        {
            return CreateSelectCommand(this);
        }

        public override string Table()
        {
            return "departamentos";
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
    }
}
