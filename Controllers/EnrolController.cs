
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

// using System.Net.Mqtt;
using MQTTnet;
using MQTTnet.Client;
// Database connection
using AspStudio.Models;
using AspStudio.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

// using System.Net.Http;
// using System.Web.Http;
using System.IO;
using System.Text;


//using System.Web;
//using System.Web.Http;
using System.Net.Http.Headers;

// Json
using System.Text.Json;
using System.Text.Json.Serialization;

// Database connection


using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

using System.Web;
using System.Web.Http.Filters;


namespace AspStudio.Controllers
{

    public class EnrolData
    {
        // public string idlenel { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string documento { get; set; }
        public string tipoDocumento { get; set; }
        public string empresa { get; set; }
        public short regional { get; set; }
        public byte instalacion { get; set; }
        public string ciudad { get; set; }
        public string ciudadOrigen { get; set; }
        public string ssno { get; set; }
        public string idStatus { get; set; }
        public string status { get; set; }
        public string badgeId { get; set; }
        public string metadatos { get; set; }
        public Boolean aceptaTerminos { get; set; }
        public string image { get; set; }
        public string origen { get; set; }

    }

    public class EnrolSearch
    {
        // public string idlenel { get; set; }
        public string BadgeId { get; set; }
        public string Documento { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            base.OnActionExecuted(actionExecutedContext);
        }
    }
    [AllowCrossSiteJson]
    public class EnrolController : Controller
    {
        // Inyecta la instancia de MQTTnet (mqttClient) que fue creada como
        // servicio inyectable en StartUp.cs
        static IMqttClient mqttClient = new MqttFactory().CreateMqttClient();
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext dbContext;


        public EnrolController(ILogger<AccountController> logger, ApplicationDbContext _dbContext)
        {
            _logger = logger;
            dbContext = _dbContext;

        }


