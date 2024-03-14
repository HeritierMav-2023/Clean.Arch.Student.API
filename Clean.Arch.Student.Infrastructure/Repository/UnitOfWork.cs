using Clean.Arch.Student.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Arch.Student.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //1-Déclaration d'objets repository
        public IStudentRepository StudentRepository { get; }

        //2-Constructeur DI
        public UnitOfWork(IStudentRepository StudentRepositorie)
        {
            StudentRepository = StudentRepositorie;
        }

    
    }
}
