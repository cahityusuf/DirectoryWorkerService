using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Results
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
