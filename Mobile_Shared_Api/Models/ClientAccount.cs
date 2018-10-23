using Mobile_Shared_Api.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mobile_Shared_Api.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ClientAccount
    {
        private List<UserProfile> _profiles;

        [JsonProperty("clientId")]
        public Guid Id { get; set; }

        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        [JsonProperty("userProfiles")]
        public List<UserProfile> UserProfiles
        {
            get { return _profiles = (_profiles ?? new List<UserProfile>()); }
            set { _profiles = value; }
        }

        public ClientAccount CreateAccount(Guid id, string clientName)
        {
            return new ClientAccount
            {
                Id = id,
                ClientName = clientName
            };
        }

        public void UpdateAccount(ClientAccount account)
        {
            Id = account.Id;
            ClientName = account.ClientName;
        }

        public static void SaveClientAccounts(List<ClientAccount> items)
        {
            using (var tbl = new DataTable())
            {
                tbl.Columns.Add(new DataColumn("Id", typeof(Guid)));
                tbl.Columns.Add(new DataColumn("ClientName", typeof(string)));

                foreach (var i in items)
                {
                    var row = tbl.NewRow();
                    row["Id"] = i.Id;
                    row["ClientName"] = i.ClientName;
                    tbl.Rows.Add(row);
                }

                var dataLayer = DataAccessFactory.GetDataLayer(Settings.LocalDbConnectionStringSettings);
                var parameters = new List<IDbDataParameter> { dataLayer.BuildParameter("Items", tbl, "dbo.ClientAccountParam") };

                var result = dataLayer.ExecuteNonQuery("dbo.SaveClientAccounts", parameters);
            }
        }

        public static List<ClientAccount> GetAllClientAccounts()
        {
            var dataLayer = DataAccessFactory.GetDataLayer(Settings.LocalDbConnectionStringSettings);

            return dataLayer.GetTypedList("dbo.GetAllClients", LoadFromDataReader);
        }

        public static ClientAccount LoadFromDataReader(IDataRecord reader)
        {
            return new ClientAccount
            {
                Id = (Guid)reader["Id"],
                ClientName = reader["ClientName"].ToString()
            };
        }
    }
}