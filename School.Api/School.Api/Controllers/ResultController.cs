using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.Masters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
using OfficeOpenXml;
using System.Collections;
using System.Data;
using School.Utility.Enum;
using System.Web.UI;
using System.Web.UI.WebControls;
using School.Application.ResultService;
using PagedList;
using School.Domain.Student;
using System.Net;

namespace School.Api.Controllers
{
    public class ResultController : Controller
    {
        private readonly IMastersService masterService;
        private readonly IResultService resultService;

        public ResultController()
        {
            masterService = ApplicatonServiceFactory<MastersService>.Create();
            resultService = ApplicatonServiceFactory<ResultsService>.Create();
        }

        [HttpGet]
        // GET: Result
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Result
        public async Task<ActionResult> ExportToExcel()
        {
            var Master = await masterService.GetAllMasters();

            return View("~/Views/Result/ExcelData.cshtml", Master);
        }

        [HttpPost]
        public async Task<ActionResult> ExportToExcel(FormCollection formCollection)
        {


            //ExcelPackage Ep = new ExcelPackage();
            //ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultExcel");
            //Sheet.Cells["A1"].Value = "AdmissionID";
            //char[] alpha = "BCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            //int i = 0, k = 0;
            //foreach (var slist in resultService.GetSubjectList())
            //{
            //    if (i <= 25 && k == 0)
            //    {
            //        Sheet.Cells[alpha[i] + "1"].Value = slist.Subject;
            //        i += 1;
            //        if(i == 25)
            //        {
            //            i = 0;
            //            k = 1;
            //        }
            //    }
            //    else
            //    {
            //        Sheet.Cells["A" + alpha[i] + "1"].Value = slist.Subject;
            //        i += 1;
            //    }
            //}

            //Sheet.Cells["A1"].Value = "Enrollement No.";
            //Sheet.Cells["B1"].Value = "Semester";
            //Sheet.Cells["C1"].Value = "Month";
            //Sheet.Cells["D1"].Value = "Year";
            //Sheet.Cells["E1"].Value = "Statement No.";

            //Sheet.Cells["A:AZ"].AutoFitColumns();
            //Response.Clear();
            //Response.ContentType = "application/ms-excel";
            //Response.AddHeader("content-disposition", "attachment: filename=ResultFormat.xls");
            //Response.BinaryWrite(Ep.GetAsByteArray());
            //Response.End();

            var gv = new GridView();
            List<DataTable> dt = resultService.ExportExcel(Convert.ToInt32(formCollection["sltclass"]));
            gv.DataSource = dt[0];
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ResultFormat_" + formCollection["sltclass"] + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            var Master = await masterService.GetAllMasters();

            return View("~/Views/Result/ExcelData.cshtml", Master);
        }

