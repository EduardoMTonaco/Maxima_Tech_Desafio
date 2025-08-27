using Google.Protobuf.WellKnownTypes;
using Maxima_Tech_API.Class.Attributes;
using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace Maxima_Tech_API.Class.DTO.Base
{
    public abstract class BaseDTO
    {
        public abstract string Table();
        public abstract string SelectCommand();
        public abstract string UpdateCommand();
        public abstract string InsertCommand();
        public abstract void FillSubClass();
        public int MaxAmount = int.MinValue;
        private string Fields<T>(T obj, string alias)
        {
            string sqlSelect = "";
            bool virgula = false;
            foreach (var property in typeof(T).GetProperties())
            {
                string field = "";
                var displayNameAttribute = property.GetCustomAttribute<DisplayAttributes>();
                if (displayNameAttribute != null)
                {
                    object value = property.GetValue(obj);
                    if (virgula)
                    {
                        field = field + ", ";
                    }
                    field = field + $" {alias}.{displayNameAttribute.Name} ";
                    virgula = true;

                    sqlSelect = sqlSelect + field;
                }
            }
            return sqlSelect;
        }

        private string Condition<T>(T obj, string alias)
        {
            string sqlCondition = "";
            bool and = false;
            foreach (var property in typeof(T).GetProperties())
            {
                string condition = "";
                var displayNameAttribute = property.GetCustomAttribute<DisplayAttributes>();
                if (displayNameAttribute != null)
                {
                    object value = property.GetValue(obj);

                    if (value is string)
                    {
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            if (value.ToString().Contains("'"))
                            {
                                throw new Exception($"Caractere invalido em {displayNameAttribute.Name}");
                            }
                            if (and)
                            {
                                condition = condition + " and ";
                            }
                            condition = condition + StringCondition(alias, displayNameAttribute, value);
                            and = true;
                        }
                    }
                    else if (value is Guid)
                    {
                        if (value != null && Guid.TryParse(value.ToString(), out Guid guid) && guid != Guid.Empty)
                        {
                            if (value.ToString().Contains("'"))
                            {
                                throw new Exception($"Caractere invalido em {displayNameAttribute.Name}");
                            }
                            if (and)
                            {
                                condition = condition + " and ";
                            }
                            condition = condition + StringCondition(alias, displayNameAttribute, value);
                            and = true;
                        }
                    }
                    else if (value is int)
                    {

                        if ((int)value > int.MinValue)
                        {
                            if (and)
                            {
                                condition = condition + " and ";
                            }
                            condition = condition + $" {alias}.{displayNameAttribute.Name} = {value} ";
                            and = true;
                        }

                    }
                    else if (value is decimal)
                    {

                        if ((decimal)value > decimal.MinValue)
                        {
                            if (and)
                            {
                                condition = condition + " and ";
                            }
                            string valueString = Convert.ToDecimal(value).ToString(CultureInfo.InvariantCulture);
                            condition = condition + $" {alias}.{displayNameAttribute.Name} = {valueString} ";
                            and = true;
                        }

                    }
                    else if (value is bool)
                    {

                        if (value != null)
                        {
                            if (and)
                            {
                                condition = condition + " and ";
                            }
                            condition = condition + $" IFNULL({alias}.{displayNameAttribute.Name},1) = {Convert.ToInt32(value)} ";
                            and = true;
                        }

                    }
                    sqlCondition = sqlCondition + condition;
                }
            }
            return sqlCondition;
        }

        protected virtual string StringCondition(string alias, DisplayAttributes? displayNameAttribute, object value)
        {
            return $" {alias}.{displayNameAttribute.Name} like '%{value}%' ";
        }

        protected string Update<T>(T obj)
        {
            string sqlSet = "";
            string sqlCondition = "";
            bool virgula = false;
            foreach (var property in typeof(T).GetProperties())
            {
                var displayNameAttribute = property.GetCustomAttribute<DisplayAttributes>();
                string set = "";
                if (displayNameAttribute != null)
                {
                    object value = property.GetValue(obj);
                    if (displayNameAttribute.PrimaryKey)
                    {
                        if (value is string)
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                sqlCondition = $" WHERE {displayNameAttribute.Name} = '{value}'";
                            }
                            else
                            {
                                throw new Exception("Chave Primaria Vazia!");
                            }

                        }
                        else if (value is int)
                        {
                            if ((int)value > int.MinValue)
                            {
                                sqlCondition = $" WHERE {displayNameAttribute.Name} = {value}";
                            }
                            else
                            {
                                throw new Exception("Chave Primaria Vazia!");
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                sqlCondition = $" WHERE {displayNameAttribute.Name} = '{value}'";
                            }
                            else
                            {
                                throw new Exception("Chave Primaria Vazia!");
                            }
                        }
                    }
                    else if (!displayNameAttribute.PrimaryKey)
                    {
                        if (value is string)
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                if (virgula)
                                {
                                    set = set + " , ";
                                }
                                set = set + $" {displayNameAttribute.Name} = '{value}' ";
                                virgula = true;
                            }
                        }
                        else if (value is int)
                        {
                            if ((int)value > int.MinValue)
                            {
                                if (virgula)
                                {
                                    set = set + " , ";
                                }
                                set = set + $" {displayNameAttribute.Name} = {value} ";
                                virgula = true;
                            }
                        }
                        else if (value is bool)
                        {
                            if (value != null)
                            {
                                if (virgula)
                                {
                                    set = set + " , ";
                                }
                                set = set + $" {displayNameAttribute.Name} = {Convert.ToInt32(value)} ";
                                virgula = true;
                            }
                        }
                        else if (value is DateTime)
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                if (virgula)
                                {
                                    set = set + " , ";
                                }
                                set = set + $" {displayNameAttribute.Name} = '{value}' ";
                                virgula = true;
                            }
                        }
                        else if (value is decimal)
                        {
                            if ((decimal)value > decimal.MinValue)
                            {
                                if (virgula)
                                {
                                    set = set + " , ";
                                }
                                set = set + $" {displayNameAttribute.Name} = {value.ToString().Replace(',', '.')} ";
                                virgula = true;
                            }
                        }
                        else if (value != null)
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                if (virgula)
                                {
                                    set = set + " , ";
                                }
                                set = set + $" {displayNameAttribute.Name} = '{value.ToString}' ";
                                virgula = true;
                            }
                        }
                        sqlSet = sqlSet + set;
                    }
                }
            }
            return $"UPDATE {Table()} SET" + sqlSet + sqlCondition;
        }

        protected string Insert<T>(T obj)
        {
            string sqlColumn = "";
            string sqlValues = "";
            bool virgula = false;
            foreach (var property in typeof(T).GetProperties())
            {
                var displayNameAttribute = property.GetCustomAttribute<DisplayAttributes>();
                string values = "";
                string column = "";
                if (displayNameAttribute != null)
                {
                    object value = property.GetValue(obj);
                    if (!displayNameAttribute.PrimaryKey)
                    {
                        if (value is string)
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                if (virgula)
                                {
                                    values = values + " , ";
                                    column = column + " , ";
                                }
                                column = column + $" {displayNameAttribute.Name}";
                                values = values + $" '{value}' ";
                                virgula = true;
                            }
                        }
                        else if (value is int)
                        {
                            if ((int)value > int.MinValue)
                            {
                                if (virgula)
                                {
                                    values = values + " , ";
                                    column = column + " , ";
                                }
                                column = column + $" {displayNameAttribute.Name} ";
                                values = values + $" {value} ";
                                virgula = true;
                            }
                        }
                        else if (value is decimal)
                        {
                            if ((decimal)value > decimal.MinValue)
                            {
                                if (virgula)
                                {
                                    values = values + " , ";
                                    column = column + " , ";
                                }
                                column = column + $" {displayNameAttribute.Name} ";
                                values = values + $" {value.ToString().Replace(',', '.')} ";
                                virgula = true;
                            }
                        }
                        sqlColumn = sqlColumn + column;
                        sqlValues = sqlValues + values;
                    }
                }
            }
            return $"INSERT INTO {Table()} (" + sqlColumn + ") values (" + sqlValues + ")";
        }

        protected string CreateSelectCommand<T>(T obj, string alias = "T1")
        {
            string Amount = "";
            if (MaxAmount > 0)
            {
                Amount = $" Top {MaxAmount} ";
            }
            string condition = Condition(obj, alias);

            if (string.IsNullOrEmpty(condition))
            {
                return $"select {Amount} {Fields(obj, alias)} from {Table()} as {alias} ";
            }
            else
            {
                return $"select {Amount} {Fields(obj, alias)} from {Table()} as {alias} where {Condition(obj, alias)}";
            }
        }
    }
}
