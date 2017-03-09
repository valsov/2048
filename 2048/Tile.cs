using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2048
{
    public class Tile
    {
        public static int minValue { get; set; }

        public bool changed { get; set; }

        public bool merged { get; set; }

        public ImageSource image { get; set; }


        private int value;

        public int Value {
            get { return this.value; }
            set
            {
                this.value = value;
                this.changed = true;        //if the value is set, we need to know it
                this.merged = false;

                SetImage();                 //set the new image for the tile
            }
        }



        public Tile()
        {
            Value = 0;
            minValue = 2;
            changed = false;
        }


        public void DoubleValue()
        {
            Value *= 2;
        }


        void SetImage()
        {
            var imageLocation = Game.images[value];
            image = new BitmapImage(new Uri(imageLocation, UriKind.Relative));
        }
    }
}
