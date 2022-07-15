using CurdOperationshudhanshu.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CurdOperationshudhanshu.Controllers
{
    public class CurdController : Controller
    {
        RecruitmentDbEntities7 obj = new RecruitmentDbEntities7();
        

        public ActionResult Index()
        {
            List<tb_CandidateMaster> abc;
            abc = obj.Database.SqlQuery<tb_CandidateMaster>(@"Exec [dbo].[Sp_List]").ToList();
            return View(abc); 
        }
      
     

        [HttpGet]
        public ActionResult Create()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Create(tb_CandidateMaster con)

        {
            object[] sqlParms = {
                new SqlParameter("@CandidateName",con.CandidateName),
                 new SqlParameter ("@DateOfBirth",con.DateOfBirth),
                 new SqlParameter ("@Discription",con.Discription),
                 new SqlParameter ("@Skills",con.@Skills),
                 new SqlParameter("@Isdeleted",1),
                 new SqlParameter("@Created",1),
                 new SqlParameter("@CreatedOn",DateTime.Now),
                 new SqlParameter("@Modified",0),
                 new SqlParameter("@ModifiedOn",0,0),

            };

            obj.Database.ExecuteSqlCommand("Exec [dbo].[Sp_Insert] @CandidateName,@DateOfBirth,@Discription,@Skills,@Isdeleted,@Created,@CreatedOn,@Modified,@ModifiedOn", sqlParms);
            obj.tb_CandidateMaster.Add(con);
             obj.SaveChanges();
            return RedirectToAction("index");

           
         
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new RecruitmentDbEntities7();
            SqlParameter params1 = new SqlParameter("@Id",Id);
            var data = db.Database.SqlQuery<tb_CandidateMaster>(@"Exec [dbo].[Sp_editGet] @Id", params1).FirstOrDefault();
            return View(data);
        }




        [HttpPost]
        public ActionResult Edit(tb_CandidateMaster con)
        {
            object[] sqlParms = {
                new SqlParameter("@Id",con.Id),
                new SqlParameter("@CandidateName",con.CandidateName),
                 new SqlParameter ("@DateOfBirth",con.DateOfBirth),
                 new SqlParameter ("@Discription",con.Discription),
                 new SqlParameter ("@Skills",con.@Skills),
                 new SqlParameter("@Isdeleted",1),
                 new SqlParameter("@Modified",con.@Modified),
                 new SqlParameter("@ModifiedOn",DateTime.Now)

        };
            obj.Database.ExecuteSqlCommand("Exec [dbo].[sp_Update]@Id,@CandidateName,@DateOfBirth,@Discription,@Skills,@Isdeleted,@Modified,@ModifiedOn", sqlParms);


           

            obj.SaveChanges();
            return RedirectToAction("index");


        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            object[] sqlParams = { new SqlParameter("@Id", Id) };
             var a = obj.Database.ExecuteSqlCommand("Exec [dbo].[Sp_Delete] @Id", sqlParams);

            return RedirectToAction("index");
        }
       

    }
} 