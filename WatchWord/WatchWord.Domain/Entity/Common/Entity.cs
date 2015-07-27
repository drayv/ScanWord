using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchWord.Domain.Entity.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        /// <summary>Gets or sets the id.</summary>
        public virtual T Id { get; set; }
    }
}
