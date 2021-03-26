using FromFarmer.DataAccess.Context;
using FromFarmer.DataAccess.EntityFramework.FromFarmer.UnitOfWork;
using FromFarmer.Entities.Concrete;
using FromFarmer.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FromFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerController : Controller
    {
        private readonly UnitOfWork _dbUnit;

        public FarmerController()
        {
            _dbUnit = new UnitOfWork();
        }
        
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<FarmerVM> farmerList = new List<FarmerVM>();

            var farmer = _dbUnit.Repo<FF_FARMER>().GetAll();
            if (farmer != null)
            {
                foreach (var item in farmer)
                {
                    FarmerVM tempModel = new FarmerVM()
                    {
                       CITY=item.CITY,
                       DISTRICT=item.DISTRICT,
                       TOWN_OR_VILLAGE=item.TOWN_OR_VILLAGE,
                       NEIGHBORHOOD=item.NEIGHBORHOOD,
                       STREET=item.STREET,
                       APARTMENT=item.APARTMENT,
                       FARMER_CERTIFICATE=item.FARMER_CERTIFICATE
                    };
                    farmerList.Add(tempModel);
                }

                return Ok(farmerList);
            }
            else
            {
                return BadRequest("Error : \"farmerList\" is not null!!");
            }
        }

        [HttpGet("GetFarmerByIdentityNumber")]
        public ActionResult GetUserByIdentityNumber(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var farmer = ctx.FF_FARMER.Where(x => x.ID == identityNumber).FirstOrDefault();

                if (farmer != null)
                {
                    FarmerVM tempFarmer = new FarmerVM()
                    {
                        CITY = farmer.CITY,
                        DISTRICT = farmer.DISTRICT,
                        TOWN_OR_VILLAGE = farmer.TOWN_OR_VILLAGE,
                        NEIGHBORHOOD = farmer.NEIGHBORHOOD,
                        STREET = farmer.STREET,
                        APARTMENT = farmer.APARTMENT,
                        FARMER_CERTIFICATE = farmer.FARMER_CERTIFICATE                        
                    };

                    return Ok(tempFarmer);
                }
                else
                {
                    return BadRequest("Error : \"farmer\" is not null!!");
                }
            }
        }

        [HttpPost("Add")]
        public ActionResult Add(FarmerVM model)
        {
            FF_FARMER tempUser = new FF_FARMER()
            {
                CITY=model.CITY,
                DISTRICT=model.DISTRICT,
                TOWN_OR_VILLAGE=model.TOWN_OR_VILLAGE,
                NEIGHBORHOOD=model.NEIGHBORHOOD,
                STREET=model.STREET,
                APARTMENT=model.APARTMENT,
                FARMER_CERTIFICATE=model.FARMER_CERTIFICATE
            };

            var user = _dbUnit.Repo<FF_FARMER>().Add(tempUser);
            if (user != null)
                return Ok($"{tempUser.ID} numaralı Farmer eklendi.");
            else
                return BadRequest($"{tempUser.ID} numaralı Farmer eklenemedi..");
        }

        [HttpPut("Update")]
        public ActionResult Update(FarmerVM model)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var farmer = ctx.FF_FARMER.Where(x => x.IDENTITY_NUMBER == model.IDENTITY_NUMBER).FirstOrDefault();
                if (farmer != null)
                {
                    FF_FARMER tempFarmer = new FF_FARMER()
                    {
                       IDENTITY_NUMBER=model.IDENTITY_NUMBER,
                       CITY=model.CITY,
                       DISTRICT=model.DISTRICT,
                       TOWN_OR_VILLAGE=model.TOWN_OR_VILLAGE,
                       NEIGHBORHOOD=model.NEIGHBORHOOD,
                       STREET=model.STREET,
                       APARTMENT=model.APARTMENT,
                       FARMER_CERTIFICATE=model.FARMER_CERTIFICATE
                     
                    };

                    var farmerResult = _dbUnit.Repo<FF_FARMER>().Update(tempFarmer);
                    if (farmerResult != null)
                        return Ok($"{model.IDENTITY_NUMBER} numaralı farmer güncellendi.");
                    else
                        return BadRequest($"{model.IDENTITY_NUMBER} numaralı farmer güncellenemedi..");
                }
                else
                    return BadRequest($"{model.IDENTITY_NUMBER} numaralı farmer bulunamadı..");
            }
        }
        [HttpDelete("Delete")]
        public ActionResult Delete(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var farmer = ctx.FF_FARMER.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();
                if (farmer != null)
                {
                    _dbUnit.Repo<FF_FARMER>().Delete(Convert.ToInt32(farmer.ID));
                    return Ok($"{identityNumber} numaralı farmer silinmiştir.");
                }
                else
                    return BadRequest($"{identityNumber} numaralı farmer bulunamamıştır!!");
            }
        }
    }
}
