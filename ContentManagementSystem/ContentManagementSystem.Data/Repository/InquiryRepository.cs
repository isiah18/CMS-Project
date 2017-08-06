using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Data.Contracts;
using ContentManagementSystem.Model;
using Dapper;

namespace ContentManagementSystem.Data.Repository
{
    public class InquiryRepository : IInquiryRepository
    {
        public Inquiry Get(int id)
        {
            Inquiry inquiry;
            var i = new DynamicParameters();
            i.Add("@InquiryId", id, DbType.Int32);
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                inquiry = cn.Query<Inquiry>("SELECT * FROM Inquiries WHERE Id = @InquiryId", i).First();
            }

            return inquiry;
        }
    

    public void Add(Inquiry inquiry)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var i = new DynamicParameters();
                i.Add("InquiryStatusId", inquiry.InquiryStatusId);
                i.Add("Name", inquiry.Name);
                i.Add("Phone", inquiry.Phone);
                i.Add("Email", inquiry.Email);
                i.Add("Message", inquiry.Message);

                cn.Execute("AddInquiries",i, commandType: CommandType.StoredProcedure);
            }

        }

        public void Delete(int inquiryId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var i = new DynamicParameters();
                i.Add("@InquiryId", inquiryId);
                cn.Execute("DeleteInquiry", i, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateStatus(Inquiry inquiry)
        {
            var u = new DynamicParameters();
            u.Add("@Id", inquiry.Id);
            u.Add("@InquiryStatusId", inquiry.InquiryStatusId);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute("UPDATE Inquiries SET InquiryStatusId = @InquiryStatusId WHERE Id = @Id", u);
            }
        }

        public void MarkAsUnread(Inquiry inquiry)
        {
            var m = new DynamicParameters();
            m.Add("@Id", inquiry.Id);
            m.Add("@InquiryStatusId", inquiry.InquiryStatusId);
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Execute("UPDATE Inquiries SET InquiryStatusId = @InquiryStatusId WHERE Id = @Id", m);
            }
        }

        public List<Inquiry> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                return cn.Query<Inquiry>("GetAllInquiries", commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}