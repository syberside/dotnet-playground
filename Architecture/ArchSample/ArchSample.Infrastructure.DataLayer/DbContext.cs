using ArchSample.Domain.User.DataLayer;
using ArchSample.UseCases.Infrastructure;
using System.Collections.Generic;

namespace ArchSample.Infrastructure.DataLayer
{
    public class DbContext : IUnitOfWorkFactory
    {
        public List<DbUser> Users { get; set; }

        public IUnitOfWork Create()
        {
            //TODO: implement using ORM
            throw new System.NotImplementedException();
        }
    }
}
