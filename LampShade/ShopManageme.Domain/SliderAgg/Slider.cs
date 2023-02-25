using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;

namespace ShopManagement.Domain.SliderAgg
{
    public class Slider:EntityBase
    {
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get;private set; }
        public string Link { get; private set; }
        public bool IsRemoved { get; private set; }

        public Slider(string picture, string pictureTitle, string pictureAlt, string heading, string title, string text, string btnText,string link)
        {
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link=link;
            IsRemoved = false;
        }
        public void Edit(string picture, string pictureTitle, string pictureAlt, string heading, string title, string text, string btbText,string link)
        {
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btbText;
            Link=link;
            IsRemoved = false;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
