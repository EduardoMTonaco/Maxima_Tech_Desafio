using Maxima_Tech_API.Class.Attributes;
using Maxima_Tech_API.Class.DTO.Base;
using Maxima_Tech_API.Class.DTO.Departamentos;
using System.Net.NetworkInformation;

namespace Maxima_Tech_API.Class.DTO.Produtos
{
    public class ProdutoDTO : BaseDTO
    {
        private Guid _id;
        private string _nome;
        private string _descricao;
        private int _departamentoId = int.MinValue;
        private decimal _preco = decimal.MinValue;
        private bool _status = true;
        private DepartamentoDTO _departamento =   new DepartamentoDTO();

        [DisplayAttributes(true, "id", 0)]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayAttributes(false, "Nome", 1)]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        [DisplayAttributes(false, "descricao", 2)]
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        [DisplayAttributes(false, "departamentoId", 3)]
        public int DepartamentoId
        {
            get { return _departamentoId; }
            set { _departamentoId = value; }
        }
        [DisplayAttributes(false, "preco", 4)]
        public decimal Preco
        {
            get { return _preco; }
            set { _preco = value; }
        }
        [DisplayAttributes(false, "status", 5)]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public DepartamentoDTO Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        public override string SelectCommand()
        {
            return CreateSelectCommand(this);
        }

        public override string Table()
        {
            return "produtos";
        }
        public override string UpdateCommand()
        {
            return Update(this);
        }

        public override string InsertCommand()
        {
            return Insert(this);
        }

        public override void FillSubClass()
        {
            FillDepartamento();
        }
        private DepartamentoDTO FindDepartamento(int id)
        {
            DepartamentoDTO objDTO = new DepartamentoDTO();
            objDTO.Id = id;
            CollectiveDepartamentosDTO collective = new CollectiveDepartamentosDTO();
            objDTO = collective.ObjOne(objDTO);
            return objDTO;
        }

        public void FillDepartamento()
        {
            if (this.DepartamentoId > 0)
            {
                this.Departamento = FindDepartamento(this.DepartamentoId);
            }
        }
    }
}
