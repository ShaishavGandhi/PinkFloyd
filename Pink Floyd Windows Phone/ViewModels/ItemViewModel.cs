using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Pink_Floyd_Windows_Phone.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _id;
        /// <summary>
        /// Sample ViewModel property; this property is used to identify the object.
        /// </summary>
        /// <returns></returns>
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private string _lineOne;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineOne
        {
            get
            {
                return _lineOne;
            }
            set
            {
                if (value != _lineOne)
                {
                    _lineOne = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private string _image;
        public string Image1
        {
            get
            {
                return _image;
            }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    NotifyPropertyChanged("Image1");
                }
            }
        }

        private string _lyrics;

        public string Lyrics
        {
            get
            {
                return _lyrics;
            }
            set
            {
                if (value != _lyrics)
                {
                    _lyrics = value;
                    NotifyPropertyChanged("Lyrics");
                }
            }
        }

        private string _audiosrc;

        public string AudioSrc
        {

            get
            {
                return _audiosrc;

            }

            set
            {
                if (value != _audiosrc)
                {
                    _audiosrc = value;
                    NotifyPropertyChanged("AudioSrc");
                }
            }
        }

        private string _trivia;

        public string Trivia
        {
            get
            {
                return _trivia;
            }
            set
            {
                if (value != _trivia)
                {
                    _trivia = value;
                    NotifyPropertyChanged("Trivia");
                }
            }
        }

        private string _personnel;

        public string Personnel
        {
            get
            {
                return _personnel;
            }
            set
            {
                if (value != _personnel)
                {
                    _personnel = value;
                    NotifyPropertyChanged("Personnel");
                }
            }
        }


        private string _lineTwo;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                if (value != _lineTwo)
                {
                    _lineTwo = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }

        private string _lineThree;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LineThree
        {
            get
            {
                return _lineThree;
            }
            set
            {
                if (value != _lineThree)
                {
                    _lineThree = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}