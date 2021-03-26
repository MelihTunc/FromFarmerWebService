using FromFarmer.DataAccess.Context;
using FromFarmer.DataAccess.EntityFramework.FromFarmer.UnitOfWork;
using FromFarmer.Entities.Concrete;
using FromFarmer.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FromFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _dbUnit;
        public ProductController()
        {
            _dbUnit = new UnitOfWork();
        }
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<ProductVM> productList = new List<ProductVM>();

            var product = _dbUnit.Repo<FF_PRODUCT>().GetAll();
            if (product != null)
            {
                foreach (var item in product)
                {
                    ProductVM tempModel = new ProductVM()
                    {
                        IDENTITY_NUMBER=item.IDENTITY_NUMBER,
                        TYPE = item.TYPE,
                        CATEGORY=item.CATEGORY,
                        TITLE=item.TITLE,
                        DESCRIPTION=item.DESCRIPTION,
                        FARMER_ID=item.FARMER_ID,
                        PRICE=item.PRICE,
                        QUANTITY=item.QUANTITY
                    };
                    productList.Add(tempModel);
                }

                return Ok(productList);
            }
            else
            {
                return BadRequest("Error : \"productList\" is not null!!");
            }
        }
       
        [HttpPost("Add")]
        public ActionResult Add(ProductVM model)
        {
            FF_PRODUCT tempProduct = new FF_PRODUCT()
            {
                
                TYPE = model.TYPE,
                CATEGORY = model.CATEGORY,
                TITLE = model.TITLE,
                DESCRIPTION = model.DESCRIPTION,
                FARMER_ID = model.FARMER_ID,
                PRICE = model.PRICE,
                QUANTITY = model.QUANTITY          
            };

            var user = _dbUnit.Repo<FF_PRODUCT>().Add(tempProduct);
            if (user != null)
                return Ok($"{tempProduct.ID} numaralı ürün eklendi.");
            else
                return BadRequest($"{tempProduct.ID} numaralı ürün eklenemedi..");
        }
       
        [HttpPut("Update")]
        public ActionResult Update(ProductVM model)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var product = ctx.FF_PRODUCT.Where(x => x.IDENTITY_NUMBER == model.IDENTITY_NUMBER).FirstOrDefault();
                if (product != null)
                {
                    FF_PRODUCT tempProduct = new FF_PRODUCT()
                    {
                        IDENTITY_NUMBER=model.IDENTITY_NUMBER,
                        TYPE = model.TYPE,
                        CATEGORY = model.CATEGORY,
                        TITLE = model.TITLE,
                        DESCRIPTION = model.DESCRIPTION,
                        FARMER_ID = model.FARMER_ID,
                        PRICE = model.PRICE,
                        QUANTITY = model.QUANTITY
                    };

                    var productResult = _dbUnit.Repo<FF_PRODUCT>().Update(tempProduct);
                    if (productResult != null)
                        return Ok($"{model.IDENTITY_NUMBER} numaralı ürün güncellendi.");
                    else
                        return BadRequest($"{model.IDENTITY_NUMBER} numaralı ürün güncellenemedi..");
                }
                else
                    return BadRequest($"{model.IDENTITY_NUMBER} numaralı ürün bulunamadı..");
            }
        }

        [HttpGet("GetProductByIdentityNumber")]
        public ActionResult GetUserByIdentityNumber(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var product = ctx.FF_PRODUCT.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();

                if (product != null)
                {
                    ProductVM tempProduct = new ProductVM()
                    {
                        IDENTITY_NUMBER = product.IDENTITY_NUMBER,
                        TYPE = product.TYPE,
                        CATEGORY = product.CATEGORY,
                        TITLE = product.TITLE,
                        DESCRIPTION = product.DESCRIPTION,
                        FARMER_ID = product.FARMER_ID,
                        PRICE = product.PRICE,
                        QUANTITY = product.QUANTITY
                    };

                    return Ok(tempProduct);
                }
                else
                {
                    return BadRequest("Error : \"product\" is not null!!");
                }
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(int identityNumber)
        {
            using (FromFarmerContext ctx = new FromFarmerContext())
            {
                var product = ctx.FF_PRODUCT.Where(x => x.IDENTITY_NUMBER == identityNumber).FirstOrDefault();
                if (product != null)
                {
                    _dbUnit.Repo<FF_PRODUCT>().Delete(Convert.ToInt32(product.ID));
                    return Ok($"{identityNumber} numaralı ürün silinmiştir.");
                }
                else
                    return BadRequest($"{identityNumber} numaralı ürün bulunamamıştır!!");
            }
        }
    }
}
