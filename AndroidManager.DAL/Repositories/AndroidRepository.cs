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

    public class AndroidRepository : IRepository<Android> {
        private ApplicationContext _db;
        private const int _initialReliability = 10;
        private const int _initialStatus = 1;

        public AndroidRepository(ApplicationContext dbContext) {
            this._db = dbContext;
        }

        public void Create(Android item) {
            item.Reliability = AndroidRepository._initialReliability;
            item.Status = 1;
            this._db.Androids.Add(item);
        }


        public void Update(Android item) {
            var _oldItem = this.Get(item.Id);
            _oldItem.Name = item.Name;
            _oldItem.Avatar = item.Name;
            _oldItem.Skills = item.Skills;
            _oldItem.JobId = item.JobId;
            _oldItem.Reliability = item.Reliability;
            _oldItem.Status = item.Status;
            this._db.Entry(_oldItem).State = EntityState.Modified;
        }

        public void Remove(Android item) {
            this._db.Androids.Remove(item);
        }

        public Android Get(int? id) {
            return this._db.Androids.Find(id);
        }

        public IEnumerable<Android> GetAll() {
            return this._db.Androids;
        }

        public IEnumerable<Android> Find(Func<Android, bool> predicate) {
            return this._db.Androids.Where(predicate).ToList();
        }

        public void Dispose() {
            this._db.Dispose();
        }
    }
}
