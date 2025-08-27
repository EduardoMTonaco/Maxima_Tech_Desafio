using Maxima_Tech_API.Class.DTO.Base;

namespace Maxima_Tech_API.Class.DTO.Login
{
    public class CollectiveUsuariosDTO : BaseCollective
    {
        public List<UsuariosDTO> ObjList(UsuariosDTO objDTO)
        {
            try
            {
                List<UsuariosDTO> objList = new List<UsuariosDTO>();
                foreach (object[] array in SelectArray(objDTO))
                {
                    UsuariosDTO obj = FillClass<UsuariosDTO>(array);
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
        public UsuariosDTO ObjOne(UsuariosDTO objDTO)
        {
            try
            {
                foreach (object[] array in SelectArray(objDTO))
                {
                    UsuariosDTO obj = FillClass<UsuariosDTO>(array);
                    obj.FillSubClass();
                    return obj;
                }
                return new UsuariosDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
