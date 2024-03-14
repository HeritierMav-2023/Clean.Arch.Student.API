using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Arch.Student.Domain.Entities;

namespace Clean.Arch.Student.Application.IRepository
{
    public interface IStudentRepository
    {
        public List<Domain.Entities.Student> GetStudentDetails();

        public void AddStudent(Domain.Entities.Student student);
        Task<Domain.Entities.Student> ADDStudent(Domain.Entities.Student entity);
        Task<int> CreateAsync(Domain.Entities.Student student);

        public void UpdateStudentDetails(Domain.Entities.Student student);
        Task<int> UpdateAsync(Domain.Entities.Student student);

        public Domain.Entities.Student GetStudentData(int id);

        Task<Domain.Entities.Student> GetByIdAsync(int id);

        public void DeleteStudent(int id);
        Task<int> DeleteAsync(int id);
    }
}
