using AutoMapper;
using Infra.School.Data.AdoCore;
using Infra.School.Data.AutoMapper;
using Infra.School.Data.Contract;
using Infra.School.Data.Db;
using School.Domain.Master;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.School.Data.ResultRepository
{
    public class ResultsRepository : TemplateADO<DataTable>, IRepository<DataTable>,IResultsRepository
    {
        private readonly IMapper mapper;
        public ResultsRepository()
        {
            mapper = DbAutoMapperProfiles.Mappers(); ;
        }

        public List<SubjectsMaster> GetSubjectList()
        {
            List<SubjectsMaster> SubjectsMasters = new List<SubjectsMaster>();
            using (var entity = new SchoolErp())
            {
                var subject = entity.SubjectMasters.ToList();
                SubjectsMasters = (mapper.Map<List<SubjectsMaster>>(subject));
                return SubjectsMasters;
            }
        }

        public List<DataTable> ReportCards(string admissionValue)
        {
            List<DataTable> obj = new List<DataTable>();
           
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand objCommand = new SqlCommand("proc_AnnualMarkSheet", conn);

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@admissionid", admissionValue);
                using (SqlDataReader sqlDataReader = objCommand.ExecuteReader())
                {
                    
                    
                    //while (sqlDataReader.Read())
                    //{
                        DataTable dt = new DataTable();
                        dt.Load(sqlDataReader);
                        obj.Add(dt);
                    // }

                    while (!sqlDataReader.IsClosed)
                    {
                        DataTable dt2 = new DataTable();
                        dt2.Load(sqlDataReader);
                        obj.Add(dt2);
                    }

                 }
               
                if (conn.State == ConnectionState.Open) conn.Close();
                return obj;
            }
        }

        public string ResultBulkInsert(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                try
                {
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        var bulkCopy = new SqlBulkCopy(conn);

                        var table = "resultdetail";
                        bulkCopy.DestinationTableName = table;
                        var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt);

                        if (conn.State == ConnectionState.Open) conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "1";
        }

        protected override void ExecuteCommand(DataTable obj)
        {
            throw new System.NotImplementedException();
        }

        protected override List<DataTable> ExecuteCommand()
        {
            throw new NotImplementedException();


        }

        public DataTable ViewStudent(int classId, int sectionId, int sessionId, string admissionId, string name)
        {
            DataTable obj = new DataTable();
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand objCommand = new SqlCommand("Proc_ViewResultClass",conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@classId", classId);
                objCommand.Parameters.AddWithValue("@sectionId", sectionId);
                objCommand.Parameters.AddWithValue("@sessionId", sessionId);
                objCommand.Parameters.AddWithValue("@admissionId", admissionId);
                objCommand.Parameters.AddWithValue("@name", name);
                using (SqlDataReader sqlDataReader = objCommand.ExecuteReader())
                {
                   
                    dt.Load(sqlDataReader);
                    // obj.Add(dt);

                }
                if (conn.State == ConnectionState.Open) conn.Close();
                return dt;
            }
        }

        protected override List<DataTable> ExecuteCommand(string AdmissionId, int? classId, int? sectionid, int? year, int? month)
        {
            List<DataTable> obj = new List<DataTable>();
            objCommand.CommandText = "proc_ClassSubjectExcel";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@classId", classId);
            using (SqlDataReader sqlDataReader = objCommand.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sqlDataReader);
                obj.Add(dt);

                return obj;

            }

            return obj;
        }

        }
}
