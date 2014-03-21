using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            var bookTable = new DataTable("Books");
            var idColumn = new DataColumn("id", typeof (int))
                {AutoIncrement = true, AutoIncrementSeed = -1, AutoIncrementStep = -1};

            bookTable.Columns.Add(idColumn);
            bookTable.Columns.Add(new DataColumn("ISBN", typeof (string)));
            bookTable.Columns.Add(new DataColumn("Title", typeof (string)));
            bookTable.Columns.Add(new DataColumn("Author", typeof (string)));
            bookTable.PrimaryKey = new [] {idColumn};

            const string fileName = "EmptyBookTable.txt";
            bookTable.WriteXml(fileName);

            bookTable.WriteXml("EmptyBookTableDiffGram.txt", XmlWriteMode.DiffGram);

            var dataRow = bookTable.NewRow();
            dataRow["ISBN"] = "12345";
            dataRow["Title"] = "Early Warnings";
            dataRow["Author"] = "Ray Dar";
            bookTable.Rows.Add(dataRow);

            bookTable.WriteXml("booksOneRow.txt");
            bookTable.WriteXml("booksOneRowDiffGram.txt", XmlWriteMode.DiffGram);
            bookTable.WriteXml("booksOneRowWithSchema.txt", XmlWriteMode.WriteSchema);

            bookTable.RemotingFormat = SerializationFormat.Binary;
            bookTable.WriteXml("booksOneRowBinary.txt"); // Still writes XML text

            var formatter = new BinaryFormatter();
            var filename = "booksOneRowBinarySerialized.txt";
            var serializationStream = File.Create(filename);
            formatter.Serialize(serializationStream, bookTable);
            serializationStream.Close();
            var bookTableDeserialized = (DataTable)formatter.Deserialize(File.Open(filename, FileMode.Open));

            Print(bookTableDeserialized);


        }

        private void Print(DataTable dataTable)
        {
            //foreach (var row in dataTable.Rows)
            for (int rowNumber = 0; rowNumber < dataTable.Rows.Count; rowNumber++)
            {
                for (var columnNumber = 0; columnNumber < dataTable.Columns.Count; columnNumber++)

                //foreach (var column in dataTable.Columns)
                {
                    //row[1]
                    Console.Write("{0} ", dataTable.Rows[rowNumber][columnNumber]);
                }
                Console.WriteLine();
            }
        }
    }
}
