using Clean.Arch.Student.Application.IRepository;
using Clean.Arch.Student.Infrastructure.Config;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Clean.Arch.Student.Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {


        //1-propriéte connexion
        private readonly ConnectionString _connection;


        /// <summary>
        /// Ceci est utilisé comme constructeur d'injection pour lire la chaîne de connexion à partir de appsettings.json
        /// </summary>
        /// <param name="configuration"></param>
        public StudentRepository(IOptions<ConnectionString> configuration)
        {
            _connection = configuration.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Domain.Entities.Student> ADDStudent(Domain.Entities.Student entity)
        {
            try
            {
                string sQuery = "GetStudentDetails";
                using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Action", "Post");
                dynamicParameters.Add("@Name", entity.Name);
                dynamicParameters.Add("@Email", entity.Email);
                dynamicParameters.Add("@Password", entity.Password);
                dynamicParameters.Add("@DateofBirth", entity.DateofBirth);
                dynamicParameters.Add("@DateofJoining", entity.DateofJoining);
                var result = await connection.QueryAsync<Domain.Entities.Student>(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddStudent(Domain.Entities.Student student)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> CreateAsync(Domain.Entities.Student student)
        {
            try
            {
                string sQuery = "GetStudentDetails";
                using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Action", "Post");
                dynamicParameters.Add("@Name", student.Name);
                dynamicParameters.Add("@Email", student.Email);
                dynamicParameters.Add("@Password", student.Password);
                dynamicParameters.Add("@DateofBirth", student.DateofBirth);
                dynamicParameters.Add("@DateofJoining", student.DateofJoining);
                
                return await connection.ExecuteAsync(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id)
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
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Domain.Entities.Student> GetByIdAsync(int id)
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
            catch
            {
                throw;
            }

            ////var query = "GetStudentDetails"; //"SELECT * FROM [dbo].[StudentDetails] WHERE Id = @Id";
            //var dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("Id", id, DbType.Int32);
            //dynamicParameters.Add("@Action", "GetData");

            //using (var connection = new SqlConnection(_connection.DefaultConnection))
            //{
            //    //return await connection.QueryFirstOrDefaultAsync<Domain.Entities.Student>(query, dynamicParameters);
            //    // var StudentList = connection.Query<Domain.Entities.Student>("GetStudentDetails", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            //    return await connection.QueryFirstOrDefaultAsync<Domain.Entities.Student>("GetStudentDetails", dynamicParameters,commandType: System.Data.CommandType.StoredProcedure);
            //}
            /*
              try
            {
                using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Action", "GetAll");

                var StudentList = connection.Query<Domain.Entities.Student>("GetStudentDetails", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                return StudentList;
            }
            catch
            {
                throw;
            }
             */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public  Domain.Entities.Student GetStudentData(int id)
        {
            try
            {
                //using var connection = new SqlConnection(_connection.DefaultConnection);
                var dynamicParameters = new DynamicParameters();
                var parameters = new DynamicParameters();
                dynamicParameters.Add("@Id", id);
                var query = "SELECT * FROM [dbo].[StudentDetails] WHERE Id = @Id";
                //dynamicParameters.Add("@Action", "GetData");
                // return await connection.QueryFirstOrDefaultAsync<Domain.Entities.Student>(query, parameters);
                //return await connection.ExecuteAsync(query, dynamicParameters);
                using (var connection = new SqlConnection(_connection.DefaultConnection))
                {
                    return connection.QueryFirstOrDefault<Domain.Entities.Student>(query, parameters);
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>liste des étudiants</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Domain.Entities.Student> GetStudentDetails()
        {
            try
            {
                string sQuery = "GetStudentDetails"; //Nom de la procédure pour récupérer la liste des étudiants
                using var connection = new SqlConnection(_connection.DefaultConnection);   //il contient la chaîne de connexion à lire à partir des paramètres d'application appsettings
                var dynamicParameters = new DynamicParameters(); // C'est l'objet Dapper pour ajouter des paramètres
                dynamicParameters.Add("@Action", "GetAll");

                //Query :Celui-ci est responsable du retour de l'étudiant en utilisant la méthode asynchrone
                var StudentList = connection.Query<Domain.Entities.Student>(sQuery, dynamicParameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                return StudentList;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(Domain.Entities.Student student)
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
                dynamicParameters.Add("@Id", student.Id);

                return await connection.ExecuteAsync(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="student"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateStudentDetails(Domain.Entities.Student student)
        {
            //try
            //{
            //    string sQuery = "GetStudentDetails";
            //    using var connection = new SqlConnection(_connection.DefaultConnection);
            //    var dynamicParameters = new DynamicParameters();
            //    dynamicParameters.Add("@Action", "Put");
            //    dynamicParameters.Add("@Name", student.Name);
            //    dynamicParameters.Add("@Email", student.Email);
            //    dynamicParameters.Add("@Password", student.Password);
            //    dynamicParameters.Add("@DateofBirth", student.DateofBirth);
            //    dynamicParameters.Add("@DateofJoining", student.DateofJoining);

            //    return await connection.ExecuteAsync(sQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
            //}
            //catch (Exception exp)
            //{
            //    throw new Exception(exp.Message, exp);
            //}
        }
    }
}
