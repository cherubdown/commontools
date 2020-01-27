using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet2.Common.Attributes
{
    public class SqlAttribute : Attribute
    {
        public SqlAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        private string _columnName;
        public virtual string ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
            }
        }
    }
}
