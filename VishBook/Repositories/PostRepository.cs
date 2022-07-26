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
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IConfiguration config) : base(config) { }

        public void Add(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Post (
                            Title, Content, CreateDateTime, UserId )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @Title, @Content, @CreateDateTime, @UserId )";
                    cmd.Parameters.AddWithValue("@Title", post.Title);
                    cmd.Parameters.AddWithValue("@Content", post.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", post.CreateDateTime);
                    cmd.Parameters.AddWithValue("@UserId", post.UserId);


                    post.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Update(Post post)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Post
                                        SET Title = @title,
                                            Content = @content
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@title", post.Title);
                    cmd.Parameters.AddWithValue("@content", post.Content);
                    cmd.Parameters.AddWithValue("@id", post.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Post post)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Post WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", post.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Post> GetAllPosts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, Content, CreateDateTime, UserId
                                        FROM Post";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Post> posts = new List<Post>();    
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                            };

                            posts.Add(post);

                        }
                        return posts;
                    }
                }
            }
        }

        public Post GetPostById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, Content, CreateDateTime, UserId
                                        FROM Post
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Post post = new Post
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = !reader.IsDBNull(reader.GetOrdinal("Title")) ? reader.GetString(reader.GetOrdinal("Title")) : " ",
                                Content = !reader.IsDBNull(reader.GetOrdinal("Content")) ? reader.GetString(reader.GetOrdinal("Content")) : " ",
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                            };
                            return post;
                        }
                        else
                        {
                            return null;    
                        }
                    }
                }
            }
        }
        public List<Post> GetPostByUserId(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, Content, CreateDateTime, UserId
                                        FROM Post
                                        WHERE UserId = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Post> posts = new List<Post>();
                        
                        while (reader.Read())
                        {
                            Post post = new Post()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                            };

                            if (reader.IsDBNull(reader.GetOrdinal("Title")) == false)
                            {
                                post.Title = reader.GetString(reader.GetOrdinal("Title"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("Content")) == false)
                            {
                                post.Content = reader.GetString(reader.GetOrdinal("Content"));
                            }
                            posts.Add(post);
                        }

                        return posts;
                    }
                }
            }
        }

    }
}

