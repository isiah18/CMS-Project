using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Model;
using Dapper;

namespace ContentManagementSystem.Data.Repository
{
	public class PostRepository : IPostRepository
	{
		public int Add(Post post)
		{
			using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add("postID", DbType.Int32, direction: ParameterDirection.Output);
				p.Add("AuthorID", post.Author.Id);
				p.Add("PostStatusID", post.Status.Id);
				p.Add("CreatedDate", post.CreatedDate);
				p.Add("PublishDate", post.PublishDate);
				p.Add("ExpireDate", post.ExpireDate);
				p.Add("Title", post.Title);
				p.Add("Content", post.Content);
				cn.Execute("CreatePost", p, commandType: CommandType.StoredProcedure);

				return p.Get<int>("postID");
			}
		}

		public void Update(Post p)
		{
			throw new NotImplementedException();
		}

		public void Remove(int id)
		{
			using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
			{
				var p = new DynamicParameters();
				p.Add("postID", id);
				cn.Execute("DeletePost", p, commandType: CommandType.StoredProcedure);
			}
		}

		public Post Get(int id)
		{
			Post post;
			var p = new DynamicParameters();
			p.Add("@postID", id, DbType.Int32);
			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				post = cn.Query<Post>("SELECT * FROM Posts WHERE Id = @postID", p).First();
				post.Status = cn.Query<PostStatus>("GetStatusForSinglePost", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			return post;
		}

		public List<Post> GetAll()
		{
			var postList = new List<Post>();

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				var cmd = new SqlCommand();
				cmd.CommandText = "GetAllPosts";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = cn;

				cn.Open();

				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						postList.Add(PopulateFromDataReader(dr));
					}
				}
				return postList;
			}
		}

		public int GetTotalPostCountByStatus(string status)
		{
			var p = new DynamicParameters();
			p.Add("@Status", status);

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				return cn.Query<int>("SELECT COUNT(p.Id) FROM Posts p INNER JOIN PostStatuses ps ON p.PostStatusId = ps.Id WHERE ps.Status = @Status", p).First();
			}
		}

		public int GetTotalPostCountByStatusAndTag(string status, int tagId)
		{
			var p = new DynamicParameters();
			p.Add("@TagId", tagId);
			p.Add("@Status", status);

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				return cn.Query<int>("SELECT COUNT(p.Id) FROM Posts p INNER JOIN PostStatuses ps ON p.PostStatusId = ps.Id INNER JOIN TagsOnPosts tonp ON p.Id = tonp.PostId WHERE ps.Status = @Status AND tonp.TagId = @TagId", p).First();
			}
		}

		public int GetTotalPostCountByStatusAndCategory(string status, int catId)
		{
			var p = new DynamicParameters();
			p.Add("@CategoryId", catId);
			p.Add("@Status", status);

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				return cn.Query<int>("SELECT COUNT(p.Id) FROM Posts p INNER JOIN PostStatuses ps ON p.PostStatusId = ps.Id INNER JOIN CategoriesOnPosts conp ON p.Id = conp.PostId WHERE ps.Status = @Status AND conp.CategoryId = @CategoryId", p).First();
			}
		}

		public List<Post> GetPostsPaginationByStatus(int offset, int maxAmount, string status)
		{
			var posts = new List<Post>();

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				var cmd = new SqlCommand
				{
					CommandType = CommandType.Text,
					CommandText = @"SELECT p.Id [PostId], p.[AuthorId], u.[UserName], p.[CreatedDate], p.[PublishDate], p.[ExpireDate], p.[Title], p.[Content], ps.Id [PostStatusId], ps.[Status]
										FROM Posts p
											INNER JOIN PostStatuses ps ON p.PostStatusId = ps.Id
											INNER JOIN AspNetUsers u ON p.AuthorId = u.Id
											WHERE ps.Status = @Status
											ORDER BY p.[PublishDate] DESC
											OFFSET @Offset ROWS FETCH NEXT @MaxAmount ROWS ONLY",
					Connection = cn
				};

				cmd.Parameters.AddWithValue("@Offset", offset);
				cmd.Parameters.AddWithValue("@MaxAmount", maxAmount);
				cmd.Parameters.AddWithValue("@Status", status);

				cn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						posts.Add(PopulateFromDataReader(dr));
					}
				}
				return posts;
			}
		}

		public List<Post> GetPostsPaginationByStatusAndCategory(int offset, int maxAmount, string status, int categoryId)
		{
			var posts = new List<Post>();

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				var cmd = new SqlCommand
				{
					CommandType = CommandType.Text,
					CommandText = @"SELECT p.Id [PostId], p.[AuthorId], u.[UserName], p.[CreatedDate], p.[PublishDate], p.[ExpireDate], p.[Title], p.[Content], ps.Id [PostStatusId], ps.[Status]
										FROM Posts p
											INNER JOIN PostStatuses ps ON p.PostStatusId = ps.Id
											INNER JOIN CategoriesOnPosts conp ON p.Id = conp.PostId
											INNER JOIN AspNetUsers u ON p.AuthorId = u.Id
											WHERE ps.Status = @Status AND conp.CategoryId = @CategoryId
											ORDER BY p.[PublishDate] DESC
											OFFSET @Offset ROWS FETCH NEXT @MaxAmount ROWS ONLY",
					Connection = cn
				};

				cmd.Parameters.AddWithValue("@Offset", offset);
				cmd.Parameters.AddWithValue("@MaxAmount", maxAmount);
				cmd.Parameters.AddWithValue("@Status", status);
				cmd.Parameters.AddWithValue("@CategoryId", categoryId);

				cn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						posts.Add(PopulateFromDataReader(dr));
					}
				}
				return posts;
			}
		}

		public List<Post> GetPostsPaginationByStatusAndTag(int offset, int maxAmount, string status, int tagId)
		{
			var posts = new List<Post>();

			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				var cmd = new SqlCommand
				{
					CommandType = CommandType.Text,
					CommandText = @"SELECT p.Id [PostId], p.[AuthorId], u.[UserName], p.[CreatedDate], p.[PublishDate], p.[ExpireDate], p.[Title], p.[Content], ps.Id [PostStatusId], ps.[Status]
										FROM Posts p
											INNER JOIN PostStatuses ps ON p.PostStatusId = ps.Id
											INNER JOIN TagsOnPosts tonp ON p.Id = tonp.PostId
											INNER JOIN AspNetUsers u ON p.AuthorId = u.Id
											WHERE ps.Status = @Status AND tonp.TagId = @TagId
											ORDER BY p.[PublishDate] DESC
											OFFSET @Offset ROWS FETCH NEXT @MaxAmount ROWS ONLY",
					Connection = cn
				};

				cmd.Parameters.AddWithValue("@Offset", offset);
				cmd.Parameters.AddWithValue("@MaxAmount", maxAmount);
				cmd.Parameters.AddWithValue("@Status", status);
				cmd.Parameters.AddWithValue("@TagId", tagId);

				cn.Open();
				using (var dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						posts.Add(PopulateFromDataReader(dr));
					}
				}
				return posts;
			}
		}

		public Author GetAuthorByPost(int postId)
		{
			var author = new Author();
			var p = new DynamicParameters();
			p.Add("@postID", postId, DbType.Int32);
			using (var cn = new SqlConnection(Settings.ConnectionString))
			{
				author = cn.Query<Author>("SELECT AspNetUsers.Id, Email AS Name FROM AspNetUsers INNER JOIN Posts ON AspNetUsers.Id = Posts.AuthorId Where Posts.Id = @postID", p).FirstOrDefault();
			}
			return author;
		}

		public List<Post> Find(Expression<Func<Post, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Post PopulateFromDataReader(SqlDataReader dr)
		{
			var post = new Post();


			post.Id = (int) dr["PostId"];
			post.Author = new Author {Id = dr["AuthorId"].ToString(), Name = dr["UserName"].ToString()};
			post.Status = new PostStatus {Id = (int) dr["PostStatusId"], Status = dr["Status"].ToString()};
			post.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
			post.PublishDate = DateTime.Parse(dr["PublishDate"].ToString());
			post.Title = dr["Title"].ToString();
			post.Content = dr["Content"].ToString();


   //         var p = new Post
			//{
			//	Id = (int)dr["PostId"],
			//	Author = new Author { Id = dr["AuthorId"].ToString(), Name = dr["UserName"].ToString() },
			//	Status = new PostStatus { Id = (int)dr["PostStatusId"], Status = dr["Status"].ToString() },
			//	CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
			//	PublishDate = DateTime.Parse(dr["PublishDate"].ToString()),
			//	Title = dr["Title"].ToString(),
			//	Content = dr["Content"].ToString()
			//};

			if (dr["ExpireDate"] != DBNull.Value)
			{
				post.ExpireDate = DateTime.Parse(dr["ExpireDate"].ToString());
			}

			return post;
		}
	}
}
