using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Security.Cryptography.X509Certificates;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.X509;

namespace OneHub360.CMS.DAL.DigitalSignature
{
    public class FileSignManager
    {
        //public static bool SignPdfDocument(Document document, X509Certificate2 certificate,string senderName,DateTime actionDate)
        //{

            //var pdfObj = new PDFSign("9203a079aee7c53ed176");

            
            //    //Digital certificate

            //    pdfObj.DigitalSignatureCertificate = certificate;

            //    if (pdfObj.DigitalSignatureCertificate == null)
            //        throw new Exception("Digital signature certificate is not valid.");


            //    //Signature Reason
            //    pdfObj.SigningReason = "Signed by OneHub360 System";


            //    //Certify Document
            //    pdfObj.CertifySignature = CertifyMethod.AnnotationsAndFormFilling;


            ////Signature Hash Algorithm
            ////pdfObj.HashAlgorithm = DigestAlgorithm.SHA1;

                

            //    //#region Visible Signature

            //    //if (signatureImage != null)
            //    //{

            //    //    pdfObj.VisibleSignature = true;

            //    //    pdfObj.SignaturePage = 1;
            //    //    pdfObj.OldStyleAdobeSignature = false;

            //    //    pdfObj.SignatureImage = signatureImage;

            //    //    pdfObj.FontSize = 10;
            //    //    pdfObj.SignatureImagePosition = SignatureImageType.ImageWithNoText;
            //    //}

            //    //#endregion

            //    //pdfObj.SmartCardPIN = userPin;

                

            //    pdfObj.LoadPDFDocument(document.FileBinary);

            //    pdfObj.VisibleSignature = false;

            //document.FileBinary = pdfObj.ApplyDigitalSignature();

            //document.Signed = true;
            //document.SignedBy = senderName;
            //document.SigningDate = actionDate;

          //  return true;
            
//        }

        public static bool SignPdfDocument(Document document, X509Certificate2 certificate, string senderName, DateTime actionDate)
        {
            PdfReader reader = new PdfReader(document.FileBinary);

            MemoryStream output = new MemoryStream();

            PdfStamper stamper = PdfStamper.CreateSignature(reader, output, '\0');
            

            PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
            signatureAppearance.Reason = "Signed by CPA Correspondence Management System";
            signatureAppearance.LocationCaption = "State of Kuwait";
            
            

            X509CertificateParser cp = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = { cp.ReadCertificate(certificate.RawData) };

            IExternalSignature externalSignature = new X509Certificate2Signature(certificate, "SHA-1");

            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

            stamper.Close();

            document.FileBinary = output.ToArray();

            return true;

        }

        

        public static bool SignWordDocument(string filepath,string comment,string imagepath, X509Certificate2 certificate)
        {
            ImagePart imagePart;

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filepath, true))
            {

                // Assign a reference to the existing document body.
                Body body = wordDoc.MainDocumentPart.Document.Body;

                // Add new text.
                Paragraph para = body.AppendChild(new Paragraph());
                var properties = para.AppendChild(new ParagraphProperties());
                properties.Append(new Justification() { Val = JustificationValues.Right });
                Run run = para.AppendChild(new Run());
                run.AppendChild(new RunProperties { RightToLeftText = new RightToLeftText() });
                run.AppendChild(new Text(comment));

                MainDocumentPart mainPart = wordDoc.MainDocumentPart;

                imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

                using (FileStream stream = new FileStream(imagepath, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }

                AddImageToBody(wordDoc, mainPart.GetIdOfPart(imagePart));


                wordDoc.Close();
            }

            // Open the package.
            Package package = Package.Open(filepath);

            // Create the PackageDigitalSignatureManager
            PackageDigitalSignatureManager dsm =
              new PackageDigitalSignatureManager(package);

            //Specify that the certificate is embedded in the signature held
            //in the XML Signature part.

            //Certificate embedding options include:
            // InSignaturePart – Certificate is embedded in the signature.
            // InCertificatePart – Certificate is embedded in a 
            //                     separate certificate part

            dsm.CertificateOption =
                CertificateEmbeddingOption.InSignaturePart;

            

            //var signingPart = package.CreatePart(new Uri("/Content/OneHub360/" + Guid.NewGuid() + ".xml", UriKind.Relative), System.Net.Mime.MediaTypeNames.Text.RichText);

            
            


            //Initialize a list to hold the part URIs to sign.

            List<Uri> partsToSign =
                new List<Uri>();

            partsToSign.Add(imagePart.Uri);

            //Sign package using components created above

            

            PackageDigitalSignature signature = dsm.Sign(partsToSign,
                 certificate);

            //After signing, close the package.
            //The signature will be persisted in the package.
            package.Close();



            return true;
        }

        private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });




            var para =  wordDoc.MainDocumentPart.Document.Body.AppendChild(
    new Paragraph(new Run(element))
    {
        ParagraphProperties = new ParagraphProperties()
        {
            Justification = new Justification()
            {
                Val = JustificationValues.Right
            }
        }
    });

             
            

            
        }

        
    }
}
