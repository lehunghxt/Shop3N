using System;
using System.Drawing;

namespace Library.Images
{
    public class RotateImage
    {
        private Image _Image;
        public string ImageUrl { get; set; }
        public float Angle { get; set; }
        public Bitmap Image { get; private set; }

        public RotateImage(string url="", float goc=0)
        {
            this.ImageUrl = url;
            _Image = (Bitmap)Bitmap.FromFile(ImageUrl);
            this.Angle = goc;
        }

        public void Rotate()
        {
            if (_Image == null)
                throw new ArgumentNullException("image");

            const double pi2 = Math.PI / 2.0;
            double oldWidth = (double)_Image.Width;
            double oldHeight = (double)_Image.Height;

            // Chuyển đổi độ sang radian
            double theta = ((double)this.Angle) * Math.PI / 180.0;
            double locked_theta = theta;

            // Đảm bảo theta bây giờ là [0, 2pi)
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight;

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;

            if ((locked_theta >= 0.0 && locked_theta < pi2) || (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);

            Image = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(Image))
            {
                Point[] points;
                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[] { 
											 new Point( (int) oppositeBottom, 0 ), 
											 new Point( nWidth, (int) oppositeTop ),
											 new Point( 0, (int) adjacentBottom )
										 };

                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[] { 
											 new Point( nWidth, (int) oppositeTop ),
											 new Point( (int) adjacentTop, nHeight ),
											 new Point( (int) oppositeBottom, 0 )						 
										 };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[] { 
											 new Point( (int) adjacentTop, nHeight ), 
											 new Point( 0, (int) adjacentBottom ),
											 new Point( nWidth, (int) oppositeTop )
										 };
                }
                else
                {
                    points = new Point[] { 
											 new Point( 0, (int) adjacentBottom ), 
											 new Point( (int) oppositeBottom, 0 ),
											 new Point( (int) adjacentTop, nHeight )		
										 };
                }

                g.DrawImage(_Image, points);
            }
        }
    }
}
