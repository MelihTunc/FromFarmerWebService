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
    public class ConsumerController : ControllerBase
    {
        private readonly UnitOfWork _dbUnit;
        public ConsumerController()
        {
            _dbUnit = new UnitOfWork();
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<ConsumerVM> userList = new List<ConsumerVM>();

            var consumer = _dbUnit.Repo<FF_CONSUMER>().GetAll();
            if (consumer != null)
            {
                foreach (var item in consumer)
                {
                    ConsumerVM tempModel = new ConsumerVM()
                    {
                       IDENTITY_NUMBER=item.IDENTITY_NUMBER,
                       CITY=item.CITY,
                       DISTRICT=item.DISTRICT,
                       NEİGHBORHOOD=item.NEİGHBORHOOD,
                       STREET=item.STREET,
                       APARTMENT=item.APARTMENT,
                       PHONENUMBER=item.PHONE_NUMBER
                    };
                    userList.Add(tempModel);
                }

                return Ok(userList);
            }
            else
            {
                return BadRequest("Error : \"consumerList\" is not null!!");
            }
        }

        [HttpPost("Add")]
        public ActionResult Add(ConsumerVM model)
        {
            FF_CONSUMER tempConsumer = new FF_CONSUMER()
            {
               IDENTITY_NUMBER=model.IDENTITY_NUMBER,
               CITY=model.CITY,
               DISTRICT=model.NEİGHBORHOOD,
               STREET=model.STREET,
               APARTMENT=model.APARTMENT,
               PHONE_NUMBER=model.PHONENUMBER
            };

            var consumer = _dbUnit.Repo<FF_CONSUMER>().Add(tempConsumer);
            if (consumer != null)
                return Ok($"{tempConsumer.IDENTITY_NUMBER} numaralı kullanıcı eklendi.");
            else
                return BadRequest($"{tempConsumer.IDENTITY_NUMBER} numaralı kullanıcı eklenemedi..");

        }

        [HttpPut("Update")]
        public ActionResult Update(ConsumerVM model)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var consumer = ctx.FF_CONSUMER.Where(x => x.IDENTITY_NUMBER == model.IDENTITY_NUMBER).FirstOrDefault();
                if (consumer != null)
                {
                    FF_CONSUMER tempConsumer = new FF_CONSUMER()
                    {
                        IDENTITY_NUMBER = model.IDENTITY_NUMBER,
                        CITY = model.CITY,
                        DISTRICT = model.NEİGHBORHOOD,
                        STREET = model.STREET,
                        APARTMENT = model.APARTMENT,
                        PHONE_NUMBER = model.PHONENUMBER
                    };

                    var userResult = _dbUnit.Repo<FF_CONSUMER>().Update(tempConsumer);
                    if (userResult != null)
                        return Ok($"{model.IDENTITY_NUMBER} numaralı kullanıcı güncellendi.");
                    else
                        return BadRequest($"{model.IDENTITY_NUMBER} numaralı kullanıcı güncellenemedi..");
                }
                else
                    return BadRequest($"{model.IDENTITY_NUMBER} numaralı kullanıcı bulunamadı..");
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var consumer = ctx.FF_CONSUMER.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();
                if (consumer != null)
                {
                    _dbUnit.Repo<FF_CONSUMER>().Delete(Convert.ToInt32(consumer.ID));
                    return Ok($"{identityNumber} numaralı consumer silinmiştir.");
                }
                else
                    return BadRequest($"{identityNumber} numaralı consumer bulunamamıştır!!");
            }
        }
    }
}

