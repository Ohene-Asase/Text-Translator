using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Text_Translator.Models;

namespace Text_Translator.Repository
{
    public class TransRepository
    {
        private readonly IConfiguration _configuration;

        public TransRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddTranslation(Models.TranslationModel obj)
        {
             string connectionString = _configuration.GetConnectionString("AppDbContext"); 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand com = new SqlCommand("insertTranslations", conn))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@original_text", obj.original_text);
                    com.Parameters.AddWithValue("@translated_text", obj.translated_text);
                    com.Parameters.AddWithValue("@requestedAT", obj.requestedAT);

                    try
                    {
                        conn.Open();
                        int i = com.ExecuteNonQuery();
                        return i >= 1;
                    }
                    catch (Exception ex)
                    {
                       
                        return false;
                    }
                }
            }
        }

        public List<TranslationModel> GetAllTranslations()
        {
            string connectionString = _configuration.GetConnectionString("AppDbContext"); 
            List<TranslationModel> translationList = new List<TranslationModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand com = new SqlCommand("GetRecords", conn)) 
                {
                    com.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = com.ExecuteReader();

                        while (reader.Read())
                        {
                            TranslationModel translation = new TranslationModel
                            {
                               
                                original_text = reader["original_text"].ToString(),
                                translated_text = reader["translated_text"].ToString(),
                                requestedAT = DateTime.Parse(reader["requestedAT"].ToString())
                            };

                            translationList.Add(translation);
                        }

                        return translationList;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

    }
}