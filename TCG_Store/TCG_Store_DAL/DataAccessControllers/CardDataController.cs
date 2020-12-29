using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TCG_Store_DAL.DTOs;

namespace TCG_Store_DAL.DataAccessControllers
{
    public class CardDataController
    {
        public bool AddCard(CardDTO NewCard)
        {
            bool Success;

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand AddNewCard = new SqlCommand
            {
                CommandText = "InsertIntoCardsInSet",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter SetIDParameter = new SqlParameter
            {
                ParameterName = "SetID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                SqlValue = NewCard.SetID
            };
            AddNewCard.Parameters.Add(SetIDParameter);

            SqlParameter CardCodeInSetParameter = new SqlParameter
            {
                ParameterName = "CardCodeInSet",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.CardCodeInSet
            };
            AddNewCard.Parameters.Add(CardCodeInSetParameter);

            SqlParameter CardNameParameter = new SqlParameter
            {
                ParameterName = "CardName",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.CardName
            };
            AddNewCard.Parameters.Add(CardNameParameter);

            SqlParameter RarityParameter = new SqlParameter
            {
                ParameterName = "Rarity",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.Rarity
            };
            AddNewCard.Parameters.Add(RarityParameter);

            SqlParameter PriceParameter = new SqlParameter
            {
                ParameterName = "Price",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Money,
                SqlValue = NewCard.Price
            };
            AddNewCard.Parameters.Add(PriceParameter);

            SqlParameter ElementalTypeParameter = new SqlParameter
            {
                ParameterName = "ElementalType",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = (NewCard.ElementalType == null) ? "N/A" : NewCard.ElementalType
            };
            AddNewCard.Parameters.Add(ElementalTypeParameter);

            SqlParameter SubTypeParameter = new SqlParameter
            {
                ParameterName = "SubType",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.SubType
            };
            AddNewCard.Parameters.Add(SubTypeParameter);

            SqlParameter SuperTypeParameter = new SqlParameter
            {
                ParameterName = "SuperType",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.SuperType
            };
            AddNewCard.Parameters.Add(SuperTypeParameter);

            SqlParameter PictureLinkParameter = new SqlParameter
            {
                ParameterName = "PictureLink",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.PictureLink
            };
            AddNewCard.Parameters.Add(PictureLinkParameter);

            SqlParameter PictureLinkSmallParameter = new SqlParameter
            {
                ParameterName = "PictureLinkSmall",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.PictureSmallLink
            };
            AddNewCard.Parameters.Add(PictureLinkSmallParameter);

            SqlParameter APIImageIDParameter = new SqlParameter
            {
                ParameterName = "APIImageID",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = NewCard.APIImageID
            };
            AddNewCard.Parameters.Add(APIImageIDParameter);

            AddNewCard.ExecuteNonQuery();
            StoreConnection.Close();

            Success = true;

            return Success;
        }

        public List<CardDTO> SearchCardsByPartialName(string SearchQuery)
        {
            List<CardDTO> CardsMatchingQuery = new List<CardDTO>();

            SqlConnection StoreConnection = new SqlConnection();
            StoreConnection.ConnectionString = "Data Source=.;Initial Catalog=TCGStore;Persist Security Info=True;Integrated Security=true;";
            StoreConnection.Open();

            SqlCommand SeachForCardsByPartialName = new SqlCommand
            {
                CommandText = "SeachForCardsByPartialName",
                CommandType = CommandType.StoredProcedure,
                Connection = StoreConnection
            };

            SqlParameter SearchQueryParameter = new SqlParameter
            {
                ParameterName = "SearchQuery",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.VarChar,
                SqlValue = SearchQuery
            };
            SeachForCardsByPartialName.Parameters.Add(SearchQueryParameter);

            SqlDataReader DataReader = SeachForCardsByPartialName.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    CardDTO FoundCard = new CardDTO();
                    for (int Index = 0; Index < DataReader.FieldCount; Index++)
                    {
                        FoundCard.CardID = int.Parse(DataReader["CardID"].ToString());
                        FoundCard.CardCodeInSet = DataReader["CardCodeInSet"].ToString();
                        FoundCard.SetID = int.Parse(DataReader["SetID"].ToString());
                        FoundCard.CardName = DataReader["CardName"].ToString();
                        FoundCard.Price = decimal.Parse(DataReader["Price"].ToString());
                        FoundCard.Rarity = DataReader["Rarity"].ToString();
                        FoundCard.ElementalType = DataReader["ElementalType"].ToString();
                        FoundCard.SubType = DataReader["SubType"].ToString();
                        FoundCard.SuperType = DataReader["SuperType"].ToString();
                        FoundCard.PictureLink = DataReader["PictureLink"].ToString();
                        FoundCard.PictureSmallLink = DataReader["PictureSmallLink"].ToString();
                    }
                    CardsMatchingQuery.Add(FoundCard);
                }
            }

            DataReader.Close();
            StoreConnection.Close();

            return CardsMatchingQuery;
        }
    }
}
