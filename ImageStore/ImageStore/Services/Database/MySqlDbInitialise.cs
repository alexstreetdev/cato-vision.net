using System.Data;
using MySql.Data.MySqlClient;

namespace ImageStore.Services.Database
{
    public class MySqlDbInitialise
    {
        private readonly MySqlDbService _dbSvc;

        public MySqlDbInitialise(MySqlDbService svc)
        {
            _dbSvc = svc;
        }

        public void Initialise()
        {
            CreateTables();
            CreateProcedures();
        }

        private void CreateTables()
        {
            CreateImageTable();
            CreateContentTable();
        }

        private void CreateImageTable()
        {
            var cs = @"CREATE TABLE IF NOT EXISTS Image " +
                    @"(ImageId VARCHAR(100) not null primary key UNIQUE," +
                    //@"Filename VARCHAR(50) null," +
                    @"EventTime DATETIME(0) not null," +
                    @"CorrelationId VARCHAR(50) not null," +
                    @"SequenceNumber INT null," +
                    @"ImageUrl VARCHAR(255) null," +
                    @"Source VARCHAR(50) null," +
                    @"CreatedOn DATETIME(6) default CURRENT_TIMESTAMP(6) not null," +
                    @"ExpiryOn DATETIME(6) null," +
                    @"DeletedOn DATETIME(6) null," +
                    @"INDEX ix_Image_CorrelationId (CorrelationId));";

            var cmd1 = new MySqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = cs
            };

            _dbSvc.ExecNonQuery(cmd1);
        }

        private void CreateContentTable()
        {
            var t = @"CREATE TABLE IF NOT EXISTS Content " +
                    @"(ContentId BIGINT(20) unsigned not null auto_increment primary key," +
                    @"ImageId VARCHAR(100) not null," +
                    @"X INT not null, Y INT not null," +
                    @"Width INT not null, Height INT not null," +
                    @"ContentDescription VARCHAR(255) not null," +
                    @"ContentData VARCHAR(255)," +
                    @"Source VARCHAR(100)," +
                    @"CreatedOn DATETIME(6) default CURRENT_TIMESTAMP(6) not null," +
                    @"INDEX ix_Content_ImageId (ImageId)," +
                    @"CONSTRAINT fk_ImageId FOREIGN KEY (ImageId) REFERENCES Image(ImageId) ON DELETE CASCADE);";

            var cmd1 = new MySqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = t
            };

            _dbSvc.ExecNonQuery(cmd1);
        }

        private void CreateProcedures()
        {
            CreateInsertImageProc();
            CreateInsertContentProc();
        }

        private void CreateInsertImageProc()
        {
            const string c = "CREATE OR REPLACE PROCEDURE prInsImage \n(\n" +
                             "IN ImageId VARCHAR(100),\n" +
                             "IN EventTime DATETIME(0),\n" +
                             "IN ImageUrl VARCHAR(255),\n" +
                             "IN CorrelationId VARCHAR(50),\n" +
                             "IN SequenceNumber INT,\n" +
                             "IN Source VARCHAR(50),\n" +
                             "IN ExpiryOn DATETIME(0) \n)\n" +
                             "BEGIN\n INSERT INTO Image\n (ImageId,EventTime,ImageUrl,CorrelationId,SequenceNumber,Source,ExpiryOn)\n" +
                             " VALUES(ImageId,EventTime,ImageUrl,CorrelationId,SequenceNumber,Source,ExpiryOn);\n" +
                             "END;";
            var cmd1 = new MySqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = c
            };
            _dbSvc.ExecNonQuery(cmd1);
        }

        private void CreateInsertContentProc()
        {
            const string c = "CREATE OR REPLACE PROCEDURE prInsContent \n(\n" +
                             "IN ImageId VARCHAR(100),\n" +
                             "IN X INT, IN Y INT,\n" +
                             "IN Width INT, IN Height INT,\n" +
                             "IN ContentDescription VARCHAR(255),\n" +
                             "IN ContentData VARCHAR(255),\n" +
                             "IN Source VARCHAR(100),\n" +
                             "OUT NEWID BIGINT(20)\n)\n" +
                             " BEGIN\n INSERT INTO Content\n (ImageId,X,Y,Width,Height,ContentDescription,ContentData,Source)\n" +
                             " VALUES(ImageId,X,Y,Width,Height,ContentDescription,ContentData,Source);\n" +
                             " SET NEWID = LAST_INSERT_ID();\n" +
                             "END;";
            var cmd1 = new MySqlCommand()
            {
                CommandType = CommandType.Text,
                CommandText = c
            };
            _dbSvc.ExecNonQuery(cmd1);
        }
    }
}