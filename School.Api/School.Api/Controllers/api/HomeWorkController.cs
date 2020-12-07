using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using School.Api.Authorization;
using School.Application.Contract;
using School.Application.FactoryApplicationService;
using School.Application.HomeWorksService;
using School.Application.Masters;
using School.Dto.HomeWorks;
using School.Dto.Master;

namespace School.Api.Controllers.api
{
       [CustomAuthorize]
    public class HomeWorkController : ApiController
    {
        private readonly IHomeWorkService homeWorkService;
        private readonly IMastersService masterService;
        
        public HomeWorkController()
        {
            homeWorkService = ApplicatonServiceFactory<HomeWorkService>.Create();
            masterService = ApplicatonServiceFactory<MastersService>.Create();
        }

        #region TeacherHomework

        [HttpPost]
        [Route("AssignmentDetails")]
        public async Task<IHttpActionResult> AssignmentDetails([FromBody]TeacherAssignmentForDto assignmentFrDto)
        {
           if (ModelState.IsValid)
            {
                assignmentFrDto.uploadeOn = DateTime.Now;
                //assignmentFrDto.AssId = assignmentFrDto.subjectId + "_" + DateTime.Now + "_" + assignmentFrDto.classid + "_" + assignmentFrDto.sectionId;
                     var upload = await homeWorkService.UploadAssignment(assignmentFrDto);
                        return Ok<bool>(upload);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("EditAssignmentDetails")]
        public async Task<IHttpActionResult> EditAssignmentDetails([FromBody]TeacherAssignmentForDto assignmentFrDto)
        {
            if (ModelState.IsValid)
            {
                assignmentFrDto.uploadeOn = DateTime.Now;
                //assignmentFrDto.AssId = assignmentFrDto.subjectId + "_" + DateTime.Now + "_" + assignmentFrDto.classid + "_" + assignmentFrDto.sectionId;
                var upload = await homeWorkService.EditAssignment(assignmentFrDto);
                return Ok<bool>(upload);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("ViewAllAssignment")]
        public async Task<IHttpActionResult> ViewAllAssignment()
        {
            var userId = HttpContext.Current.User.Identity.Name;
            var ViewAss = await homeWorkService.ViewAllAssignment(userId);
            return Ok(ViewAss);
        }

        [HttpGet]
        [Route("ViewAssignmentByTeacher")]

        public async Task<IHttpActionResult> ViewAssignmentByTeacher(string userid)
        {
            var ViewAss = await homeWorkService.ViewAssignmentByTeacher(userid);
            return Ok(ViewAss);
        }

        //[HttpGet]
        //[Route("ViewAssignmentbyStudent")]
        //public async Task<IHttpActionResult> ViewAssignmentbyStudent(string UserId)
        //{
        //    var ViewAss = await homeWorkService.ViewAssignmentbyStudent(UserId);
        //    return Ok(ViewAss);
        //}


        [HttpPost]
        [Route("UploadFile")]
        public async Task<HttpResponseMessage> UploadFile()
        {
              List<string> savedFilePath = new List<string>();
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                string rootPath = HttpContext.Current.Server.MapPath("~/UploadHomework");
                var provider = new MultipartFileStreamProvider(rootPath);
                var task = await Request.Content.ReadAsMultipartAsync(provider).
                    ContinueWith<HttpResponseMessage>(t =>
                    {
                        if (t.IsCanceled || t.IsFaulted)
                        {
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                        }
                        foreach (MultipartFileData item in provider.FileData)
                        {
                           
                                string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                                string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                                string FileExtension = Path.GetExtension(name);
                            //if (!(FileExtension=="pdf" || FileExtension == "mp3" || FileExtension == "mp4" || FileExtension == "jpg" || FileExtension == "png"))
                            //{
                            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please select the correct file format");
                            //}
                            File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));

                                Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                                string fileRelativePath = "~/UploadHomework/" + newFileName;
                                Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                                savedFilePath.Add(fileFullPath.ToString());
                           
                        }

                        return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                    });
                return task;
           
        }
        [HttpGet]
        [Route("GetAllMaster")]
        public async Task<IHttpActionResult> GetAllMaster()
        {
            var Master =await masterService.GetAllMasters();
            return Ok(Master);
        }

        [HttpGet]
        [Route("GetSubjectBasedClassId")]
        public async Task<IHttpActionResult> GetSubjectBasedClassId(int ClassId)
        {
            var SubMaster = await masterService.GetAllSubjectMaster(ClassId);
            return Ok(SubMaster);
        }

        [HttpGet]
        [Route("DeleteHW")]
        public async Task<IHttpActionResult> DeleteHW(int FileId)
        {
            var res = await homeWorkService.DeleteHW(FileId);
          //  var filepath = res.Split(',');
            string f = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadHomework");
            for (int i=0;i<res.Count;i++)
            {
                 string f1 = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadHomework/"+ res[i].Split('/').Last());
                if (System.IO.File.Exists(f1))
                {
                    System.IO.File.Delete(f1);
                }
            }
            return Ok<string>(f);
        }

        [HttpGet]
        [Route("DownloadHW")]
        public async Task<IHttpActionResult> DownloadHW(int FileId)
        {
            var res = await homeWorkService.DownloadHW(FileId);
            return Ok(res);
        }
        [HttpGet]
        [Route("EditHW")]
        public async Task<IHttpActionResult> EditHW(int FileId)
        {
            var res = await homeWorkService.EditHW(FileId);
            return Ok(res);
        }


        [HttpGet]
        [Route("SearchHWByTeacher")]
        public async Task<IHttpActionResult> SearchHWByTeacher(int ClassId, int SubId, string Date)
        {
            //TeacherUploadAssignmentForDto.UserId=HttpContext.Current.User.Identity.Name;
            string UserId= HttpContext.Current.User.Identity.Name;
            var res= await homeWorkService.SearchHWByTeacher(ClassId, SubId, Date,UserId);
            return Ok(res);
        }

        [HttpGet]
        [Route("SearchViewHW")]
        public async Task<IHttpActionResult> SearchViewHW(int ClassId, int SubId, string Date)
        {
            var userId = HttpContext.Current.User.Identity.Name;
            var res = await homeWorkService.SearchViewHW(ClassId, SubId, Date,userId);
            return Ok(res);
        }

        #endregion

        #region StudentHomework

        [HttpGet]
        [Route("ViewStudentAssignment")]
        public async Task<IHttpActionResult> ViewStudentAssignment(string UserId)
        {
            //HttpContext.Current.Session["AdmissionId"] = AdmissionId;
            var res = await homeWorkService.ViewStudentAssignment(UserId);
            return Ok(res);
        }

        [HttpGet]
        [Route("ViewAssignmenByAssID")]
        public async Task<IHttpActionResult> ViewAssignmentByAssId(string AssId,string AdmissionId)
        {
            // string AdmissionId = HttpContext.Current.Session["AdmissionId"].ToString();
          
             var res =await homeWorkService.ViewAssignmentByAssId(AssId,AdmissionId);
            return Ok(res);
        }

        [HttpPost]
        [Route("StudentUploadHW")]
        public async Task<IHttpActionResult> StudentUploadHW([FromBody] StudentAssignmentForDto assignmentForDto)
        {
            if (assignmentForDto!=null)
            {
                var upload = await homeWorkService.StudentUploadHW(assignmentForDto);
                return Ok(upload);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetUploadFileName")]
        public async Task<IHttpActionResult> GetUploadFileName(string AssId,string UserId)
        {
            var res= await homeWorkService.GetUploadedFileName(AssId, UserId);
            return Ok(res);
        }

        
        #endregion
    }
}
