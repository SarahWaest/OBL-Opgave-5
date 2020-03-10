using System;

namespace Opgave5
{
    public class Bog
    {
        private int _sidetal;
        private string _forfatter;
        private string _titel;
        private string _isbn13;

        public Bog()
        {

        }

        public Bog(int sidetal, string forfatter, string titel, string isbn13)
        {
            _sidetal = sidetal;
            _forfatter = forfatter;
            _titel = titel;
            _isbn13 = isbn13;
        }

        public string Titel
        {
            get => _titel;
            set => _titel = value;
        }

        public string Forfatter
        {
            get => _forfatter;
            set
            {
                if (value.Length < 2) throw new ArgumentOutOfRangeException();
                _forfatter = value;
            }
        }

        public int Sidetal
        {
            get => _sidetal;
            set
            {
                if (value <= 4 || value >= 1000) throw new ArgumentOutOfRangeException();
                _sidetal = value;
            }
        }

        public string Isbn13
        {
            get => _isbn13;
            set
            {
                if (value.Length > 13 || value.Length < 11) throw new ArgumentOutOfRangeException();
                _isbn13 = value;
            }
        }

        public override string ToString()
        {
            return Forfatter + " " + Titel + " " + Sidetal + " " + Isbn13;
        }
    }
}