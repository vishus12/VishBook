using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using VishBook.Models;
using VishBook.Utils;

namespace VishBook.Repositories
{
    public class MoodRepository : BaseRepository, IMoodRepository
    {
        public MoodRepository(IConfiguration config) : base(config) { }

        public List<Mood> GetAllMood()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name
                                        FROM Mood";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Mood> moods = new List<Mood>();
                        while (reader.Read())
                        {
                            Mood mood = new Mood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))

                            };

                            moods.Add(mood);

                        }
                        return moods;
                    }
                }
            }
        }

        public Mood GetMoodById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name
                                        FROM Mood
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Mood mood = new Mood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };


                            return mood;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<Mood> GetAllMoodById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.Id AS mid, m.Name, pm.Id AS pmid, pm.PostId, pm.MoodId
                                        FROM Mood m
                                        LEFT JOIN PostMood pm ON m.Id = pm.MoodId
                                        WHERE PostId =  @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Mood> moods = new List<Mood>();
                        while (reader.Read())
                        {
                            Mood mood = new Mood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("mid")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))

                            };
                            moods.Add(mood);

                        }
                  
                            return moods;
                    }
                }
            }
        }

    }
}

