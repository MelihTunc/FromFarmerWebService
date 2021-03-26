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
    public class OrderController : ControllerBase
    {
        private readonly UnitOfWork _dbUnit;
        public OrderController()
        {
            _dbUnit = new UnitOfWork();
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<OrderVM> orderList = new List<OrderVM>();

            var order = _dbUnit.Repo<FF_ORDER>().GetAll();
            if (order != null)
            {
                foreach (var item in order)
                {
                    OrderVM tempModel = new OrderVM()
                    {
                        IDENTITY_NUMBER = item.IDENTITY_NUMBER,
                        STATUS =item.STATUS,
                        CONSUMER_ID=item.CONSUMER_ID,
                        DATE_TİME=item.DATE_TİME,
                        DELIVERY_ADDRESS=item.DELIVERY_ADDRESS,
                        PAYMENT_INFORMATION=item.PAYMENT_INFORMATION,
                        SHIPPING_FREE=item.SHIPPING_FREE,
                        TOTAL_PRICE=item.TOTAL_PRICE,
                        SHIPPING_TRACKING_NUMBER=item.SHIPPING_TRACKING_NUMBER                      
                    };
                    orderList.Add(tempModel);
                }

                return Ok(orderList);
            }
            else
            {
                return BadRequest("Error : \"orderList\" is not null!!");
            }
        }

        [HttpGet("GetUserByIdNumber")]
        public ActionResult GetOrderByIdentityNumber(int id)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var order = ctx.FF_ORDER.Where(x => x.ID == id).FirstOrDefault();

                if (order != null)
                {
                    OrderVM tempOrder = new OrderVM()
                    {
                        IDENTITY_NUMBER = order.IDENTITY_NUMBER,
                        STATUS = order.STATUS,
                        CONSUMER_ID = order.CONSUMER_ID,
                        DATE_TİME = order.DATE_TİME,
                        DELIVERY_ADDRESS = order.DELIVERY_ADDRESS,
                        PAYMENT_INFORMATION = order.PAYMENT_INFORMATION,
                        SHIPPING_FREE = order.SHIPPING_FREE,
                        TOTAL_PRICE = order.TOTAL_PRICE,
                        SHIPPING_TRACKING_NUMBER = order.SHIPPING_TRACKING_NUMBER
                    };
                    return Ok(tempOrder);
                }
                else
                {
                    return BadRequest("Error : \"order\" is not null!!");
                }
            }
        }

        [HttpPost("Add")]
        public ActionResult Add(OrderVM model)
        {
            FF_ORDER tempOrder = new FF_ORDER()
            {
                IDENTITY_NUMBER = model.IDENTITY_NUMBER,
                STATUS = model.STATUS,
                CONSUMER_ID = model.CONSUMER_ID,
                DATE_TİME = model.DATE_TİME,
                DELIVERY_ADDRESS = model.DELIVERY_ADDRESS,
                PAYMENT_INFORMATION = model.PAYMENT_INFORMATION,
                SHIPPING_FREE = model.SHIPPING_FREE,
                TOTAL_PRICE = model.TOTAL_PRICE,
                SHIPPING_TRACKING_NUMBER = model.SHIPPING_TRACKING_NUMBER
            };

            var order = _dbUnit.Repo<FF_ORDER>().Add(tempOrder);
            if (order != null)
                return Ok($"{tempOrder.ID} numaralı kullanıcı eklendi.");
            else
                return BadRequest($"{tempOrder.ID} numaralı kullanıcı eklenemedi..");

        }

        [HttpPut("Update")]
        public ActionResult Update(OrderVM model)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var order = ctx.FF_ORDER.Where(x => x.IDENTITY_NUMBER == model.IDENTITY_NUMBER).FirstOrDefault();
                if (order != null)
                {
                    FF_ORDER tempUser = new FF_ORDER()
                    {
                        IDENTITY_NUMBER=model.IDENTITY_NUMBER,
                        STATUS = model.STATUS,
                        CONSUMER_ID = model.CONSUMER_ID,
                        DATE_TİME = model.DATE_TİME,
                        DELIVERY_ADDRESS = model.DELIVERY_ADDRESS,
                        PAYMENT_INFORMATION = model.PAYMENT_INFORMATION,
                        SHIPPING_FREE = model.SHIPPING_FREE,
                        TOTAL_PRICE = model.TOTAL_PRICE,
                        SHIPPING_TRACKING_NUMBER = model.SHIPPING_TRACKING_NUMBER
                    };

                    var userResult = _dbUnit.Repo<FF_ORDER>().Update(tempUser);
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
                var order = ctx.FF_ORDER.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();
                if (order != null)
                {
                    _dbUnit.Repo<FF_ORDER>().Delete(Convert.ToInt32(order.ID));
                    return Ok($"{identityNumber} numaralı order silinmiştir.");
                }
                else
                    return BadRequest($"{identityNumber} numaralı order bulunamamıştır!!");
            }
        }
    }
}
