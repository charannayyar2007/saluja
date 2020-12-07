using Infra.School.Data.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.AdoCore
{
    public abstract class TemplateADO<AnyType> : AbstractDal<AnyType>
    {
        protected SqlConnection objConn = null;
        protected SqlCommand objCommand = null;
        IUow uowobj = null;
        public override void SetUnitWork(IUow uow)
        {
            uowobj = uow;
            objConn = ((AdoUow)uow).Connection;
            objCommand = new SqlCommand();
            objCommand.Connection = objConn;
            objCommand.Transaction = ((AdoUow)uow).Transaction;
        }

        private void Open()
        {
            if ((objConn == null) || (objConn.State == ConnectionState.Closed))
            {
                objConn = new SqlConnection(ConfigurationManager.
                        ConnectionStrings["ConString"].ConnectionString);
                objConn.Open();
                objCommand = new SqlCommand();
                objCommand.Connection = objConn;
            }

        }
        protected abstract void ExecuteCommand(AnyType obj); // Child classes 
        protected abstract List<AnyType> ExecuteCommand(); // Child classes 
        protected abstract List<AnyType> ExecuteCommand(string AdmissionId=null, int? classId=null, int? sectionid = null, int? year = null, int? month = null); // Child classes 
        private void Close()
        {
            if (uowobj == null)
            {
                objConn.Close();
            }
        }
        public void Execute(AnyType obj) // Fixed Sequence Insert
        {
            Open();
            ExecuteCommand(obj);
            Close();
        }
        public List<AnyType> Execute() // Fixed Sequence select
        {
            Open();
            AnyTypes = ExecuteCommand();
            Close();
            return AnyTypes;
        }

        public List<AnyType> Execute(string AdmissionId=null, int? classId = null, int? sectionid = null, int? year = null, int? month = null) // Fixed Sequence select
        {
            Open();
            AnyTypes = ExecuteCommand(AdmissionId,classId, sectionid, year, month);
            Close();
            return AnyTypes;
        }
        public override void Save()
        {
            foreach (AnyType o in AnyTypes)
            {
                Execute(o);
            }
        }
        public override IEnumerable<AnyType> Search()
        {
            return Execute();
        }
        public override IEnumerable<AnyType>  GetAttView(string AdmissionId=null, int? classId = null, int? sectionid = null, int? year = null, int? month = null)
        {
            return Execute(AdmissionId,classId, sectionid, year, month);
        }
    }
    public class AdoUow : IUow
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }
        public AdoUow()
        {
            Connection = new SqlConnection(ConfigurationManager.
                        ConnectionStrings["Conn"].ConnectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }
        public void Committ()
        {
            Transaction.Commit();
            Connection.Close();
        }

        public void RollBack() // object Adapte 
        {
            Transaction.Dispose();
            Connection.Close();
        }
    }
}
