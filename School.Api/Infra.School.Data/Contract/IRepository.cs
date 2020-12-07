using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.Contract
{
  
    public interface IRepository<AnyType>
    {
        void SetUnitWork(IUow uow);
        void Add(AnyType obj); // Inmemory addition
        void Update(AnyType obj);  // Inmemory updation
        //  :- Iterator
        IEnumerable<AnyType> Search();
        IEnumerable<AnyType> GetAttView(string AdmissionId = null, int? classId = null, int? sectionid = null, int? year = null, int? month = null);
        IEnumerable<AnyType> GetData();
        AnyType GetData(Object Index);
        AnyType GetData(int Index);
        void Save(); // Physical committ

    }
    // :- Unit of Work pattern
    public interface IUow
    {
        void Committ();
        void RollBack();
    }
}
