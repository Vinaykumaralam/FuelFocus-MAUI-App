using System.Data;
using System.Net.Http.Json;
using System.Reflection;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Text;
using SkiaSharp;
using ClosedXML.Excel;
using FuelFocus.Model;
using CommunityToolkit.Maui.Storage;
using FuelFocus.View;
namespace FuelFocus.ViewModel;

public class VehicleReportViewModel : BindableObject
{
    //private string baseUrl = "http://10.0.2.2:5183/Trip";
    private string baseUrl = "http://10.0.2.2:5183/Trip";
    public List<VehicleReport> Data { get; set; }
    public ICommand PdfCommand { get; set; }
    public ICommand GoBackCommand { get; set; }
    private HttpClient _httpClient { get; set; }

    public VehicleReportViewModel()
    {
        _httpClient = new HttpClient();
        Data = new List<VehicleReport>()
        {
            new VehicleReport{Hours=1,Kilometer=50},
            new VehicleReport{Hours=2,Kilometer=46},
            new VehicleReport{Hours=3,Kilometer=63},
            new VehicleReport{Hours=4,Kilometer=53},
            //new VehicleReport{Hours=5,Kilometer=59},
        };
        PdfCommand = new Command<string>(OnMenuActionClicked);
        GoBackCommand = new Command(async () => await GoBack());
    }
    private async void OnMenuActionClicked(string action)
    {
        var T_Id = 0;
        if (Application.Current.Resources.TryGetValue("TripId", out var TripId))
        {
            T_Id = (int)TripId;
        }
        var records = await _httpClient.GetFromJsonAsync<List<TripDetail>>("http://10.0.2.2:5183/Trip/GetRecords");
        records = records.Where(x => x.TripId == T_Id).ToList();
        if (action.ToUpper() == "PDF")
        {
            if (records != null)
            {
                byte[] pdfbytes = DownloadPdf(records).Result;

                using (var stream = new MemoryStream(pdfbytes))
                {
                    var result = await FileSaver.SaveAsync("TripDetails.pdf", stream, CancellationToken.None);
                    if (result.IsSuccessful)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "PDF saved successfully", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Unable to download", "OK");

                    }
                }
            }
        }
        else
        if (action.ToUpper() == "EXCEL")
        {
            byte[] ExcelBytes = DownloadExcel(records).Result;

            using (var stream = new MemoryStream(ExcelBytes))
            {
                var result = await FileSaver.SaveAsync("TripDetails.xlsx", stream, CancellationToken.None);
                if (result.IsSuccessful)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Excel sheet saved successfully", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to download", "OK");

                }
            }
        }
        else if (action.ToUpper() == "WORD")
        {
            byte[] WordBytes = DownloadWord(records).Result;

            using (var stream = new MemoryStream(WordBytes))
            {
                var result = await FileSaver.SaveAsync("TripDetails.doc", stream, CancellationToken.None);
                if (result.IsSuccessful)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Word document saved successfully", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to download", "OK");

                }
            }
        }
    }

    public async Task<byte[]> DownloadPdf(List<TripDetail> records)
    {
        try
        {
            DataTable dt = ConvertListToDT(records);
            //using (MemoryStream pdfs = new MemoryStream())
            //{
            //    var documentWidth = 595;
            //    var documentHeight = 842;
            //    using (var pdfdoc = SKDocument.CreatePdf(pdfs))
            //    {
            //        var canvas = pdfdoc.BeginPage(documentWidth, documentHeight);
            //        var paint = new SKPaint
            //        {
            //            Color = SKColors.Black,
            //            TextSize = 12,
            //            IsAntialias = true

            //        };
            //        var borderPaint = new SKPaint
            //        {
            //            Color = SKColors.Black,
            //            Style = SKPaintStyle.Stroke,
            //            StrokeWidth = 1
            //        };
            //        float startX = 10;
            //        float startY = 10;
            //        float cellPadding = 5;
            //        float cellHeight = 20;
            //        float[] columnWidths = new float[dt.Columns.Count];

            //        using (var measurePaint = new SKPaint { TextSize = 12 })
            //        {
            //            for (int i = 0; i < dt.Columns.Count; i++)
            //            {
            //                string headerText = dt.Columns[i].ColumnName;
            //                float headerWidth = measurePaint.MeasureText(headerText);
            //                columnWidths[i] = headerWidth;

            //                foreach (DataRow row in dt.Rows)
            //                {
            //                    string cellText = row[i].ToString();
            //                    float cellWidth = measurePaint.MeasureText(cellText);
            //                    if (cellWidth > columnWidths[i])
            //                    {
            //                        columnWidths[i] = cellWidth;
            //                    }
            //                }

            //                columnWidths[i] += 2 * cellPadding;
            //            }
            //        }

            //        float x = startX;
            //        float y = startY;

            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            string headerText = dt.Columns[i].ColumnName;
            //            canvas.DrawRect(x, y, columnWidths[i], cellHeight, borderPaint);
            //            canvas.DrawText(headerText, x + cellPadding, y + cellHeight - cellPadding, paint);
            //            x += columnWidths[i];
            //        }

            //        x = startX;
            //        y += cellHeight;

            //        foreach (DataRow row in dt.Rows)
            //        {
            //            for (int i = 0; i < dt.Columns.Count; i++)
            //            {
            //                string cellText = row[i].ToString();
            //                canvas.DrawRect(x, y, columnWidths[i], cellHeight, borderPaint);
            //                canvas.DrawText(cellText, x + cellPadding, y + cellHeight - cellPadding, paint);
            //                x += columnWidths[i];
            //            }
            //            x = startX;
            //            y += cellHeight;
            //        }

            //        // End the page
            //        pdfdoc.EndPage();
            //    }
            //    return pdfs.ToArray();
            //}
            using (var pdfStream = new MemoryStream())
            {

                var documentWidth = 595;
                var documentHeight = 842;


                using (var pdfDocument = SKDocument.CreatePdf(pdfStream))
                {

                    var canvas = pdfDocument.BeginPage(documentWidth, documentHeight);


                    var textPaint = new SKPaint
                    {
                        Color = SKColors.Black,
                        TextSize = 12,
                        IsAntialias = true
                    };

                    var borderPaint = new SKPaint
                    {
                        Color = SKColors.Black,
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = 1
                    };
                    var titlePaint = new SKPaint
                    {
                        Color = SKColors.Black,
                        TextSize = 24,
                        IsAntialias = true,
                        TextAlign = SKTextAlign.Center
                    };

                    float startX = 10;
                    float startY = 90;
                    float cellPadding = 5;
                    float cellHeight = 20;
                    float columnWidth = 200;

                    float titleX = documentWidth / 2;
                    float titleY = 50;
                    canvas.DrawText("TRIP REPORT", titleX, titleY, titlePaint);
                    float x = startX;
                    float y = startY;


                    canvas.DrawRect(x, y, columnWidth, cellHeight, borderPaint);
                    canvas.DrawText("Field", x + cellPadding, y + cellHeight - cellPadding, textPaint);
                    x += columnWidth;
                    canvas.DrawRect(x, y, columnWidth, cellHeight, borderPaint);
                    canvas.DrawText("Value", x + cellPadding, y + cellHeight - cellPadding, textPaint);

                    x = startX;
                    y += cellHeight;


                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {

                            canvas.DrawRect(x, y, columnWidth, cellHeight, borderPaint);
                            canvas.DrawText(column.ColumnName, x + cellPadding, y + cellHeight - cellPadding, textPaint);

                            x += columnWidth;


                            string cellText = row[column].ToString();
                            canvas.DrawRect(x, y, columnWidth, cellHeight, borderPaint);
                            canvas.DrawText(cellText, x + cellPadding, y + cellHeight - cellPadding, textPaint);

                            x = startX;
                            y += cellHeight;
                        }
                    }


                    pdfDocument.EndPage();

                }
                return pdfStream.ToArray();
            }
        }

        catch (Exception ex) { throw; }
    }

    public async Task<byte[]> DownloadExcel(List<TripDetail> records)
    {
        try
        {
            DataTable dt = ConvertListToDT(records);
            using (XLWorkbook wb = new XLWorkbook())
            {
                var worksheet = wb.Worksheets.Add("Sheet1");
                var titlecell = worksheet.Cell(1, 1);
                titlecell.Value = "TRIP REPORT";
                var titleRange = worksheet.Range(1, 1, 1, dt.Columns.Count);
                titleRange.Merge();
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.FontSize = 16;
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(3, 1).InsertTable(dt);
                worksheet.Columns().AdjustToContents();

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return ms.ToArray();
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<byte[]> DownloadWord(List<TripDetail> records)
    {
        try
        {
            DataTable dt = ConvertListToDT(records);

            if (dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
                sb.Append("<tr><td>");
                sb.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class=\"Header\" width=\"120\" style=\"border: 1px solid gray; text-align:left; font-family:Verdana; font-size:12px; font-weight:bold;\">" + dt.Columns[j].ColumnName.ToString().Replace(".", "<br>") + "</td>");
                        sb.Append("<td class=\"Content\" style=\"border: 1px solid gray; text-align:left;\">" + dt.Rows[i][j].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>");
                sb.Append("</td></tr></table>");
                var s = sb.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(s);
                return byteArray;

            }

            else { return null; }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //public async Task<List<TripDetail>> GetRecords()
    //{
    //    try
    //    {
    //        var httpclient = new HttpClient();
    //        var tripDetails = await httpclient.GetAsync("http://10.0.2.2:5135/Trip/GetRecords"); 
    //        if (tripDetails.IsSuccessStatusCode)
    //        {
    //            var content = await tripDetails.Content.ReadAsStringAsync();
    //            List<TripDetail> records = JsonConvert.DeserializeObject<List<TripDetail>>(content);
    //            return records;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    public static DataTable ConvertListToDT(List<TripDetail> records)
    {
        try
        {
            DataTable dataTable = new DataTable(typeof(TripDetail).Name);

            PropertyInfo[] Props = typeof(TripDetail).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in Props)
            {
                dataTable.Columns.Add(prop.Name, prop.PropertyType);
            }
            foreach (var item in records)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        catch (Exception ex) { throw; }
    }
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync($"{nameof(RecentTripsPage)}");
    }
}
