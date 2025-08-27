using Maxima_Tech_API.Class.Attributes;
using Maxima_Tech_API.Class.DBHandler;
using System.Reflection;

namespace Maxima_Tech_API.Class.DTO.Base
{
    public abstract class BaseCollective
    {

        public int MaxAmount = 0;
        public static string RemoveSQLInjection(string value)
        {
            return value.Replace("-", "").Replace("'", "");
        }
        public T FillClass<T>(object[] dados) where T : new()
        {
            T obj = new T();
            PropertyInfo[] propriedades = typeof(T).GetProperties();

            foreach (var propriedade in propriedades)
            {
                var atributo = propriedade.GetCustomAttributes(typeof(DisplayAttributes), false)
                                           .FirstOrDefault() as DisplayAttributes;

                if (atributo != null && atributo.Column < dados.Length)
                {
                    if (dados[atributo.Column] != DBNull.Value)
                    {
                        if (propriedade.PropertyType.FullName.Contains("DateTime"))
                        {
                            propriedade.SetValue(obj, (DateTime)dados[atributo.Column]);
                        }
                        else if (propriedade.PropertyType.FullName.Contains("Boolean"))
                        {
                            propriedade.SetValue(obj, (bool)dados[atributo.Column]);
                        }
                        else if (typeof(IConvertible).IsAssignableFrom(dados[atributo.Column].GetType()))
                        {
                            propriedade.SetValue(obj, Convert.ChangeType(dados[atributo.Column], propriedade.PropertyType));
                        }                       
                        else
                        {
                            propriedade.SetValue(obj, dados[atributo.Column]);
                        }
                    }
                }
            }
            return obj;
        }
        public List<Array> SelectArray(BaseDTO objDTO)
        {
            string command = objDTO.SelectCommand();
            return MySQLHandler.SQLReader(command);
        }
        public void Update(BaseDTO objDTO)
        {
            string command = objDTO.UpdateCommand();
            MySQLHandler.SQLCommand(command);
        }
        public void Insert(BaseDTO objDTO)
        {
            string command = objDTO.InsertCommand();
            MySQLHandler.SQLCommand(command);
        }
    }
}
