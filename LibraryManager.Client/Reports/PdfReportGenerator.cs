using LibraryManager.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LibraryManager.Client.Reports
{
    public class PdfReportGenerator
    {
        Manager _manager = ManagerInstance.Instance;
        public PdfReportGenerator() 
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
        public void GenerateReport()
        {
            var books = _manager.Books;
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Header()
                        .Text("Library report")
                        .SemiBold().FontSize(36);

                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text("Books in library").SemiBold().AlignCenter();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            for (int i = 0; i < _manager.Books.Count; i++)
                            {
                                table.Cell().Text(books[i].Id);
                                table.Cell().Text(books[i].Name);
                                table.Cell().Text(books[i].BookAuthor.FullName);
                                table.Cell().Text(books[i].Year);
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
                .GeneratePdfAndShow();
        }
    }
}
