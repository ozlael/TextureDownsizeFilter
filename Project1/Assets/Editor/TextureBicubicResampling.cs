using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
//using UnityEngine.Windows; // https://docs.unity3d.com/ScriptReference/Windows.File.html

class TextureBicubicResampling : AssetPostprocessor
{
    int orgw, orgh;
    bool filterFlag = false;
    System.Drawing.Image srcImg;


    void OnPreprocessTexture()
    {
        var importer = (assetImporter as TextureImporter);

        if (assetPath.EndsWith("_bicubic.png"))
        {
            TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
            importer.ReadTextureSettings(textureImporterSettings);
            importer.SetTextureSettings(textureImporterSettings);   // () = TextureImporterFormat.RGBA32;
            string filePath = importer.assetPath;
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);	// initial dimensions are meaningless.
            tex.LoadImage(fileData);

            orgw = tex.width;
            orgh = tex.height;

            if (orgw != orgh) {
                Debug.Log("Can handle Square only");
                return;
            }

            // Problem is that Mono on MAC does not support System.Drawing. 
            // It works only for Windows at this moment.
            // Check : http://www.mono-project.com/docs/advanced/pinvoke/dllnotfoundexception/
            // Anyway...
            srcImg = Texture2Image(tex);

            filterFlag = true;
        }
    }
    

    void OnPostprocessTexture(Texture2D texture)
    {
        if (!filterFlag){
            return;
        }
        
        int texw = texture.width;
        int texh = texture.height;

        if (texw != orgw && texh != orgw) {
            Debug.Log("Do filterling");
        } else {
            return;
        }

        Bitmap bmp = (Bitmap)srcImg;
        var destImage = ResizeBitmap(bmp, texw, texh);

        CopyToTexture(ref texture, ref destImage);
    }


    public static bool CopyToTexture( ref Texture2D texture, ref Bitmap bmp)
    {
        if (texture.width != bmp.Width || texture.height != bmp.Height)
        {
            Debug.Log("You fucked up : sizes between a texture and a bitmap is not matched.");
            return false;
        }

        UnityEngine.Color texclr;
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                System.Drawing.Color bmpclr = bmp.GetPixel(x, y);
                texclr.r = (float)bmpclr.R / 255.0f;
                texclr.g = (float)bmpclr.G / 255.0f;
                texclr.b = (float)bmpclr.B / 255.0f;
                texclr.a = (float)bmpclr.A / 255.0f;
                texture.SetPixel(x, texture.height-y, texclr);
            }
        }
            
        return true;
    }

    public static Bitmap ResizeBitmap( Bitmap image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        using (var graphics = System.Drawing.Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }


    public static System.Drawing.Image Texture2Image(Texture2D texture)
    {
        if (texture == null)
            return null;

        //Save the texture to the stream.
        byte[] bytes = texture.EncodeToPNG();
        //Memory stream to store the bitmap data.
        MemoryStream ms = new MemoryStream(bytes);
        ms.Seek(0, SeekOrigin.Begin);
        System.Drawing.Image bmp = System.Drawing.Bitmap.FromStream(ms);
        ms.Close();
        ms = null;

        return bmp;
    }


}

