using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CsvHelper;
using System.Globalization;

namespace SkinCancerDetectionAPI.Services
{
    public interface IExportService
    {
        byte[] GeneratePdf<T>(List<T> data, string title);
        byte[] GenerateCsv<T>(List<T> data);
    }

    public class ExportService : IExportService
    {
        public byte[] GeneratePdf<T>(List<T> data, string title)
        {
            if (data == null || !data.Any())
            {
                throw new ArgumentException("Data cannot be null or empty", nameof(data));
            }

            using var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add title
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK);
            var titleParagraph = new Paragraph(title, titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20f
            };
            document.Add(titleParagraph);

            // Create table
            var table = new PdfPTable(GetPropertyCount<T>())
            {
                WidthPercentage = 100,
                DefaultCell = { Padding = 5f }
            };

            // Add headers
            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
            foreach (var prop in GetPropertyNames<T>())
            {
                var cell = new PdfPCell(new Phrase(prop, headerFont))
                {
                    BackgroundColor = new BaseColor(51, 122, 183),
                    Padding = 5f
                };
                table.AddCell(cell);
            }

            // Add data rows
            var dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);
            foreach (var item in data)
            {
                foreach (var value in GetPropertyValues(item))
                {
                    table.AddCell(new Phrase(value?.ToString() ?? string.Empty, dataFont));
                }
            }

            document.Add(table);
            document.Close();
            return memoryStream.ToArray();
        }

        public byte[] GenerateCsv<T>(List<T> data)
        {
            if (data == null || !data.Any())
            {
                throw new ArgumentException("Data cannot be null or empty", nameof(data));
            }

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            // Write headers
            foreach (var prop in GetPropertyNames<T>())
            {
                csvWriter.WriteField(prop);
            }
            csvWriter.NextRecord();

            // Write data rows
            foreach (var item in data)
            {
                foreach (var value in GetPropertyValues(item))
                {
                    csvWriter.WriteField(value);
                }
                csvWriter.NextRecord();
            }

            streamWriter.Flush();
            return memoryStream.ToArray();
        }

        private static int GetPropertyCount<T>()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Length;
        }

        private static IEnumerable<string> GetPropertyNames<T>()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .Select(prop => prop.Name);
        }

        private static IEnumerable<object?> GetPropertyValues<T>(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .Select(prop => prop.GetValue(item, null));
        }
    }
}
