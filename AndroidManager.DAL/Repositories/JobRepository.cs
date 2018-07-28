using AndroidManager.DAL.EF;
using AndroidManager.DAL.Entities;
using AndroidManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AndroidManager.DAL.Repositories {

    public class JobRepository : IRepository<Job> {
        private ApplicationContext _db;

        public JobRepository(ApplicationContext dbContext) {
            this._db = dbContext;
        }

        public void Create(Job item) {
            this._db.Jobs.Add(item);
        }

        public void Update(Job item) {
            var _oldItem = this.Get(item.Id);
            _oldItem.Name = item.Name;
            _oldItem.Complexity = item.Complexity;
            _oldItem.Description = item.Description;
            this._db.Entry(_oldItem).State = EntityState.Modified;
        }

        public void Remove(Job item) {
            this._db.Jobs.Remove(item);
        }

        public Job Get(int? id) {
            return this._db.Jobs.Find(id);
        }

        public IEnumerable<Job> GetAll() {
            return this._db.Jobs;
        }

        public IEnumerable<Job> Find(Func<Job, bool> predicate) {
            return this._db.Jobs.Where(predicate);
        }

        public void Dispose() {
            this._db.Dispose();
        }
    }
}
