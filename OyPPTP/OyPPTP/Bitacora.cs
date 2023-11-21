using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
/// 
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace OyPPTP
{
    public class Footer : PdfPageEventHelper
    {

        PdfContentByte cb;

        //Me armo un templata de lo que voy a poner en el pie de pagina
        //La idea es escribir pagina por pagina este template
        //Eecien cuando cierro el documento agrego el dato
        //del recuento total ed paginas
        PdfTemplate footerTemplate;

        PdfTemplate headerTemplate;

        //Esta es la fuenta que voy a usar para el encabezado y pie de pagina
        BaseFont bf = null;

        private string tipoReporte;
        private string fechaImpresion;
        private string horaImpresion;
        private string fechaDesde;
        private string fechaHasta;
        private string usuario;
        private string evento;

        public Footer(string tipoReporte, string fechaImpresion, string horaImpresion, string fechaDesde, string fechaHasta, string usuario, string evento) {
            this.tipoReporte = tipoReporte;
            this.fechaImpresion = fechaImpresion;
            this.horaImpresion = horaImpresion;
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.usuario = usuario;
            this.evento = evento;
        }
        
        //Este metodo se va a ejecutar siempre que abra el documento para escribir en el
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                footerTemplate = cb.CreateTemplate(50, 50);
                headerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
                return;
            }
            catch (System.IO.IOException ioe)
            {
                return;
            }
        }

        //Esto se ejecuta cada vez que se termina de escribir una pagina
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            //Invoco al OnEndPage de la clase padre
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            PdfPTable pdfTab = new PdfPTable(3);

            string footer = "Página " + writer.PageNumber + " de ";
            string header1 = "Tipo de reporte: " + this.tipoReporte + " \n" +
                            "Fecha de impresión: " + this.fechaImpresion;
            string header2 = "Fecha desde: " + this.fechaDesde + " \n" +
                            "Fecha hasta: " + this.fechaHasta;
            string header3 = "Usuario: " + this.usuario + " \n" +
                            "Evento: " + this.evento;
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));
                cb.ShowText(footer);
                cb.SetTextMatrix(document.PageSize.GetLeft(10), document.PageSize.GetTop(20));
                cb.ShowText(header1);
                cb.SetTextMatrix(document.PageSize.GetLeft(10), document.PageSize.GetTop(35));
                cb.ShowText(header2);
                cb.SetTextMatrix(document.PageSize.GetLeft(10), document.PageSize.GetTop(50));
                cb.ShowText(header3);
                cb.EndText();
                float lenFooter = bf.GetWidthPoint(footer, 12);
                float lenHeader1 = bf.GetWidthPoint(header1, 12);
                float lenHeader2 = bf.GetWidthPoint(header2, 12);
                float lenHeader3 = bf.GetWidthPoint(header3, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + lenFooter, document.PageSize.GetBottom(30));
                cb.AddTemplate(headerTemplate, document.PageSize.GetLeft(10) + lenHeader1, document.PageSize.GetTop(30));
                cb.AddTemplate(headerTemplate, document.PageSize.GetLeft(10) + lenHeader2, document.PageSize.GetTop(30));
                cb.AddTemplate(headerTemplate, document.PageSize.GetLeft(10) + lenHeader3, document.PageSize.GetTop(30));
            }

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;

        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber).ToString());
            footerTemplate.EndText();
        }
    }

    public partial class Bitacora : Form
    {
        private string tipoReporte;
        private string fechaImpresion;
        private string horaImpresion;
        private string fechaDesde;
        private string fechaHasta;
        private string usuario;
        private string evento;
        private List<(string, string, string, int, DateTime, string, string, string, string, string)> registrosBitacora;

        /////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////    CONSTRUCTOR     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        public Bitacora(string tipoReporte, string fechaImpresion, string horaImpresion, string fechaDesde, string fechaHasta, string usuario, string evento, List<(string, string, string, int, DateTime, string, string, string, string, string)> registrosBitacora)
        {
            this.tipoReporte = tipoReporte;
            this.fechaImpresion = fechaImpresion;
            this.horaImpresion = horaImpresion;
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            if (this.usuario != "-1")
            {
                this.usuario = usuario;
            }
            else {
                this.usuario = "Sin filtro";
            }
            if (this.evento!= "-1")
            {
                this.evento = evento;
            }
            else
            {
                this.evento = "Sin filtro";
            }
            this.registrosBitacora = registrosBitacora;
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////    FORM LOAD     ///////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void Bitacora_Load(object sender, EventArgs e)
        {
            this.grilla_bitacora.Columns.Add("usuario", "Usuario responsable");
            this.grilla_bitacora.Columns.Add("evento", "Email usuario responsable");
            this.grilla_bitacora.Columns.Add("evento", "Evento");
            this.grilla_bitacora.Columns.Add("criticidad", "Criticidad");
            this.grilla_bitacora.Columns.Add("hora", "Fecha y hora");
            this.grilla_bitacora.Columns.Add("usuarioAfectado", "Usuario afectado");
            this.grilla_bitacora.Columns.Add("tablaAfectada", "Tabla afectada");
            this.grilla_bitacora.Columns.Add("grupoAfectado", "Grupo afectado");
            this.grilla_bitacora.Columns.Add("patenteAfectada", "Patente afectada");


            foreach ((string, string, string, int, DateTime, string, string, string, string, string) registro in this.registrosBitacora) {
                this.grilla_bitacora.Rows.Add(
                    registro.Item1,
                    registro.Item2,
                    registro.Item3,
                    registro.Item4,
                    registro.Item5,
                    registro.Item6,
                    registro.Item8,
                    registro.Item9,
                    registro.Item10
                );
            }
            
            this.grilla_bitacora.AutoResizeColumns();
            this.grilla_bitacora.ReadOnly = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////    SUBFORMS CREATION     ///////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////

        private void cerrar_bitacora_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cambiar_filtros_Click(object sender, EventArgs e)
        {
            PreBitacora preBitacora = new PreBitacora();
            preBitacora.Show();
        }

        private void imprimir_Click(object sender, EventArgs e)
        {
            if (this.grilla_bitacora.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Error de escritura en disco: " + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(grilla_bitacora.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in grilla_bitacora.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in grilla_bitacora.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 60f, 60f);
                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                                writer.PageEvent = new Footer(
                                    this.tipoReporte, 
                                    this.fechaImpresion, 
                                    this.horaImpresion, 
                                    this.fechaDesde, 
                                    this.fechaHasta, 
                                    this.usuario, 
                                    this.evento
                                );
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Éxito al imprimir el reporte!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay registros para imprimir.", "Info");
            }
        }
    }
}
