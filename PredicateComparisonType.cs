using Intranet2.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet2.Common.Enums
{
    public enum PredicateComparisonType
    {
        [PredicateLiteral(">=")]
        GreaterEqual,
        [PredicateLiteral("<=")]
        LessEqual,
        [PredicateLiteral("=")]
        EqualTo,
        [PredicateLiteral(">")]
        GreaterThan,
        [PredicateLiteral("<")]
        LessThan
    }
}
