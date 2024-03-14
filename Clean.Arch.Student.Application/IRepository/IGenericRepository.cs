
using System.Linq.Expressions;


namespace Clean.Arch.Student.Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1 si la création s'est deroulée correctement </returns>
        Task<int> Add(T student);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The all dataset.</returns>
        List<T> GetAll();

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>The object dataset.</returns>
        Task<T> GetbyId( int id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<int> Update(T student);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        Task<int> Delete(int id);
   
        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The number of rows.</returns>
        int Count(Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// Check if an element exists for a condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A boolean</returns>
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
