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
        public async Task GenerateReportAsync()
        {
            var books = _manager.Books;
            var stat = new Statistics();
            await Task.Run(async () =>
            {
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

                            column.Item().Text($"Current users count: {_manager.Visitors.Count}");
                            column.Item().Text($"New users this month: {stat.NewVisitorsMonth}");
                            column.Item().Text($"Books borrowed this month: {stat.NewTransactionsMonth}");
                            
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
            });
        }
    }
}
