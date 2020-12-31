using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG_Store_DAL.DTOs;

namespace TCG_Store_DAL.DataAccessControllers
{
    public class InventoryDataController
    {
        public List<InventoryDTO> GetAllInventoryItems()
        {
            List<InventoryDTO> Inventory = new List<InventoryDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetAllInventory = new SqlCommand
            {
                CommandText = "GetAllInventoryItems",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlDataReader DataReader = GetAllInventory.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    InventoryDTO FoundItem = new InventoryDTO();
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        FoundItem.InventoryID = int.Parse(DataReader["InventoryID"].ToString());
                        FoundItem.CardID = int.Parse(DataReader["CardID"].ToString());
                        FoundItem.SealedProductID = int.Parse(DataReader["SealedProductID"].ToString());
                        FoundItem.QualityID = int.Parse(DataReader["QualityID"].ToString());
                        FoundItem.Quantity = int.Parse(DataReader["Quantity"].ToString());
                        FoundItem.FirstEdition = bool.Parse(DataReader["FirstEdition"].ToString());
                    }
                    Inventory.Add(FoundItem);
                }
            }

            DataReader.Close();
            StoreConnection.Close();
            return Inventory;
        }

        public InventoryDTO GetInventoryItemByID(int InventoryID)
        {
            InventoryDTO FoundItem = new InventoryDTO();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand GetItemByID = new SqlCommand
            {
                CommandText = "GetInventoryItemByID",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter InventoryIDParameter = new SqlParameter
            {
                ParameterName = "InventoryID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = InventoryID
            };
            GetItemByID.Parameters.Add(InventoryIDParameter);

            SqlDataReader DataReader = GetItemByID.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        FoundItem.InventoryID = int.Parse(DataReader["InventoryID"].ToString());
                        FoundItem.CardID = int.Parse(DataReader["CardID"].ToString());
                        FoundItem.SealedProductID = int.Parse(DataReader["SealedProductID"].ToString());
                        FoundItem.QualityID = int.Parse(DataReader["QualityID"].ToString());
                        FoundItem.Quantity = int.Parse(DataReader["Quantity"].ToString());
                        FoundItem.FirstEdition = bool.Parse(DataReader["FirstEdition"].ToString());
                    }
                }
            }
            DataReader.Close();
            StoreConnection.Close();
            return FoundItem;
        }

        public bool AddNewInventoryItem(InventoryDTO NewItem)
        {
            bool Success;
            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand InsertIntoInventory = new SqlCommand
            {
                CommandText = "InsertIntoInventory",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter CardIDParameter = new SqlParameter
            {
                ParameterName = "CardID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewItem.CardID
            };
            InsertIntoInventory.Parameters.Add(CardIDParameter);

            SqlParameter SealedProductIDParameter = new SqlParameter
            {
                ParameterName = "SealedProductID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewItem.SealedProductID
            };
            InsertIntoInventory.Parameters.Add(SealedProductIDParameter);

            SqlParameter QuantityParameter = new SqlParameter
            {
                ParameterName = "Quantity",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewItem.Quantity
            };
            InsertIntoInventory.Parameters.Add(QuantityParameter);

            SqlParameter QualityIDParameter = new SqlParameter
            {
                ParameterName = "QualityID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewItem.QualityID
            };
            InsertIntoInventory.Parameters.Add(QualityIDParameter);

            SqlParameter FirstEditionParameter = new SqlParameter
            {
                ParameterName = "FirstEdition",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Bit,
                SqlValue = NewItem.FirstEdition
            };
            InsertIntoInventory.Parameters.Add(FirstEditionParameter);

            InsertIntoInventory.ExecuteNonQuery();

            StoreConnection.Close();
            Success = true;
            return Success;
        }

        public bool UpdateInventoryItem(int InventoryID, int Quantity)
        {
            bool Success;
            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand UpdateInventory = new SqlCommand
            {
                CommandText = "UpdateInventory",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter InventoryIDParameter = new SqlParameter
            {
                ParameterName = "InventoryID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = InventoryID
            };
            UpdateInventory.Parameters.Add(InventoryIDParameter);

            SqlParameter QuantityParameter = new SqlParameter
            {
                ParameterName = "Quantity",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = Quantity
            };
            UpdateInventory.Parameters.Add(QuantityParameter);

            UpdateInventory.ExecuteNonQuery();

            StoreConnection.Close();
            Success = true;
            return Success;
        }

        public bool DeleteInventoryItem(int InventoryID)
        {
            bool Success;
            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand DeleteInventory = new SqlCommand
            {
                CommandText = "DeleteInventoryItem",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter InventoryIDParameter = new SqlParameter
            {
                ParameterName = "InventoryID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = InventoryID
            };
            DeleteInventory.Parameters.Add(InventoryIDParameter);

            DeleteInventory.ExecuteNonQuery();

            StoreConnection.Close();
            Success = true;
            return Success;
        }

        public bool RemoveFromInventory (int InventoryID, int Quantity)
        {
            //leave here for no but in the future. Change it to be in the controller instead. This is logical
            //I want this method to minus from the existing items in the DB and lower stock after an order is placed
            throw new NotImplementedException();
        }
    }
}
