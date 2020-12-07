using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.EfCore
{
    public  class EfAbstract<AnyType> : IRepository<AnyType>
        where AnyType : class
    {
        SchoolErp dbcont = null;
        public EfAbstract()
        {
            dbcont = new SchoolErp(); // Self contained transaction
        }
        public void Add(AnyType obj)
        {
            dbcont.Set<AnyType>().Add(obj); // in memory committ
        }

        public void Save()
        {
            dbcont.SaveChanges(); //physical committ
        }
        public void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnyType> Search()
        {
            return dbcont.Set<AnyType>().
                     AsQueryable<AnyType>().
                         ToList<AnyType>();
        }



        public void SetUnitWork(IUow uow)
        {
            dbcont = ((EUow)uow); // Global transaction UOW
        }


        public IEnumerable<AnyType> GetData()
        {
            throw new NotImplementedException();
        }


        public AnyType GetData(object Index)
        {
            throw new NotImplementedException();
        }


        public void Cancel(int Index)
        {
            throw new NotImplementedException();
        }

        public AnyType GetData(int Index)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnyType> GetData(int classId, int sectionid, int year, int month)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnyType> GetAttView(string AdmissionId = null, int? classId = null, int? sectionid = null, int? year = null, int? month = null)
        {
            throw new NotImplementedException();
        }
    }




    public class EUow : SchoolErp, IUow
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CustomerBase>()
        //               .ToTable("tblCustomer");
        //}
        //public EUow() : base("name=Conn")
        //{

        //}
        public void Committ()
        {
            SaveChanges();
        }

        public void RollBack() // Adapter
        {
            Dispose();
        }
    }
}
