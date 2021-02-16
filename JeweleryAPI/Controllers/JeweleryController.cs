using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using JewelAPI.Models;
using JeweleryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JeweleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JeweleryController : ControllerBase
    {
        private JeweleryDBContext _context;
        private IConfiguration _config;

        public JeweleryController(JeweleryDBContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] User model)
        {
            UserModel result = new UserModel();
            try
            {
                if(model != null)
                {
                    var user = (_context.Users.Local.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault());

                    if(user.IsPrivilegedUser)
                    {
                        result.UserName = user.UserName;
                        result.IsPrivilegedUser = user.IsPrivilegedUser;
                        result.Discount = _config.GetValue<int>("UserSettings:Discount");
                    }
                    else
                    {
                        result.UserName = user.UserName;
                        result.IsPrivilegedUser = user.IsPrivilegedUser;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        [HttpPost("CalculatePrice")]
        public async Task<IActionResult> CalculatePrice([FromBody] UserModel model)
        {
            UserModel result = new UserModel();
            try
            {
                if (model != null)
                {
                    if(model.IsPrivilegedUser)
                    {
                        var price = (model.GoldWeightInGrams * model.GoldPriceInGrams);
                        var discount = price / 100 * (model.Discount);
                        result.TotalPrice = price - discount;
                    }
                    else
                    {
                        result.TotalPrice = (model.GoldWeightInGrams * model.GoldPriceInGrams);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        [HttpPost("PDFDownload")]
        public async Task<IActionResult> PDFDownload(UserModel model)
        {

            var response = string.Empty;
            Byte[] Base64 = null;
            try
            {

                if (ModelState.IsValid)
                {
                    Base64 = ObjectToByteArray(model);  
                }
            }
            catch (Exception ex)
            {

            }
            return File(Base64, "application/pdf", "DownloadedFile.pdf");
        }

        private Byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
