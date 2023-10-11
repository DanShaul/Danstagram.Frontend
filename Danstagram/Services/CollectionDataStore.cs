using Danstagram.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services
{
    public class CollectionDataStore<T> : IDataStore<T> where T : IEntity
    {
        private readonly ICollection<T> entityCollection;
        public CollectionDataStore(){
            this.entityCollection = DependencyService.Get<ICollection<T>>();
        }
        public async Task CreateAsync(T entity)
        {

            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var existingEntity = await Task.Run(() => entityCollection.FirstOrDefault(tempEntity => tempEntity.Id == entity.Id));
            if (existingEntity != null){
                return;
            }
            await Task.Run(() => entityCollection.Add(entity));
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Run(() => entityCollection.Remove(entityCollection.Single(entity => entity.Id == id)));
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await Task.Run(() => entityCollection.ToList());
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await Task.Run(() => entityCollection.Where(filter.Compile()).ToList());
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await Task.Run(() => entityCollection.FirstOrDefault(entity => entity.Id == id));
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await Task.Run(() => entityCollection.FirstOrDefault(filter.Compile()));
        }

        public Task LoadDataFromBackend()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await Task.Run(() => entityCollection.Select(existingEntity => existingEntity.Id == entity.Id ? entity : existingEntity));
        }
    }
}
