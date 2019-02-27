using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using System.IO;
using System.Drawing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CompaugesWebApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace CompaugesWebApi.Controllers
{
    public class fileSaveController : ApiController
    {
        GTStagingNewEntities db = new GTStagingNewEntities();
        public IHttpActionResult Post([FromBody]  SearchInfo.ImageInfo image)
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            Logger.TraceLog("filse save");
            Image _image = null;
            string fileName = image.ImageName.Split('.')[0] + DateTime.Now.ToString("yyyyMMddHHmmss")+"." +  image.ImageName.Split('.')[1]; 
            MemoryStream mStream = new MemoryStream();

            byte[] data = Convert.FromBase64String(image.ProfileImage.ToString().Replace(" ", "+"));

            mStream = new MemoryStream(data);
            _image = Image.FromStream(mStream);
            string ImagePath = HttpContext.Current.Server.MapPath("~/Images/");
            //_image.Save(@"D:\angular\GrptalkCompauges\CompaugesWebApi\Images\test.png");
            _image.Save(ImagePath + fileName);
            var CompId = identityClaims.FindFirst("CompId").Value;

            List<SqlParameter> dbParams = new List<SqlParameter>();
            dbParams.Add(new SqlParameter("@fileName", "/Images/"+fileName));
            dbParams.Add(new SqlParameter("@CompId", CompId.ToString() ));


            SqlParameter OutputParam = new SqlParameter("@retVal", SqlDbType.TinyInt, 20);
            OutputParam.Direction = ParameterDirection.Output;
            dbParams.Add(OutputParam);

            SqlParameter OutputParam1 = new SqlParameter("@retMsg", SqlDbType.VarChar, 200);
            OutputParam1.Direction = ParameterDirection.Output;
            dbParams.Add(OutputParam1);

            var results = new GTStagingNewEntities()
                   .MultipleResults("[dbo].[Comp_UploadProfilePic]", dbParams)
                   .Execute();

            return Ok<string>(fileName); ;
        }

    }
}
