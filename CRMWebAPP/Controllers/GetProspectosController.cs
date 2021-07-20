using CRMWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRMWebApp.Controllers
{
    public class GetProspectosController : Controller
    {
        // GET: GetProspectos
        public ActionResult Index()
        {

            List<MdlProspecto> pros = new List<MdlProspecto>();
            pros = mdlProspecto();
            return View(pros);
            
        }

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