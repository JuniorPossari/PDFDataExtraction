using iTextSharp.text.pdf;

namespace PDFDataExtraction
{
    public static class PDFDataExtraction
    {
        /// <summary>
        /// Extract text from all pages of the PDF file
        /// </summary>
        /// <param name="pdfBytes">Bytes of the PDF file</param>
        /// <returns>Returns a list with the full text of all pages</returns>
        public static List<string> ExtractPagesText(byte[] pdfBytes)
        {
            var pagesText = new List<string>();

            try
            {
                var reader = new PdfReader(pdfBytes);

                for (var pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
                {
                    var contentBytes = reader.GetPageContent(pageNum);
                    var tokenizer = new PrTokeniser(new RandomAccessFileOrArray(contentBytes));

                    var stringsList = new List<string>();

                    while (tokenizer.NextToken())
                    {
                        if (tokenizer.TokenType == PrTokeniser.TK_STRING) stringsList.Add(tokenizer.StringValue);
                    }

                    var pageText = string.Join("\r\n", stringsList);

                    pagesText.Add(pageText);
                }

                reader.Close();
            }
            catch (Exception)
            {
                pagesText = new List<string>();
            }

            return pagesText;
        }
    }
}