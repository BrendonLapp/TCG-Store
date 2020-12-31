using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG_Store_DAL.DTOs;
using System.Data;

namespace TCG_Store_DAL.DataAccessControllers
{
    /// <summary>
    /// Data controller for quality
    /// </summary>
    public class QualityDataController
    {
        /// <summary>
        /// Gets a quality entry based on the provided ID
        /// </summary>
        /// <param name="QualityID">The provided quality ID</param>
        /// <returns>QualityDTO</returns>
        public QualityDTO GetQualityByID (int QualityID)
        {
            QualityDTO FoundQuality = new QualityDTO();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand SearchForQualityByID = new SqlCommand
            {
                CommandText = "GetQualityByID",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter QualityIDParameter = new SqlParameter
            {
                ParameterName = "QualityID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = QualityID
            };
            SearchForQualityByID.Parameters.Add(QualityIDParameter);

            SqlDataReader DataReader = SearchForQualityByID.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        FoundQuality.QualityID = int.Parse(DataReader["QualityID"].ToString());
                        FoundQuality.QualityPercentage = decimal.Parse(DataReader["QualityPercentage"].ToString());
                        FoundQuality.QualityName = DataReader["QualityName"].ToString();
                        FoundQuality.QualityShortName = DataReader["QualityShortName"].ToString();
                    }
                }
            }
            DataReader.Close();
            StoreConnection.Close();

            return FoundQuality;
        }

        /// <summary>
        /// Gets all qualities saved in the database
        /// </summary>
        /// <returns>List of QualityDTOs</returns>
        public List<QualityDTO> GetAllQualities ()
        {
            List<QualityDTO> AllQualities = new List<QualityDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand SeacrchForAllQualities = new SqlCommand
            {
                CommandText = "GetAllQualities",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlDataReader DataReader = SeacrchForAllQualities.ExecuteReader();

            if (DataReader.Read())
            {
                while (DataReader.HasRows)
                {
                    QualityDTO IncomingQuality = new QualityDTO();
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        IncomingQuality.QualityID = int.Parse(DataReader["QualityID"].ToString());
                        IncomingQuality.QualityPercentage = decimal.Parse(DataReader["QualityPercentage"].ToString());
                        IncomingQuality.QualityName = DataReader["QualityName"].ToString();
                        IncomingQuality.QualityShortName = DataReader["QualityShortName"].ToString();
                    }
                    AllQualities.Add(IncomingQuality);
                }
            }
            DataReader.Close();
            StoreConnection.Close();
            
            return AllQualities;
        }

        /// <summary>
        /// Adds a new quality to the database
        /// </summary>
        /// <param name="NewQuality">Quality DTO to be added</param>
        /// <returns>Success as a bool</returns>
        public bool AddNewQuality(QualityDTO NewQuality)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand InsertIntoQuality = new SqlCommand
            {
                CommandText = "InsertIntoQuality",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter QualityNameParameter = new SqlParameter
            {
                ParameterName = "QualityName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewQuality.QualityName
            };
            InsertIntoQuality.Parameters.Add(QualityNameParameter);

            SqlParameter QualityShortNameParameter = new SqlParameter
            {
                ParameterName = "QualityShortName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewQuality.QualityShortName
            };
            InsertIntoQuality.Parameters.Add(QualityShortNameParameter);

            SqlParameter QualityPercentageParamter = new SqlParameter
            {
                ParameterName = "QualityPercentage",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Decimal,
                SqlValue = NewQuality.QualityPercentage
            };
            InsertIntoQuality.Parameters.Add(QualityPercentageParamter);
            try
            {
                InsertIntoQuality.ExecuteNonQuery();
                Success = true;
            }
            catch
            {
                Success = false;
            }

            return Success;
        }

        /// <summary>
        /// Performs an update on the suppled quality
        /// </summary>
        /// <param name="UpdatedQuality">Quality DTO to update</param>
        /// <returns>Success as bool</returns>
        public bool UpdateQuality (QualityDTO UpdatedQuality)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand UpdateQuality = new SqlCommand
            {
                CommandText = "UpdateQuality",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter QualityIDParameter = new SqlParameter
            {
                ParameterName = "QualityID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = UpdatedQuality.QualityID
            };
            UpdateQuality.Parameters.Add(QualityIDParameter);

            SqlParameter QualityNameParameter = new SqlParameter
            {
                ParameterName = "QualityName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = UpdatedQuality.QualityName
            };
            UpdateQuality.Parameters.Add(QualityNameParameter);

            SqlParameter QualityPercentageParameter = new SqlParameter
            {
                ParameterName = "QualityPercentage",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Decimal,
                SqlValue = UpdatedQuality.QualityPercentage
            };
            UpdateQuality.Parameters.Add(QualityPercentageParameter);

            SqlParameter QualityShortNameParameter = new SqlParameter
            {
                ParameterName = "QualityShortName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = UpdatedQuality.QualityShortName
            };
            UpdateQuality.Parameters.Add(QualityShortNameParameter);

            UpdateQuality.ExecuteNonQuery();
            StoreConnection.Close();
            Success = true;
            return Success;
        }

        public bool DeleteQuality (int QualityID)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand DeleteQuality = new SqlCommand
            {
                CommandText = "DeleteQuality",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter QualityIDParameter = new SqlParameter
            {
                ParameterName = "QualityID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = QualityID
            };
            DeleteQuality.Parameters.Add(QualityIDParameter);

            DeleteQuality.ExecuteNonQuery();

            StoreConnection.Close();
            Success = true;
            return Success;
        }
    }
}
