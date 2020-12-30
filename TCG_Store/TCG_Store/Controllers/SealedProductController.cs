using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TCG_Store.Models;
using TCG_Store_DAL.DataAccessControllers;
using TCG_Store_DAL.DTOs;

namespace TCG_Store.Controllers
{
    [Route("api/v1/SealedProduct")]
    [ApiController]
    public class SealedProductController
    {
        [HttpGet]
        public List<SealedProduct> Get()
        {
            List<SealedProduct> AllSealedProducts = new List<SealedProduct>();
            List<SealedProductDTO> SealedProductDTOs = new List<SealedProductDTO>();
            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTOs = SealedProductDataController.GetAllSealedProducts();

            foreach (var Product in SealedProductDTOs)
            {
                SealedProduct FoundProduct = new SealedProduct
                {
                    SealedProductID = Product.SealedProductID,
                    SetID = Product.SetID,
                    SealedProductName = Product.SealedProductName,
                    Price = Product.Price
                };
                AllSealedProducts.Add(FoundProduct);
            }

            return AllSealedProducts;
        }

        [Route("/GameID={GameID}")]
        [HttpGet]
        public List<SealedProduct> GetSealedProductByGame (int GameID)
        {
            List<SealedProduct> SealedProducts = new List<SealedProduct>();
            List<SealedProductDTO> SealedProductDTOs = new List<SealedProductDTO>();
            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTOs = SealedProductDataController.GetSealedProductsByGame(GameID);

            foreach (var Product in SealedProductDTOs)
            {
                SealedProduct FoundProduct = new SealedProduct
                {
                    SealedProductID = Product.SealedProductID,
                    SetID = Product.SetID,
                    SealedProductName = Product.SealedProductName,
                    Price = Product.Price
                };
                SealedProducts.Add(FoundProduct);
            }

            return SealedProducts;
        }

        [Route("/SealedProductID={SealedProductID}")]
        [HttpGet]
        public SealedProduct GetSealedProductByID (int SealedProductID)
        {
            SealedProduct FoundProduct = new SealedProduct();
            SealedProductDTO SealedProductDTO = new SealedProductDTO();
            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTO = SealedProductDataController.GetSealedProduct(SealedProductID);

            FoundProduct.SealedProductID = SealedProductDTO.SealedProductID;
            FoundProduct.SetID = SealedProductDTO.SetID;
            FoundProduct.SealedProductName = SealedProductDTO.SealedProductName;
            FoundProduct.Price = SealedProductDTO.Price;

            return FoundProduct;
        }

        [HttpPost]
        public bool Post ([FromBody] SealedProduct NewProduct)
        {
            bool Confrimation = false;

            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTO ProductDTO = new SealedProductDTO
            {
                SealedProductID = NewProduct.SealedProductID,
                SealedProductName = NewProduct.SealedProductName,
                SetID = NewProduct.SetID,
                Price = NewProduct.Price
            };

            Confrimation = SealedProductDataController.AddSealedProduct(ProductDTO);
            
            return Confrimation;
        }

        [Route("{SealedProductID:int}")]
        [HttpDelete]
        public bool Delete (int SealedProductID)
        {
            bool Confirmation = false;

            SealedProductDataController SealedProductDataController = new SealedProductDataController();

            Confirmation = SealedProductDataController.DeleteSealedProduct(SealedProductID);

            return Confirmation;
        }

        [Route("{SealedProduct}")]
        [HttpPut]
        public bool Put ([FromBody] SealedProduct UpdatedSealedProduct)
        {
            bool Confirmation = false;

            SealedProductDataController SealedProductDataController = new SealedProductDataController();
            SealedProductDTO ProductDTO = new SealedProductDTO
            {
                SealedProductID = UpdatedSealedProduct.SealedProductID,
                SealedProductName = UpdatedSealedProduct.SealedProductName,
                SetID = UpdatedSealedProduct.SetID,
                Price = UpdatedSealedProduct.Price
            };

            Confirmation = SealedProductDataController.UpdateSealedProduct(ProductDTO);

            return Confirmation;
        }
    }
}
