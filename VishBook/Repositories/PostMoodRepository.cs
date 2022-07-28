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
    public class PostMoodRepository : BaseRepository, IPostMoodRepository
    {
        public PostMoodRepository(IConfiguration config) : base(config) { }

        public List<PostMood> GetAllPostMood()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, PostId, MoodId
                                        FROM PostMood";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<PostMood> postmoods = new List<PostMood>();
                        while (reader.Read())
                        {
                            PostMood postmood = new PostMood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                                MoodId = reader.GetInt32(reader.GetOrdinal("MoodId"))

                            };

                            postmoods.Add(postmood);

                        }
                        return postmoods;
                    }
                }
            }
        }

        public PostMood GetPostMoodById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, PostId, MoodId
                                        FROM PostMood
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PostMood postmood = new PostMood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                                MoodId = reader.GetInt32(reader.GetOrdinal("MoodId"))
                            };


                            return postmood;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public void Add(PostMood postmood)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO PostMood (
                                    PostId, MoodId )
                        OUTPUT INSERTED.ID
                        VALUES (
                                    @PostId, @MoodId )";
                    cmd.Parameters.AddWithValue("@PostId", postmood.PostId);
                    cmd.Parameters.AddWithValue("@MoodId", postmood.MoodId);



                    postmood.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Update(PostMood postmood)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE PostMood
                                        SET PostId = @postId,
                                            MoodId = @moodId
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@postId", postmood.PostId);
                    cmd.Parameters.AddWithValue("@moodId", postmood.MoodId);
                    cmd.Parameters.AddWithValue("@id", postmood.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public PostMood GetPostMoodByPostId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, PostId, MoodId
                                        FROM PostMood
                                        WHERE PostId = @postId";

                    cmd.Parameters.AddWithValue("@postId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PostMood postmood = new PostMood
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                                MoodId = reader.GetInt32(reader.GetOrdinal("MoodId"))
                            };


                            return postmood;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

    }
}

