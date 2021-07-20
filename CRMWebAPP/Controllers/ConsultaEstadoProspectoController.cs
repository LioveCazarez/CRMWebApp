using CRMWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRMWebApp.Controllers
{
    public class ConsultaEstadoProspectoController : Controller
    {
        // GET: ConsultaEstadoProspecto
        public ActionResult Index()
        {

            List<MdlProspecto> pros = new List<MdlProspecto>();
            pros = mdlProspecto();
            return View(pros);
        }
        //----------------

        [HttpPost]
        [ActionName("ModificaUsuario")]
        public JsonResult ModificaUsuario(string nombre, string estatus, string comentarios)
        {
            SqlConnection c;
            

            try
            {
                using (c = new SqlConnection(ConfigurationManager.ConnectionStrings["DBEjercicioCRM"].ToString()))
                {
                    c.Open();                  
                    string query= "UPDATE ProspectosInformacion SET Estatus = '"+estatus+"', Comentarios='"+comentarios+"' WHERE Rfc = '"+nombre+"'";
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(query, c))
                    {                        
                        DataSet ds = new DataSet();

                       da.Fill(ds, "result_name");
                        
                    }
                   
                }
              

            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = true, message = ex.Message, name = ex.Message });
            }

            return Json(new { success = true, error = false, message = "Exito al modificar el prospecto: "+nombre });

        }




        //---------------

        public List<MdlProspecto> mdlProspecto()
        {
            List<MdlProspecto> respuesta = new List<MdlProspecto>();

            try
            {
                var url = $"http://www.sdapiprospectos.somee.com/api/ProspectosInformacions";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";



                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            respuesta = JsonConvert.DeserializeObject<List<MdlProspecto>>(responseBody);
                            return respuesta;
                        }

                    }
                }



            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}