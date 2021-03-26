using FromFarmer.DataAccess.Context;
using FromFarmer.DataAccess.EntityFramework.FromFarmer.UnitOfWork;
using FromFarmer.Entities.Concrete;
using FromFarmer.Entities.ViewModel;
using FromFarmer.Utilities.StringCiphers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FromFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _dbUnit;

        public UsersController()
        {
            _dbUnit = new UnitOfWork();
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<UserVM> userList = new List<UserVM>();

            var users = _dbUnit.Repo<FF_USER>().GetAll();
            if (users != null)
            {
                foreach (var item in users)
                {
                    UserVM tempModel = new UserVM()
                    {
                        IdentityNumber = item.IDENTITY_NUMBER,
                        Firstname = item.FIRSTNAME,
                        Lastname = item.LASTNAME,
                        Gender = item.GENDER,
                        DateOfBirth = item.DATE_OF_BIRTH,
                        MailAddress = item.MAIL_ADDRESS,
                        PhoneNumber = item.PHONE_NUMBER,
                        WorkAddress = item.WORK_ADDRESS,
                        IsFarmer = item.IS_FARMER
                    };
                    userList.Add(tempModel);
                }

                return Ok(userList);
            }
            else
            {
                return BadRequest("Error : \"userList\" is not null!!");
            }
        }

        [HttpGet("GetUserByIdentityNumber")]
        public ActionResult GetUserByIdentityNumber(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var user = ctx.FF_USER.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();

                if (user != null)
                {
                    UserVM tempUser = new UserVM()
                    {
                        IdentityNumber = user.IDENTITY_NUMBER,
                        Firstname = user.FIRSTNAME,
                        Lastname = user.LASTNAME,
                        Gender = user.GENDER,
                        DateOfBirth = user.DATE_OF_BIRTH,
                        MailAddress = user.MAIL_ADDRESS,
                        PhoneNumber = user.PHONE_NUMBER,
                        WorkAddress = user.WORK_ADDRESS,
                        IsFarmer = user.IS_FARMER
                    };

                    return Ok(tempUser);
                }
                else
                {
                    return BadRequest("Error : \"user\" is not null!!");
                }
            }
        }

        [HttpPost("Add")]
        public ActionResult Add(UserVM model)
        {
            FF_USER tempUser = new FF_USER()
            {
                IDENTITY_NUMBER = model.IdentityNumber,
                FIRSTNAME = model.Firstname,
                LASTNAME = model.Lastname,
                GENDER = model.Gender,
                DATE_OF_BIRTH = model.DateOfBirth,
                MAIL_ADDRESS = model.MailAddress,
                PHONE_NUMBER = model.PhoneNumber,
                WORK_ADDRESS = model.WorkAddress,
                IS_FARMER = model.IsFarmer,
                PASSWORD = Base64Cipher.Encode("Aa123456")
            };

            var user = _dbUnit.Repo<FF_USER>().Add(tempUser);
            if (user != null)
                return Ok($"{tempUser.IDENTITY_NUMBER} numaralı kullanıcı eklendi.");
            else
                return BadRequest($"{tempUser.IDENTITY_NUMBER} numaralı kullanıcı eklenemedi..");

        }

        [HttpPut("Update")]
        public ActionResult Update(UserVM model)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var user = ctx.FF_USER.Where(x => x.IDENTITY_NUMBER == model.IdentityNumber).FirstOrDefault();
                if (user != null)
                {
                    FF_USER tempUser = new FF_USER()
                    {
                        ID = user.ID,
                        IDENTITY_NUMBER = model.IdentityNumber,
                        FIRSTNAME = model.Firstname,
                        LASTNAME = model.Lastname,
                        GENDER = model.Gender,
                        DATE_OF_BIRTH = model.DateOfBirth,
                        MAIL_ADDRESS = model.MailAddress,
                        PHONE_NUMBER = model.PhoneNumber,
                        WORK_ADDRESS = model.WorkAddress,
                        IS_FARMER = model.IsFarmer
                    };

                    var userResult = _dbUnit.Repo<FF_USER>().Update(tempUser);
                    if (userResult != null)
                        return Ok($"{model.IdentityNumber} numaralı kullanıcı güncellendi.");
                    else
                        return BadRequest($"{model.IdentityNumber} numaralı kullanıcı güncellenemedi..");
                }
                else
                    return BadRequest($"{model.IdentityNumber} numaralı kullanıcı bulunamadı..");
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var user = ctx.FF_USER.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();
                if (user != null)
                {
                    _dbUnit.Repo<FF_USER>().Delete(Convert.ToInt32(user.ID));
                    return Ok($"{identityNumber} numaralı kullanıcı silinmiştir.");
                }
                else
                    return BadRequest($"{identityNumber} numaralı kullanıcı bulunamamıştır!!");
            }
        }
    }
}