        public async Task<ActionResult> UploadResult()
        {
            var Master = await masterService.GetAllMasters();

            return View("~/Views/Result/Index.cshtml", Master);
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase postedFile, FormCollection formCollection)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/UploadResult/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + "test_" + Path.GetFileName(postedFile.FileName));
                if (Path.GetExtension(postedFile.FileName).Equals(".xlsx"))
                {
                    ExcelPackage excel = new ExcelPackage(postedFile.InputStream);
                    ExcelWorksheet workSheet = excel.Workbook.Worksheets.ElementAt(0);

                    ArrayList arorder = new ArrayList();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AdmissionID");
                    dt.Columns.Add("SubId");
                    dt.Columns.Add("SessionId");
                    dt.Columns.Add("ResultId");
                    dt.Columns.Add("EvaluationMarks");
                    dt.Columns.Add("IsEvaluteType");

                    int rowCount = workSheet.Dimension.End.Row;
                    int AdmissionCount = 0;
                    string AdmissionVal = "";
                    string subHeading = "";

                    foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                    {
                        if (firstRowCell.Text.Trim() == SubjectEnum.AdmissionID.ToString())
                        {
                        }
                        else
                        {
                            if (subHeading == "")
                            {
                                subHeading = firstRowCell.Text.Trim();
                            }
                            else
                            {
                                subHeading += "," + firstRowCell.Text.Trim();
                            }
                        }

                    }

                    string[] Heading = subHeading.Split(',');
                    int k = 0;
                    for (int j = 1; j <= rowCount; j++)
                    {

                        AdmissionCount = 0;
                        k = 0;
                        foreach (var RowCell in workSheet.Cells[j + 1, 1, 1, workSheet.Dimension.End.Column])
                        {

                            if (AdmissionCount == 0)
                            {
                                AdmissionVal = RowCell.Text.Trim();
                                AdmissionCount = 1;
                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = AdmissionVal;
                                dr[1] = (int)Enum.Parse(typeof(SubjectEnum), Heading[k]);
                                dr[2] = formCollection["sltsession"];
                                dr[3] = formCollection["sltresult"];
                                dr[4] = RowCell.Text.Trim();
                                dr[5] = formCollection["sltevaluate"];
                                dt.Rows.Add(dr);
                                k = k + 1;
                            }

                            if (RowCell.Text.Trim().Replace(" ", "").ToLower() == "")
                                break;
                        }

                    }

                    string valType = resultService.ResultBulk(dt);


                }
            }
            var Master = await masterService.GetAllMasters();

            return View("~/Views/Result/Index.cshtml", Master);
        }

        public async Task<ActionResult> ViewResult()
        {
            ViewBag.Master = await masterService.GetAllMasters();
            ViewBag.flag = "0";
            IPagedList<StudentDetail> stu = null;
            return View("~/Views/Result/ViewData.cshtml", stu);
        }

        [HttpPost]
        public async Task<ActionResult> ViewResult(FormCollection formCollection, int? page)
        {
            ViewBag.ClassName = formCollection["hdnclass"];
            ViewBag.SectionName = formCollection["hdnsection"];
            ViewBag.SessionName = formCollection["hdnsession"];
            ViewBag.flag = "1";
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<StudentDetail> stu = null;

            DataTable dt = resultService.ViewStudent(Convert.ToInt32(formCollection["sltclass"]), Convert.ToInt32(formCollection["sltsection"]), Convert.ToInt32(formCollection["sltsession"]), formCollection["admissionId"], formCollection["stdname"]);
            List<StudentDetail> lst = new List<StudentDetail>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add(new StudentDetail
                    {
                        Id = Convert.ToInt32(item["id"]),
                        admissionId = Convert.ToString(item["admissionId"]),
                        firstname = Convert.ToString(item["name"])
                    });

                }

            }
            stu = lst.ToPagedList(pageIndex, pageSize);

            ViewBag.Master = await masterService.GetAllMasters();

            return View("~/Views/Result/ViewData.cshtml", stu);
        }

        public JsonResult MarkSheet(string id, string rid)
        {
            string GridHtml = string.Empty;
            string URI = "http://" + urlpath() + "/Result/ViewResultType";
            string myParameters = "pid=" + id + "&rid=" + rid;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                GridHtml = wc.UploadString(URI, myParameters);
            }

            return Json(new { GridHtml, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult ViewResultType()
        {
            if (Request["pid"] != null)
            {
                List<DataTable> dt = resultService.ReportCard(Convert.ToString(Request["pid"]));
                ViewBag.StudentDetails = dt[0];
                ViewBag.Subject = dt[1];
                ViewBag.Marks = dt[2];
                string viewpath = FindResult(Convert.ToInt32(Request["rid"]));
                return View("~/Views/Result/" + viewpath);
            }
            else
            {
                return Content("No data");
            }
        }

        public string FindResult(int id)
        {
            switch (id)
            {
                case 1:
                    return "TermResult.cshtml";
                case 2:
                    return "CyclicResult.cshtml";
                case 3:
                    return "PeriodicResult.cshtml";
                case 4:
                    return "AnnualResult.cshtml";

            }

            return "";
        }

        public string urlpath()
        {
            string url = "";
            if(Request.IsLocal)
            {
                url = "localhost:" + Request.Url.Port;
            }
            else
            {
                url = Request.Url.Host;
            }

            return url;
        }

    }
}