using Maxima_Tech_API.Class.DTO.Base;

namespace Maxima_Tech_API.Class.DTO.Departamentos
{
    public class CollectiveDepartamentosDTO : BaseCollective
    {
        public List<DepartamentoDTO> ObjList(DepartamentoDTO objDTO)
        {
            try
            {
                List<DepartamentoDTO> objList = new List<DepartamentoDTO>();
                foreach (object[] array in SelectArray(objDTO))
                {
                    DepartamentoDTO obj = FillClass<DepartamentoDTO>(array);
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
        public DepartamentoDTO ObjOne(DepartamentoDTO objDTO)
        {
            try
            {
                foreach (object[] array in SelectArray(objDTO))
                {
                    DepartamentoDTO obj = FillClass<DepartamentoDTO>(array);
                    obj.FillSubClass();
                    return obj;
                }
                return new DepartamentoDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
