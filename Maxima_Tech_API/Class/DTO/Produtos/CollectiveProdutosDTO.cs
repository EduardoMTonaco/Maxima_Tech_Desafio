using Maxima_Tech_API.Class.DTO.Base;

namespace Maxima_Tech_API.Class.DTO.Produtos
{
    public class CollectiveProdutosDTO : BaseCollective
    {
        public List<ProdutoDTO> ObjList(ProdutoDTO objDTO)
        {
            try
            {
                List<ProdutoDTO> objList = new List<ProdutoDTO>();
                foreach (object[] array in SelectArray(objDTO))
                {
                    ProdutoDTO obj = FillClass<ProdutoDTO>(array);
                    obj.FillSubClass();
                    objList.Add(obj);
                }
                return objList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProdutoDTO ObjOne(ProdutoDTO objDTO)
        {
            try
            {
                foreach (object[] array in SelectArray(objDTO))
                {
                    ProdutoDTO obj = FillClass<ProdutoDTO>(array);
                    obj.FillSubClass();
                    return obj;
                }
                return new ProdutoDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
