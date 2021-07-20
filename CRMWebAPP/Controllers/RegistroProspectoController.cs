using CRMWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CRMWebApp.Controllers
{
    public class RegistroProspectoController : Controller
    {
        // GET: RegistroProspecto
        public ActionResult Index()
        {
            return View();
        }

        //-----------------------------------

        [HttpPost]
        [ActionName("InsertaProspecto")]
        public MdlProspecto mdlProspecto(MdlProspecto mdlProspecto)
        {
            MdlProspecto respuesta = new MdlProspecto();
            mdlProspecto.Estatus = "Enviado";
            mdlProspecto.Archivos = "01010101";
            mdlProspecto.Comentarios = "";
            mdlProspecto.NombreArchivo = "Sin Archivos";
            try
            {
                var url = $"http://www.sdapiprospectos.somee.com/api/ProspectosInformacions";
                var request = (HttpWebRequest)WebRequest.Create(url);

                string json = $"{{\"Nombre\":\"{mdlProspecto.Nombre}\",\"Apellido_Paterno\":\"{mdlProspecto.Apellido_Paterno}\",\"Apellido_Materno\":\"{mdlProspecto.Apellido_Materno}\",\"Calle\":\"{mdlProspecto.Calle}\",\"Numero\":\"{mdlProspecto.Numero}\",\"Colonia\":\"{mdlProspecto.Colonia}\",\"Codigo_Postal\":\"{mdlProspecto.Codigo_Postal}\",\"Telefono\":\"{mdlProspecto.Telefono}\",\"Rfc\":\"{mdlProspecto.Rfc}\",\"Estatus\":\"{mdlProspecto.Estatus}\",\"Comentarios\":\"{mdlProspecto.Comentarios}\",\"Archivos\":\"{mdlProspecto.Archivos}\",\"NombreArchivo\":\"{mdlProspecto.NombreArchivo}\"}}";

              
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            respuesta = JsonConvert.DeserializeObject<MdlProspecto>(responseBody);
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

        //--------------------------------

        [HttpPost]
        [ActionName("InsertaArchivo")]
        public ActionResult InsertaArch(string archivos, string rfc,string nombreArchivo)
        {
            SqlConnection c;

            
            byte[] arch = Encoding.ASCII.GetBytes(archivos);
            int arch1 = BitConverter.ToInt32(arch,0);
         
           

            try
            {
                using (c = new SqlConnection(ConfigurationManager.ConnectionStrings["DBEjercicioCRM"].ToString()))
                {
                    c.Open();
                    string query = "UPDATE ProspectosInformacion SET Archivos = " +arch1+ ", NombreArchivo='"+nombreArchivo+"' WHERE Rfc = '" + rfc + "'";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, c))
                    {
                        DataSet ds = new DataSet();

                        da.Fill(ds, "result_name");

                    }

                }
               

            }
            catch (Exception ex)
            {
                return (null);
            }

            return (null);
        }


    }   
}