        [HttpGet]
        public ViewResult Index()
        {
            var dispositivos = dbContext.Empresas;

            List<dynamic> empresas = new List<dynamic>();
            dynamic empresa;

            try
            {
                foreach (var dispositivo in dispositivos)
                {
                    empresa = new ExpandoObject();
                    empresa.codigo = dispositivo.codigo;
                    empresa.descripcion = dispositivo.descripcion;
                    empresas.Add(empresa);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error generando lista" + e.Message + e.StackTrace);
            }
            
            System.Console.WriteLine(empresas);
            ViewBag.empresas = empresas;





            var regRegionales = dbContext.Regionales;

            List<dynamic> regionales = new List<dynamic>();
            dynamic regional;

            try
            {
                foreach (var registro in regRegionales)
                {
                    regional = new ExpandoObject();
                    regional.Codigo = registro.Codigo;
                    regional.Descripcion = registro.Descripcion;
                    regionales.Add(regional);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error generando lista" + e.Message + e.StackTrace);
            }

            System.Console.WriteLine(regionales);
            ViewBag.regionales = regionales;





            var regInstalaciones = dbContext.Instalaciones;

            List<dynamic> instalaciones = new List<dynamic>();
            dynamic instalacion;

            try
            {
                foreach (var registro in regInstalaciones)
                {
                    instalacion = new ExpandoObject();
                    instalacion.Codigo = registro.Codigo;
                    instalacion.Descripcion = registro.Descripcion;
                    instalaciones.Add(instalacion);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error generando lista" + e.Message + e.StackTrace);
            }

            System.Console.WriteLine(instalaciones);
            ViewBag.instalaciones = instalaciones;







            var texto = dbContext.TextosEnrolam;
            

            List<dynamic> textoEnr = new List<dynamic>();
            List<dynamic> textoEnr1 = new List<dynamic>();
            dynamic textos;

            try
            {
                foreach (var regtexto in texto)
                {
                    textos = new ExpandoObject();
                    textos.id = regtexto.id;
                    textos.Texto = regtexto.Texto;
                    textos.Fecha = regtexto.Fecha;
                    textos.Tipo = regtexto.Tipo;
                    textos.Version = regtexto.Version;
                    textos.Pregunta = regtexto.Pregunta;
                    textoEnr.Add(textos);
                }
                /*
                var results = from x in textoEnr
                              group new { x.Tipo } by x.Tipo;

                foreach (var xTipo in results)
                {

                    var txts = from s in dbContext.TextosEnrolam select s;

                    textos = textoEnr.Where(s => s.Tipo.Contains(xTipo));

                    foreach (var tipoVer in textos)
                    {



                    }

                }
                */
                //textoEnr1 = (List<dynamic>)textoEnr.OrderBy(x => x.Tipo);

                /*
                var results = from x in textoEnr
                              group new { x.Texto, x.Version } by x.Tipo;*/


            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Error generando lista" + e.Message + e.StackTrace);
            }

            System.Console.WriteLine(textoEnr);
            ViewBag.textoEnr = textoEnr;

            return View();
        }


        [HttpGet]
        [Route("/api/get_enrol")]
        [EnableCors("CorsPolicy")]
        // [DisableCors]
        public  Object getEnrol(EnrolSearch enrolsearch) {

            try {
                // HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                // HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
                // HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                var result = from o in dbContext.EnrolTemporal
                            select o;
                
                if (enrolsearch.BadgeId != null)
                    result = result.Where(c => c.Badge_id == enrolsearch.BadgeId);
                
                if (enrolsearch.Documento != null)
                    result = result.Where(c => c.Documento == enrolsearch.Documento);
                
                if (enrolsearch.LastName != null)
                    result = result.Where(c => c.LastName == enrolsearch.LastName);
                
                if (enrolsearch.FirstName != null)
                    result = result.Where(c => c.FirstName == enrolsearch.FirstName);

                //var result = dbContext.EnrolTemporal.FirstOrDefault(p => p.IdLenel == IdLenel);
                if (result != null)
                {
                     
                    //  Access-Control-Allow-Origin: *

                    // Retorna Json indicando que ya existe
                    return new {success=true, data=result};
                } else {
                    return new {success=false, message = "Registro no encontrado"};
                }
                
            } catch (Exception e) {
                System.Console.WriteLine("Error :" + e.Message + e.StackTrace);
                // Retorna Json indicando que fue exitoso
                return new {success=false, message = "Database communication error"};
            }
        }


        [HttpPost]
        [Route("/api/create_enrol")]
        [EnableCors("CorsPolicy")]
        // [DisableCors]
            
        public Object CreateEnrol([FromBody] EnrolData mensaje)
        {
            
            if (mensaje != null){
                System.Console.WriteLine(mensaje);

                try {

                    dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    var employeeDb = dbContext.EnrolTemporal.FirstOrDefault(p => p.Badge_id == mensaje.badgeId);

                    System.Console.WriteLine(employeeDb);

                    EnrolTemp empleado = new EnrolTemp();
                    DateTime localDate = DateTime.Now;  

                    var imageUrl = "";
                    if (mensaje.image != "") {
                        imageUrl = ExportToImage(mensaje.image, mensaje.documento);
                    } else {
                        imageUrl = "/Enrols/avatar.png";
                    }
                    
                    

                    // empleado.IdLenel = mensaje.idlenel;
                    empleado.Badge_id = mensaje.badgeId;
                    empleado.FirstName = mensaje.firstName;
                    empleado.LastName = mensaje.lastName;
                    empleado.Documento = mensaje.documento;
                    empleado.tipoDocumento = mensaje.tipoDocumento;
                    empleado.Empresa = mensaje.empresa;
                    empleado.Regional = (short)mensaje.regional;
                    empleado.Instalacion = mensaje.instalacion;
                    empleado.Metadatos = mensaje.metadatos;
                    empleado.Ciudad = mensaje.ciudad;
                    empleado.ciudadOrigen = mensaje.ciudadOrigen;
                    empleado.aceptaTerminos = mensaje.aceptaTerminos;
                    empleado.imageUrl = imageUrl;
                    empleado.SSNO = "";
                    empleado.IdStatus = "";
                    empleado.Status = "";
                    empleado.origen = mensaje.origen;
                    empleado.Created = localDate;

                    if(employeeDb != null) {
                        empleado.Id = employeeDb.Id;
                        empleado.Created = employeeDb.Created;
                        empleado.Updated = localDate;
                        dbContext.EnrolTemporal.Update(empleado);
                        dbContext.SaveChanges();
                    } else {
                        dbContext.EnrolTemporal.Add(empleado);
                        dbContext.SaveChanges();
                    }

                    return Json(new { success = true });

                } catch (Exception ex) {
                    //throw ex;
                    return Json(new { success = false, msg = ex.Message });

                }

            } else {
                return Json(new { success = false, msg = "No valid data present" });
            }



        }




        [HttpPost]

        public ActionResult IngresoEmployee(string badge_id, string name, string lastname, string documento, string empresa, Int16 regional, Byte instalacion, string Ciudad)
        {

            try
            {

                EnrolTemp empleado = new EnrolTemp();

                empleado.Badge_id = badge_id;
                empleado.FirstName = name;
                empleado.LastName = lastname;
                empleado.Documento = documento;
                empleado.Empresa = empresa;
                empleado.Regional = regional;
                empleado.Instalacion = instalacion;
                empleado.Ciudad = Ciudad;
                empleado.SSNO = "";
                empleado.IdStatus = "";
                empleado.Status = "";

                dbContext.EnrolTemporal.Add(empleado);
                dbContext.SaveChanges();

                return RedirectToAction("Logout", "Account");

            }

            catch (Exception ex)
            {
                //throw ex;
                System.Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Enrol");

            }

            

        }



        protected string ExportToImage(string imagen, string documento)
        {

            //  Convierte la cadena base64 en un arreglo de bytes
            byte[] bytes = Convert.FromBase64String(imagen);

            // Define el nombre del archivo a guardar (Nombre de la persona + id dispositivo + fecha)

            var imageName = documento + ".jpg";
            System.Console.WriteLine(imageName);

            // Define la ruta del directorio y de la imagen
            var folderPath = "wwwroot/Enrols/";
            var imagePath = folderPath + imageName;
            var imageUrl = "/Enrols/" + imageName;

            // Guarda la imagen en el sistema de archivos
            // using (Image image = Image.FromStream(new MemoryStream(bytes)))
            // {
            //     try
            //     {
            //         image.Save(imagePath, ImageFormat.Jpeg);  // Or Png
            //     }
            //     catch (System.Exception e)
            //     {
            //         System.Console.WriteLine("Error saving " + imagePath + " in filesystem" + e.Message + e.StackTrace);
            //     }

            // }
            using (var imageFile = new FileStream(Path.Combine(folderPath,imageName), FileMode.Create))
            {
                try
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("Error saving " + imagePath + " in filesystem" + e.Message + e.StackTrace);
                }

            }

            return (imageUrl);
        }

        /*
         fecha de autorización,
hora de autorización, 
versión de texto presentada, 
responsable de la caprtura de datos,
ubicación*/



    }
}