using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TCG_Store_DAL.DTOs;

namespace TCG_Store_DAL.DataAccessControllers
{
    public class SealedProductDataController
    {
        public List<SealedProductDTO> GetAllSealedProducts ()
        {
            List<SealedProductDTO> SealedProducts = new List<SealedProductDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetAllSealedProduct = new SqlCommand
            {
                CommandText = "GetAllSealedProduct",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlDataReader DataReader = GetAllSealedProduct.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    SealedProductDTO Product = new SealedProductDTO();
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        Product.SealedProductID = int.Parse(DataReader["SealedProductID"].ToString());
                        Product.SealedProductName = DataReader["SealedProductName"].ToString();
                        Product.SetID = int.Parse(DataReader["SetID"].ToString());
                        Product.Price = decimal.Parse(DataReader["Price"].ToString());
                    }
                    SealedProducts.Add(Product);
                }
            }

            DataReader.Close();
            StoreConnection.Close();
            return SealedProducts;
        }

        public List<SealedProductDTO> GetSealedProductsByGame (int GameID)
        {
            List<SealedProductDTO> SealedProducts = new List<SealedProductDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetSealedProductByGame = new SqlCommand
            {
                CommandText = "GetSealedProductByGame",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter GameIDParameter = new SqlParameter
            {
                ParameterName = "GameID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = GameID
            };
            GetSealedProductByGame.Parameters.Add(GameIDParameter);

            SqlDataReader DataReader = GetSealedProductByGame.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    SealedProductDTO Product = new SealedProductDTO();
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        Product.SealedProductID = int.Parse(DataReader["SealedProductID"].ToString());
                        Product.SealedProductName = DataReader["SealedProductName"].ToString();
                        Product.SetID = int.Parse(DataReader["SetID"].ToString());
                        Product.Price = decimal.Parse(DataReader["Price"].ToString());
                    }
                    SealedProducts.Add(Product);
                }
            }

            DataReader.Close();
            StoreConnection.Close();
            return SealedProducts;
        }

        public SealedProductDTO GetSealedProduct (int SealedProductID)
        {
            SealedProductDTO SealedProduct = new SealedProductDTO();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetSealedProduct = new SqlCommand
            {
                CommandText = "GetSealedProduct",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter SealedProductIDParameter = new SqlParameter
            {
                ParameterName = "SealedProductID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = SealedProductID
            };
            GetSealedProduct.Parameters.Add(SealedProductIDParameter);

            SqlDataReader DataReader = GetSealedProduct.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        SealedProduct.SealedProductID = int.Parse(DataReader["SealedProductID"].ToString());
                        SealedProduct.SetID = int.Parse(DataReader["SetID"].ToString());
                        SealedProduct.SealedProductName = DataReader["SealedProductName"].ToString();
                        SealedProduct.Price = decimal.Parse(DataReader["Price"].ToString());
                    }
                }
            }
            DataReader.Close();
            StoreConnection.Close();
            return SealedProduct;
        }

        public bool AddSealedProduct (SealedProductDTO NewProduct)
        {
            bool Confirmation;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand InsertSealedProduct = new SqlCommand
            {
                CommandText = "InsertSealedProduct",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter SetIDParameter = new SqlParameter
            {
                ParameterName = "SetID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewProduct.SetID
            };
            InsertSealedProduct.Parameters.Add(SetIDParameter);

            SqlParameter SealedProductNameParameter = new SqlParameter
            {
                ParameterName = "SealedProductName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewProduct.SealedProductName
            };
            InsertSealedProduct.Parameters.Add(SealedProductNameParameter);

            SqlParameter PriceParameter = new SqlParameter
            {
                ParameterName = "Price",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Money,
                SqlValue = NewProduct.Price
            };
            InsertSealedProduct.Parameters.Add(PriceParameter);

            InsertSealedProduct.ExecuteNonQuery();

            StoreConnection.Close();

            Confirmation = true;
            return Confirmation;
        }

        public bool UpdateSealedProduct (SealedProductDTO UpdatedProduct)
        {
            bool Confirmation;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand UpdateSealedProduct = new SqlCommand
            {
                CommandText = "UpdateSealedProduct",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter SealedProductIDParameter = new SqlParameter
            {
                ParameterName = "SealedProductID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = UpdatedProduct.SealedProductID
            };
            UpdateSealedProduct.Parameters.Add(SealedProductIDParameter);

            SqlParameter SetIDParameter = new SqlParameter
            {
                ParameterName = "SetID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = UpdatedProduct.SetID
            };
            UpdateSealedProduct.Parameters.Add(SetIDParameter);

            SqlParameter SealedProductNameParameter = new SqlParameter
            {
                ParameterName = "SealedProductName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = UpdatedProduct.SealedProductName
            };
            UpdateSealedProduct.Parameters.Add(SealedProductNameParameter);

            SqlParameter PriceParameter = new SqlParameter
            {
                ParameterName = "Price",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Money,
                SqlValue = UpdatedProduct.Price
            };
            UpdateSealedProduct.Parameters.Add(PriceParameter);

            UpdateSealedProduct.ExecuteNonQuery();

            StoreConnection.Close();
            Confirmation = true;
            return Confirmation;
        }

        public bool DeleteSealedProduct (int SealedProductID)
        {
            bool Confirmation;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand DeleteSealedProduct = new SqlCommand
            {
                CommandText = "DeleteSealedProduct",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter SealedProductIDParameter = new SqlParameter
            {
                ParameterName = "SealedProductID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = SealedProductID
            };
            DeleteSealedProduct.Parameters.Add(SealedProductIDParameter);

            DeleteSealedProduct.ExecuteNonQuery();

            StoreConnection.Close();
            Confirmation = true;
            return Confirmation;
        }
    }
}
