using System;
using System.Collections.Generic;
using ADODB;

namespace Music_App.Models
{
    public class DBGateway
    {
        public List<Artist> GetArtist()
        {
            List<Artist> artists = new List<Artist>();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\fisch\\source\\repos\\CIS414MVCFirstLesson\\Northwind.MDB;User Id=admin;Password=;";

            // Ado Objects
            Connection aConnection = new Connection();
            Command aCommand = new Command();
            ADODB.Recordset aRecordset = null;

            // This version uses the Execute method
            try
            {
                // Open the connection
                aConnection.Open(connectionString, "", "", 0);

                // Configure the command
                aCommand.ActiveConnection = aConnection;
                aCommand.CommandText = "select artistid, artistname, description from artists";

                aCommand.CommandType = CommandTypeEnum.adCmdText;

                object recordsAffected;
                aRecordset = aCommand.Execute(out recordsAffected, Type.Missing, -1);

                //Process Recordset and populate the list
                while (!aRecordset.EOF)
                {
                    Artist anArtist = new Artist();

                    anArtist.ArtistId = Convert.ToInt32(aRecordset.Fields["ArtistId"].Value);
                    anArtist.ArtistName = aRecordset.Fields["ArtistName"].Value.ToString();
                    anArtist.Description = aRecordset.Fields["Description"].Value.ToString();

                    artists.Add(anArtist);
                    aRecordset.MoveNext();

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            finally
            {
                //Clean up
                if (aRecordset != null && aRecordset.State == (int)ADODB.ObjectStateEnum.adStateOpen) aRecordset.Close();
                if (aConnection.State == (int)ADODB.ObjectStateEnum.adStateOpen) aConnection.Close();
            }

            return artists;
        }
    }
}
