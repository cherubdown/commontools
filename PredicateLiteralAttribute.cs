using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet2.Common.Attributes
{
    public class PredicateLiteralAttribute : Attribute
    {
        public PredicateLiteralAttribute(string value)
        {
            Value = value;
        }

        private string _value;
        public virtual string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
