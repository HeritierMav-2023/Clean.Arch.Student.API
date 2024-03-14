using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Arch.Student.Application.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork 
    {
        IStudentRepository StudentRepository { get; }
    }
}
