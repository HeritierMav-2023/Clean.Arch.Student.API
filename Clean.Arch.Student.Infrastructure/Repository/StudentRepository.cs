using Clean.Arch.Student.Application.IRepository;
using Clean.Arch.Student.Infrastructure.Config;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Clean.Arch.Student.Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        //1-propriéte connexion BD
        private readonly ConnectionString _connection;


        /// <summary>
        /// Ceci est utilisé comme constructeur d'injection pour lire la chaîne de connexion à partir de appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        public StudentRepository(IOptions<ConnectionString> configuration)
        {
            _connection = configuration.Value;
        }

        public async Task<int> Add(Domain.Entities.Student student)
        {
            var dateAdded = DateTime.Now;
            string sQuery = "GetStudentDetails";
            var dynamicParameters = new DynamicParameters();
            using (var connection = new SqlConnection(_connection.DefaultConnection))
            {
                dynamicParameters.Add("@Action", "Post");
                dynamicParameters.Add("@Id", student.Id);
                dynamicParameters.Add("@Name", student.Name);
                dynamicParameters.Add("@Email", student.Email);
                dynamicParameters.Add("@Password", student.Password);
                dynamicParameters.Add("@DateofBirth", student.DateofBirth);
                dynamicParameters.Add("@DateofJoining", student.DateofJoining);
                dynamicParameters.Add("@DateAdded", dateAdded);
                return await connection.ExecuteAsync(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
            }

        }

        public int Count(Expression<Func<Domain.Entities.Student, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                string sQuery = "GetStudentDetails";
                using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Action", "Delete");
                dynamicParameters.Add("@Id", id);
                return await connection.ExecuteAsync(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public bool Exists(Expression<Func<Domain.Entities.Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Domain.Entities.Student>GetAll()
        {
           var stdList = new List<Domain.Entities.Student>();
            try
            {
                //Nom de la procédure pour récupérer la liste des étudiants
                string sQuery = "GetStudentDetails";

                //il contient la chaîne de connexion à lire à partir des paramètres d'application appsettings
                using var connection = new SqlConnection(_connection.DefaultConnection);

                // C'est l'objet Dapper pour ajouter des paramètres
                var dynamicParameters = new DynamicParameters();
                
                dynamicParameters.Add("@Action", "GetAll");

                //Query :Celui-ci est responsable du retour de l'étudiant en utilisant la méthode asynchrone
                var StudentList = connection.Query<Domain.Entities.Student>(sQuery, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
                stdList.AddRange(StudentList);
            }
            catch(Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
            return stdList;
        }

        public async Task<Domain.Entities.Student> GetbyId(int id)
        {
            try
            {
                string sQuery = "GetStudentDetails";
                using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Action", "GetData");
                dynamicParameters.Add("@Id", id);

                var Student = await connection.QueryAsync<Domain.Entities.Student>(sQuery, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

                return Student.FirstOrDefault();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<int> Update(Domain.Entities.Student student)
        {
            try
            {

                string sQuery = "GetStudentDetails";
                using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Action", "Put");
                dynamicParameters.Add("@Name", student.Name);
                dynamicParameters.Add("@Email", student.Email);
                dynamicParameters.Add("@Password", student.Password);
                dynamicParameters.Add("@DateofBirth", student.DateofBirth);
                dynamicParameters.Add("@DateofJoining", student.DateofJoining);
                dynamicParameters.Add("@DateModified", DateTime.Now);
                dynamicParameters.Add("@Id", student.Id);

                return await connection.ExecuteAsync(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
