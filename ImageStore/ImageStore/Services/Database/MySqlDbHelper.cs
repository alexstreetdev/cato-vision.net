using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using VisionCommon.Models;

namespace ImageStore.Services.Database
{
    public static class MySqlDbHelper
    {


        public static string AddImageQuery(Image img)
        {
            var sb = new StringBuilder("INSERT INTO Image");
            sb.Append(" (ImageId,EventTime,CorrelationId,SequenceNumber,Source,CreatedOn)");
            sb.Append($" VALUES('{img.ImageId}','{img.EventTime:yyyy-MM-dd}','{img.CorrelationId}'");
            sb.Append($",{img.SequenceNumber},'{img.Source}','{img.CreatedOn:yyyy-MM-dd hh-mm-ss.ffffff}')");
            return sb.ToString();
        }

        public static MySqlCommand AddImageCommand(Image img)
        {
            var cmd = new MySqlCommand("prInsImage");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ImageId", img.ImageId);
            cmd.Parameters["ImageId"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("EventTime", img.EventTime);
            cmd.Parameters["EventTime"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("ImageUrl", img.ImageUrl);
            cmd.Parameters["ImageUrl"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("CorrelationId", img.CorrelationId);
            cmd.Parameters["CorrelationId"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("SequenceNumber", img.SequenceNumber);
            cmd.Parameters["SequenceNumber"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("Source", img.Source);
            cmd.Parameters["Source"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("ExpiryOn", img.ExpiryOn);
            cmd.Parameters["ExpiryOn"].Direction = ParameterDirection.Input;

            return cmd;
        }

        public static MySqlCommand AddImageContentCommand(ImageContent content)
        {
            var cmd = new MySqlCommand("prInsContent");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ImageId", content.ImageId);
            cmd.Parameters["ImageId"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("X", content.X);
            cmd.Parameters["X"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("Y", content.Y);
            cmd.Parameters["Y"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("Width", content.Width);
            cmd.Parameters["Width"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("Height", content.Height);
            cmd.Parameters["Height"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("ContentDescription", content.ContentDescription);
            cmd.Parameters["ContentDescription"].Direction = ParameterDirection.Input;
            cmd.Parameters.Add("NEWID", MySqlDbType.Int64);
            cmd.Parameters["NEWID"].Direction = ParameterDirection.Output;

            return cmd;
        }

        public static MySqlCommand DeleteImageCommand(string imageId)
        {
            var cmd = new MySqlCommand("prDelImage");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", imageId);
            cmd.Parameters["Id"].Direction = ParameterDirection.Input;

            return cmd;
        }

    }
}
